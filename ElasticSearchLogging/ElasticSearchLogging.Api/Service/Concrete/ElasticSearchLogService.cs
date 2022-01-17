using ElasticSearchLogging.Api.Model;
using ElasticSearchLogging.Api.Service.Abstract;
using ElasticSearchLogging.Api.Setting;
using Microsoft.Extensions.Options;
using Nest;

namespace ElasticSearchLogging.Api.Service.Concrete;

public class ElasticSearchLogService:ILogService
{
    private readonly IElasticClient _client;
    private readonly ElasticSearchSetting _elasticSearchSetting;

    public ElasticSearchLogService(IElasticClient client,
        IOptions<ElasticSearchSetting> elasticSearchSetting)
    {
        _client = client;
        _elasticSearchSetting = elasticSearchSetting.Value;
    }
    public async Task<IEnumerable<LogModel>> GetLogsAsync()
    {
        var logs = await _client.SearchAsync<LogModel>(q => q.Index(_elasticSearchSetting.IndexName)); //.Scroll("5m")
        return logs.Documents.ToList();
    }

    public async Task<LogModel> Find(string id)
    {
        var result = await _client.GetAsync<LogModel>(id, q => q.Index(_elasticSearchSetting.IndexName));
        return result.Source;
    }

    public async Task<IEnumerable<LogModel>> GetLogsByLevelId(int levelId)
    {
        var query = QueryOperation.CreateQuery(levelId, _elasticSearchSetting.IndexName);
        var result = await _client.SearchAsync<LogModel>(query);
        return result.Documents.ToList();
    }

    public async Task DeleteAsync(string id)
    { 
        var entity = await Find(id);
        await _client.DeleteAsync<LogModel>(entity);
    }

    public async Task DeleteAsync(LogModel logModel)
    {
        await _client.DeleteAsync<LogModel>(logModel);
    }

    public async Task InsertLog(LogModel logModel)
    {
        await _client.IndexDocumentAsync(logModel);
    }

    public async Task InsertLogRange(LogModel[] logModels)
    {
        await _client.IndexManyAsync(logModels);
    }

    public async Task UpdateLog(LogModel logModel)
    {
        await _client.UpdateAsync<LogModel>(logModel, u => u.Doc(logModel));
    }

    public async Task SaveBulkAsync(LogModel[] logModels)
    {
        await _client.BulkAsync(b => b.Index(_elasticSearchSetting.IndexName).IndexMany(logModels));
    }
}