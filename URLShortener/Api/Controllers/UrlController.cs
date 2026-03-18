using Microsoft.AspNetCore.Mvc;
using URLShortener.Common;
using URLShortener.Services;

namespace URLShortener.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UrlController(IUrlShorteningService urlShorteningService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateShortUrl(CreateUrlRequest urlRequest)
    {
        var response = await urlShorteningService.CreateUrlAsync(urlRequest.OriginalUrl);
        return Ok(response);
    }

    [HttpGet("~/{code:regex(^[[a-zA-Z0-9]]{{7}}$)}")]
    public async Task<IActionResult> RedirectToDestination(string code)
    {
        var url = await urlShorteningService.GetOriginalUrlAsync(code);
        if (url == ErrorMsgs.UrlNotFound)
            return NotFound(ErrorMsgs.UrlNotFound);
        return Redirect(url);
    }

    public record CreateUrlRequest
    {
        public string OriginalUrl { get; init; }
    }
}
