using ElasticSearchLogging.Api.Model;
using Nest;

namespace ElasticSearchLogging.Api.Service.Concrete;

public static class QueryOperation
{
    public static SearchRequest<LogModel> CreateQuery(int levelId,string index)
    {
        var sr = new SearchRequest<LogModel>(index)
        {
            TrackScores = true
        };
            
        QueryContainer queryContainer = null;
        queryContainer&= new BoolQuery { Must = new QueryContainer[] { new TermQuery { Field = "levelId", Value = levelId, Boost = 0.0 } } };
        sr.Query = queryContainer;
            
        return sr;
    }
}