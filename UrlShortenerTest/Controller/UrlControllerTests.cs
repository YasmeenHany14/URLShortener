using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Api.Controllers;
using URLShortener.Common;
using URLShortener.Models.Dtos;
using URLShortener.Services;
using Xunit.Sdk;

namespace UrlShortnerTest.Controller;

public class UrlControllerTests
{
    private readonly IUrlShorteningService _urlShorteningService;
    public UrlControllerTests()
    {
        _urlShorteningService = A.Fake<IUrlShorteningService>();
    }

    [Theory]
    [InlineData("https://www.google.com/")]
    public async Task UrlController_CreateShortUrlAsync_ReturnOk(string url)
    {
        // arrange
        var request = new UrlController.CreateUrlRequest { OriginalUrl = url };
        var expectedResponse = new UrlDto("NqoTh9A");

        A.CallTo(() => _urlShorteningService.CreateUrlAsync(url))
            .Returns(expectedResponse);

        var controller = new UrlController(_urlShorteningService);

        // act
        var result = await controller.CreateShortUrlAsync(request);

        // assert
        result.Should().NotBeNull();
        result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeEquivalentTo(expectedResponse);
    }
    
    // [Theory]
    // [InlineData("https://www.google.com/")]
    // public async Task UrlController_CreateShortUrlAsync_Return500(string url)
    // {
    //     // arrange
    //     var request = new UrlController.CreateUrlRequest { OriginalUrl = url };
    //
    //     A.CallTo(() => _urlShorteningService.CreateUrlAsync(url))
    //         .Throws<Exception>();
    //
    //     var controller = new UrlController(_urlShorteningService);
    //
    //     // act
    //     var result = await controller.CreateShortUrlAsync(request);
    //
    //     // assert
    //     result.Should().BeOfType<ObjectResult>()
    //         .Which.StatusCode.Should().Be(500);
    // }

    [Theory]
    [InlineData("NqoTh9A")]
    public async Task UrlController_RedirectToDestinationAsync_ReturnNotFound(string code)
    {
        A.CallTo(() => _urlShorteningService.GetOriginalUrlAsync(code))
            .Returns(ErrorMsgs.UrlNotFound);
        
        var controller = new UrlController(_urlShorteningService);
        
        var result = await controller.RedirectToDestinationAsync(code);
        
        result.Should().BeOfType<NotFoundObjectResult>()
            .Which.Value.Should().Be(ErrorMsgs.UrlNotFound);

        result.As<NotFoundObjectResult>()
            .StatusCode.Should().Be(404);
    }
    
    [Theory]
    [InlineData("NqoTh9A", "https://www.google.com/")]
    public async Task UrlController_RedirectToDestinationAsync_ReturnRedirect(string code, string targetUrl)
    {
        A.CallTo(() => _urlShorteningService.GetOriginalUrlAsync(code))
            .Returns(targetUrl);
        
        var controller = new UrlController(_urlShorteningService);
        
        var result = await controller.RedirectToDestinationAsync(code);
        result.Should().NotBeNull();
        result.Should().BeOfType<RedirectResult>()
            .Which.Url.Should().Be(targetUrl);
    }
    
    // [Theory]
    // [InlineData("NqoTh9A", "https://www.google.com/")]
    // public async Task UrlController_RedirectToDestinationAsync_Return500(string code, string targetUrl)
    // {
    //     A.CallTo(() => _urlShorteningService.GetOriginalUrlAsync(code))
    //         .Throws<Exception>();
    //     
    //     var controller = new UrlController(_urlShorteningService);
    //     
    //     var result = await controller.RedirectToDestinationAsync(code);
    //     result.Should().BeOfType<ObjectResult>()
    //         .Which.StatusCode.Should().Be(500);    }
}
