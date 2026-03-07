using Civiti.Api.Data;
using Civiti.Api.Models.Domain;
using Civiti.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Civiti.Api.Services;

public class PushTokenService(
    CivitiDbContext context,
    ILogger<PushTokenService> logger) : IPushTokenService
{
    public async Task RegisterTokenAsync(Guid userId, string token, string platform)
    {
        if (!Enum.TryParse<PushTokenPlatform>(platform, ignoreCase: true, out var parsedPlatform))
            throw new ArgumentException($"Invalid platform: {platform}. Must be 'ios' or 'android'.");

        try
        {
            await UpsertTokenAsync(userId, token, parsedPlatform);
        }
        catch (DbUpdateException)
        {
            // Race condition: another request inserted the same token concurrently.
            // Retry once — the token now exists, so the upsert will take the update path.
            context.ChangeTracker.Clear();
            await UpsertTokenAsync(userId, token, parsedPlatform);
        }
    }

    private async Task UpsertTokenAsync(Guid userId, string token, PushTokenPlatform platform)
    {
        PushToken? existing = await context.PushTokens
            .FirstOrDefaultAsync(pt => pt.Token == token);

        if (existing != null)
        {
            if (existing.UserId != userId)
            {
                logger.LogInformation("Reassigning push token to user {NewUserId}", userId);
                existing.UserId = userId;
            }

            existing.Platform = platform;
            existing.UpdatedAt = DateTime.UtcNow;
        }
        else
        {
            context.PushTokens.Add(new PushToken
            {
                UserId = userId,
                Token = token,
                Platform = platform
            });
        }

        await context.SaveChangesAsync();
    }

    public async Task DeregisterTokenAsync(Guid userId, string token)
    {
        int deleted = await context.PushTokens
            .Where(pt => pt.Token == token && pt.UserId == userId)
            .ExecuteDeleteAsync();

        if (deleted > 0)
        {
            logger.LogInformation("Deregistered push token for user {UserId}", userId);
        }
    }
}
