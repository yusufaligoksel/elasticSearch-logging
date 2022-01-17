using ElasticSearchLogging.Api.Mapping;
using Nest;

namespace ElasticSearchLogging.Api.Extension;

public static class ElasticSearchExtension
{
    public async static Task AddElasticsearch(
        this IServiceCollection services, IConfiguration configuration)
    {
        var url = configuration["ElasticSearchSetting:Url"];
        var defaultIndex = configuration["ElasticSearchSetting:IndexName"];

        var settings = new ConnectionSettings(new Uri(url))
            .DefaultIndex(defaultIndex);

        var client = new ElasticClient(settings);

        services.AddSingleton<IElasticClient>(client);

        await CreateIndex(client, defaultIndex);
    }
    
    // create index by name
    public static async Task CreateIndex(IElasticClient client, string indexName)
    {
        var any = await client.Indices.ExistsAsync(indexName);
        if (any.Exists)
            return;
            
        var createIndexResponse = await client.Indices.CreateAsync(indexName,
            ci => ci
                .Index(indexName)
                .LogMapping()
                .Settings(s => s.NumberOfShards(3).NumberOfReplicas(1))
        );
    }
}