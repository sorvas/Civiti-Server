using System.ComponentModel.DataAnnotations;
using Civiti.Api.Models.Domain;

namespace Civiti.Api.Models.Requests.Reports;

public class CreateReportRequest : IValidatableObject
{
    [Required(ErrorMessage = "Field 'reason' is required.")]
    public string? Reason { get; set; }

    [MaxLength(500, ErrorMessage = "Field 'details' must not exceed {1} characters.")]
    public string? Details { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Reason) && !Enum.TryParse<ReportReason>(Reason, ignoreCase: true, out _))
        {
            yield return new ValidationResult(
                $"Field 'reason' must be one of: {string.Join(", ", Enum.GetNames<ReportReason>())}.",
                [nameof(Reason)]);
        }
    }

    public ReportReason ParsedReason => Enum.Parse<ReportReason>(Reason!, ignoreCase: true);
}
