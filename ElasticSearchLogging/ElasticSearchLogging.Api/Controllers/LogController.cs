using ElasticSearchLogging.Api.Helper;
using ElasticSearchLogging.Api.Model;
using ElasticSearchLogging.Api.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ElasticSearchLogging.Api.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class LogController : ControllerBase
{
    private readonly ILogService _logService;
    private readonly WebHelperService _webHelperService;
    public LogController(ILogService logService,WebHelperService webHelperService)
    {
        _logService = logService;
        _webHelperService = webHelperService;
    }
    
    [HttpPost]
    public async Task<IActionResult> Add([FromBody]LogModel request)
    {
        request.Id = Guid.NewGuid().ToString();
        request.RequestUrl = _webHelperService.GetRequestUrl();
        request.Parameters = _webHelperService.GetQueryString();
        request.IpAddress = _webHelperService.GetIpAddress();
        request.CreatedDate=DateTime.Now;

        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = "Macbook Pro 2019",
            Stock = 45,
            IsActive = true,
            Price = 50M
        };
        request.Data = JsonConvert.SerializeObject(product);
        await _logService.InsertLog(request);
        return Ok("OK");
    }

    [HttpGet]
    public async Task<IActionResult> GetList()
    {
        var logs = await _logService.GetLogsAsync();
        return Ok(logs);
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] string id)
    {
        var log = await _logService.Find(id);
        return Ok(log);
    }

    [HttpGet]
    public async Task<IActionResult> GetListByLevelId([FromQuery] int levelId)
    {
        var logs = await _logService.GetLogsByLevelId(levelId);
        return Ok(logs);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] LogModel logModel)
    {
        await _logService.UpdateLog(logModel);
        return Ok(true);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromQuery] string id)
    {
        await _logService.DeleteAsync(id);
        return Ok(true);
    }
}