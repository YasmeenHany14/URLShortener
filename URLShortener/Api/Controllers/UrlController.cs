using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Common;
using URLShortener.Services;

namespace URLShortener.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlController(IUrlShorteningService urlShorteningService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateShortUrlAsync(CreateUrlRequest urlRequest)
    {
        var response = await urlShorteningService.CreateUrlAsync(urlRequest.OriginalUrl);
        return Ok(response);
    }

    [HttpGet("~/{code:regex(^[[a-zA-Z0-9]]{{7}}$)}")]
    public async Task<IActionResult> RedirectToDestinationAsync(string code)
    {
        var url = await urlShorteningService.GetOriginalUrlAsync(code);
        if (url == ErrorMsgs.UrlNotFound)
            return NotFound(ErrorMsgs.UrlNotFound);
        return Redirect(url);
    }

    public record CreateUrlRequest
    {
        [Required]
        [RegularExpression("(http(s)?:\\/\\/.)?(www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{2,256}\\.[a-z]{2,6}\\b([-a-zA-Z0-9@:%_\\+.~#?&//=]*)", ErrorMessage = "Invalid URL")] // temperoraly check with regex
        public string OriginalUrl { get; init; }
    }
}
