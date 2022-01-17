using ElasticSearchLogging.Api.Extension;
using ElasticSearchLogging.Api.Helper;
using ElasticSearchLogging.Api.Service.Abstract;
using ElasticSearchLogging.Api.Service.Concrete;
using ElasticSearchLogging.Api.Setting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

#region Options
builder.Services.Configure<ElasticSearchSetting>(builder.Configuration.GetSection("ElasticSearchSetting"));
#endregion

#region DI
builder.Services.AddScoped<ILogService, ElasticSearchLogService>();
builder.Services.AddScoped<WebHelperService>();
#endregion

#region ElasticSearch
builder.Services.AddElasticsearch(builder.Configuration);
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();