namespace Civiti.Api.Infrastructure.Constants;

/// <summary>
/// Centralised domain error messages used in services and endpoints.
/// Matching on these constants (instead of string literals) prevents
/// silent breakage from typos in catch-when guards and switch expressions.
/// </summary>
public static class DomainErrors
{
    public const string AccountDeleted = "This account has been deleted";
    public const string UserNotFound = "User not found";
    public const string UserProfileNotFound = "User profile not found";
    public const string IssueNotFound = "Issue not found";
    public const string CommentNotFound = "Comment not found";
    public const string ParentCommentNotFound = "Parent comment not found";
    public const string EditOwnCommentsOnly = "You can only edit your own comments";
    public const string DeleteOwnCommentsOnly = "You can only delete your own comments";
    public const string EditOwnIssuesOnly = "You can only edit your own issues";
    public const string ChangeOwnIssueStatusOnly = "You can only change status of your own issues";
    public const string CommentRateLimited = "Please wait before posting another comment";
    public const string DuplicateComment = "You have already posted this comment";
}
