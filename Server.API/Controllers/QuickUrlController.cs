using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System;
using Microsoft.Extensions.Caching.Memory;
namespace Server.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuickUrlController : ControllerBase
{
    private readonly ILogger<QuickUrlController> _logger;
    private readonly IMemoryCache _cache;

    public QuickUrlController(IMemoryCache memoryCache, ILogger<QuickUrlController> logger)
    {
        _logger = logger;
        _cache = memoryCache;
    }
    [HttpGet]
    public async Task<ActionResult> Get(string urlShort)
    {
        string res = "";
        if (urlShort.Contains("http"))
        {
            MyUrlObject myurl = new MyUrlObject(urlShort);
            _cache.Set(myurl.SmallerURL, myurl);
            res = JsonSerializer.Serialize(myurl);
        }
        else
        {
            if (_cache.TryGetValue(urlShort, out MyUrlObject myurl))
            {
                res = JsonSerializer.Serialize(myurl);
            }
            else
            {
                res = @"{""OriginalURL"":"""",""SmallerURL"":""""}";
            }
        }
        //Console.WriteLine(res);
        _logger.LogInformation(res);
        var result = new ContentResult()
        {
            StatusCode = 200,
            ContentType = "application/json",
            Content = res
        };
        //return NotFound();
        return result;
    }
}