using URLShortener.Common;
using URLShortener.Infra.Repositories;
using URLShortener.Models;
using URLShortener.Models.Dtos;

namespace URLShortener.Services;

public class UrlShorteningService(IUrlRepository urlRepository) : IUrlShorteningService
{
    private readonly Random _random = new();
    public async Task<string> GenerateCodeAsync()
    {
        var len = ShortLinkSettings.Length;
        var newCode = new char[len];

        while (true)
        {
            for (int i = 0; i < len; i++)
                newCode[i] = ShortLinkSettings.Alphabet[_random.Next(ShortLinkSettings.Alphabet.Length)];
            
            var code = new string(newCode);
            if(await urlRepository.CodeUnique(code))
                return code;
        }
    }
    public async Task<UrlDto> CreateUrlAsync(string originUrl)
    {
        var code = await GenerateCodeAsync();
        var newUrl = new UrlDto(code);
        var entity = new Url
        {
            OriginalUrl = originUrl,
            Code = code,
            ShortUrl = newUrl.shortUrlCode
        };
        
        await urlRepository.CreateAsync(entity);
        await  urlRepository.SaveChangesAsync();
        return newUrl;
    }

    public async Task<string> GetOriginalUrlAsync(string code)
    {
        var original = await urlRepository.FindByShortcodeAsync(code!);
        if (original is null)
            return ErrorMsgs.UrlNotFound;
        return original.OriginalUrl;
    }
    
}
