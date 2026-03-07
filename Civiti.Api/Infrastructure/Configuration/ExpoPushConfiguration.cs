namespace Civiti.Api.Infrastructure.Configuration;

public class ExpoPushConfiguration
{
    public string? AccessToken { get; set; }
    public int ChannelCapacity { get; set; } = 10_000;
    public int BatchSize { get; set; } = 100;

    public bool IsConfigured => true; // Expo push works without an access token for low volume
}
