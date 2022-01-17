using ElasticSearchLogging.Api.Model;

namespace ElasticSearchLogging.Api.Service.Abstract;

public interface ILogService
{
    Task<IEnumerable<LogModel>> GetLogsAsync();
    Task<LogModel> Find(string id);
    Task<IEnumerable<LogModel>> GetLogsByLevelId(int levelId);
    Task DeleteAsync(string id);
    Task DeleteAsync(LogModel logModel);
    Task InsertLog(LogModel logModel);
    Task InsertLogRange(LogModel[] logModels);
    Task UpdateLog(LogModel logModel);
    Task SaveBulkAsync(LogModel[] logModels);
}