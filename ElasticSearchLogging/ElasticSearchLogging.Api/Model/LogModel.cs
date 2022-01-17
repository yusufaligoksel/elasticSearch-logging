using LogLevel = ElasticSearchLogging.Api.Enum.LogLevel;

namespace ElasticSearchLogging.Api.Model;

public class LogModel
{
    public string Id { get; set; }
    public virtual LogLevel LevelId { get; set; }
    public virtual string ShortMessage { get; set; }
    public virtual string FullMessage { get; set; }
    public virtual string Data { get; set; }
    public virtual string RequestUrl { get; set; }
    public virtual string Parameters { get; set; }
    public virtual DateTime CreatedDate { get; set; }
    public virtual int? UserId { get; set; }
    public virtual string Email { get; set; }
    public virtual string IpAddress { get; set; }
}