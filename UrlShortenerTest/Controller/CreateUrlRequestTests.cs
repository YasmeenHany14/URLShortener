using System.ComponentModel.DataAnnotations;
using FluentAssertions;
using URLShortener.Api.Controllers;

namespace UrlShortenerTest.Controller;

public class CreateUrlRequestTests
{
    private IList<ValidationResult> ValidateModel(object model)
    {
        var validationResults = new List<ValidationResult>();
        var context = new ValidationContext(model);
        Validator.TryValidateObject(model, context, validationResults, true);
        return validationResults;
    }

    [Theory]
    [InlineData("https://www.google.com/")]
    [InlineData("http://www.github.com/")]
    [InlineData("https://youtube.com/")]
    public void CreateUrlRequest_ValidUrl_PassesValidation(string url)
    {
        // arrange
        var request = new UrlController.CreateUrlRequest { OriginalUrl = url };

        // act
        var result = ValidateModel(request);

        // assert
        result.Should().BeEmpty();
    }

    [Theory]
    [InlineData("false-url")]
    [InlineData("just some text")]
    [InlineData("ftp://invalid-scheme.com")]
    public void CreateUrlRequest_InvalidUrl_UrlNotValidFormat(string url)
    {
        // arrange
        var request = new UrlController.CreateUrlRequest { OriginalUrl = url };

        // act
        var result = ValidateModel(request);

        // assert
        result.Should().NotBeEmpty();
        result.First().ErrorMessage.Should().Be("Invalid URL");
    }
    
    [Theory]
    [InlineData("")]
    public void CreateUrlRequest_InvalidUrl_UrlEmpty(string url)
    {
        // arrange
        var request = new UrlController.CreateUrlRequest { OriginalUrl = url };

        // act
        var result = ValidateModel(request);

        // assert
        result.Should().NotBeEmpty();
    }
}
