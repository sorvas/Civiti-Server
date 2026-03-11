using System.ComponentModel.DataAnnotations;

namespace Civiti.Api.Models.Requests.Push;

public class RegisterPushTokenRequest : IValidatableObject
{
    [Required(ErrorMessage = "Push token is required.")]
    [MaxLength(255, ErrorMessage = "Push token must not exceed 255 characters.")]
    [RegularExpression(@"^Expo(nent)?PushToken\[.+\]$", ErrorMessage = "Invalid Expo push token format.")]
    public string Token { get; set; } = string.Empty;

    [Required(ErrorMessage = "Platform is required.")]
    public string Platform { get; set; } = string.Empty;

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrEmpty(Platform) &&
            !Platform.Equals("ios", StringComparison.OrdinalIgnoreCase) &&
            !Platform.Equals("android", StringComparison.OrdinalIgnoreCase))
        {
            yield return new ValidationResult(
                "Platform must be 'ios' or 'android'.",
                [nameof(Platform)]);
        }
    }
}
