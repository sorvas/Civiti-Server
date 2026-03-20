using Civiti.Api.Data;
using Civiti.Api.Infrastructure.Constants;
using Civiti.Api.Infrastructure.Exceptions;
using Civiti.Api.Models.Domain;
using Civiti.Api.Models.Requests.Reports;
using Civiti.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Civiti.Api.Services;

public class ReportService(
    ILogger<ReportService> logger,
    CivitiDbContext context) : IReportService
{
    private const int AutoFlagThreshold = 3;

    public async Task<(bool Success, Guid? ReportId, string? Error)> ReportIssueAsync(
        Guid issueId,
        CreateReportRequest request,
        string supabaseUserId)
    {
        try
        {
            UserProfile? user = await context.UserProfiles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.SupabaseUserId == supabaseUserId);

            if (user == null)
                return (false, null, DomainErrors.UserNotFound);
            if (user.IsDeleted)
                throw new AccountDeletedException();

            Issue? issue = await context.Issues
                .FirstOrDefaultAsync(i => i.Id == issueId);

            if (issue == null)
                return (false, null, DomainErrors.IssueNotFound);

            if (issue.UserId == user.Id)
                return (false, null, DomainErrors.CannotReportOwnContent);

            // DB-based rate limit: max reports in last hour
            var recentReportCount = await context.Reports
                .CountAsync(r => r.ReporterId == user.Id
                    && r.CreatedAt > DateTime.UtcNow.AddHours(-1));

            if (recentReportCount >= 5)
                return (false, null, DomainErrors.ReportRateLimited);

            // Check for duplicate report
            var alreadyReported = await context.Reports
                .AnyAsync(r => r.ReporterId == user.Id
                    && r.TargetType == "Issue"
                    && r.TargetId == issueId);

            if (alreadyReported)
                return (false, null, DomainErrors.AlreadyReported);

            Report report = new()
            {
                Id = Guid.NewGuid(),
                ReporterId = user.Id,
                TargetType = "Issue",
                TargetId = issueId,
                Reason = request.ParsedReason,
                Details = request.Details?.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            context.Reports.Add(report);

            // Increment report count and auto-flag if threshold reached
            issue.ReportCount++;
            if (issue.ReportCount >= AutoFlagThreshold && !issue.IsFlagged)
            {
                issue.IsFlagged = true;
                logger.LogWarning("Issue {IssueId} auto-flagged after {Count} reports", issueId, issue.ReportCount);
            }

            await context.SaveChangesAsync();

            logger.LogInformation(
                "User {UserId} reported issue {IssueId} for {Reason}",
                user.Id, issueId, request.Reason);

            return (true, report.Id, null);
        }
        catch (AccountDeletedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reporting issue: {IssueId}", issueId);
            throw;
        }
    }

    public async Task<(bool Success, Guid? ReportId, string? Error)> ReportCommentAsync(
        Guid commentId,
        CreateReportRequest request,
        string supabaseUserId)
    {
        try
        {
            UserProfile? user = await context.UserProfiles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(u => u.SupabaseUserId == supabaseUserId);

            if (user == null)
                return (false, null, DomainErrors.UserNotFound);
            if (user.IsDeleted)
                throw new AccountDeletedException();

            Comment? comment = await context.Comments
                .FirstOrDefaultAsync(c => c.Id == commentId && !c.IsDeleted);

            if (comment == null)
                return (false, null, DomainErrors.CommentNotFound);

            if (comment.UserId == user.Id)
                return (false, null, DomainErrors.CannotReportOwnContent);

            // DB-based rate limit: max reports in last hour
            var recentReportCount = await context.Reports
                .CountAsync(r => r.ReporterId == user.Id
                    && r.CreatedAt > DateTime.UtcNow.AddHours(-1));

            if (recentReportCount >= 5)
                return (false, null, DomainErrors.ReportRateLimited);

            // Check for duplicate report
            var alreadyReported = await context.Reports
                .AnyAsync(r => r.ReporterId == user.Id
                    && r.TargetType == "Comment"
                    && r.TargetId == commentId);

            if (alreadyReported)
                return (false, null, DomainErrors.AlreadyReported);

            Report report = new()
            {
                Id = Guid.NewGuid(),
                ReporterId = user.Id,
                TargetType = "Comment",
                TargetId = commentId,
                Reason = request.ParsedReason,
                Details = request.Details?.Trim(),
                CreatedAt = DateTime.UtcNow
            };

            context.Reports.Add(report);

            // Increment report count and auto-hide if threshold reached
            comment.ReportCount++;
            if (comment.ReportCount >= AutoFlagThreshold && !comment.IsHidden)
            {
                comment.IsHidden = true;
                logger.LogWarning("Comment {CommentId} auto-hidden after {Count} reports", commentId, comment.ReportCount);
            }

            await context.SaveChangesAsync();

            logger.LogInformation(
                "User {UserId} reported comment {CommentId} for {Reason}",
                user.Id, commentId, request.Reason);

            return (true, report.Id, null);
        }
        catch (AccountDeletedException)
        {
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error reporting comment: {CommentId}", commentId);
            throw;
        }
    }
}
