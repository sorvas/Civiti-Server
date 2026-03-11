using System.ComponentModel.DataAnnotations;

namespace Civiti.Api.Models.Requests.Push;

public class DeregisterPushTokenRequest
{
    [Required(ErrorMessage = "Push token is required.")]
    [MaxLength(255, ErrorMessage = "Push token must not exceed 255 characters.")]
    [RegularExpression(@"^Expo(nent)?PushToken\[.+\]$", ErrorMessage = "Invalid Expo push token format.")]
    public string Token { get; set; } = string.Empty;
}
