using System.ComponentModel.DataAnnotations;
using Civiti.Api.Infrastructure.Constants;
using Civiti.Api.Models.Requests.Issues;
using FluentAssertions;

namespace Civiti.Tests.Validators;

public class CreateIssueRequestValidatorTests
{
    private static bool TryValidate(CreateIssueRequest request, out List<ValidationResult> results)
    {
        results = [];
        var context = new ValidationContext(request);
        return Validator.TryValidateObject(request, context, results, validateAllProperties: true);
    }

    [Fact]
    public void Should_Pass_When_PhotoUrls_Is_Null()
    {
        var request = new CreateIssueRequest { PhotoUrls = null };

        var isValid = TryValidate(request, out var results);

        results.Should().NotContain(r => r.MemberNames.Contains(nameof(CreateIssueRequest.PhotoUrls)));
    }

    [Fact]
    public void Should_Pass_When_PhotoUrls_Is_Empty()
    {
        var request = new CreateIssueRequest { PhotoUrls = [] };

        var isValid = TryValidate(request, out var results);

        results.Should().NotContain(r => r.MemberNames.Contains(nameof(CreateIssueRequest.PhotoUrls)));
    }

    [Fact]
    public void Should_Pass_When_PhotoUrls_At_Max()
    {
        var request = new CreateIssueRequest
        {
            PhotoUrls = Enumerable.Range(0, IssueValidationLimits.MaxPhotoCount)
                .Select(i => $"https://example.com/photo{i}.jpg")
                .ToList()
        };

        var isValid = TryValidate(request, out var results);

        results.Should().NotContain(r => r.MemberNames.Contains(nameof(CreateIssueRequest.PhotoUrls)));
    }

    [Fact]
    public void Should_Fail_When_PhotoUrls_Exceeds_Max()
    {
        var request = new CreateIssueRequest
        {
            PhotoUrls = Enumerable.Range(0, IssueValidationLimits.MaxPhotoCount + 1)
                .Select(i => $"https://example.com/photo{i}.jpg")
                .ToList()
        };

        var isValid = TryValidate(request, out var results);

        results.Should().Contain(r =>
            r.MemberNames.Contains(nameof(CreateIssueRequest.PhotoUrls)) &&
            r.ErrorMessage!.Contains($"A maximum of {IssueValidationLimits.MaxPhotoCount} photos are allowed."));
    }
}
