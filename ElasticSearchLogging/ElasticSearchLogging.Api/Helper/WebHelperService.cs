using System.Text;
using Microsoft.AspNetCore.Http.Extensions;

namespace ElasticSearchLogging.Api.Helper;

public class WebHelperService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public WebHelperService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string ConvertExceptionToText(Exception exception)
        {
            StringBuilder error = new StringBuilder();

            if (exception != null)
            {
                error.AppendLine("Message:");
                error.AppendLine(exception.Message);
                error.AppendLine();

                if (exception.Source != null)
                {
                    error.AppendLine("Source:");
                    error.AppendLine(exception.Source);
                    error.AppendLine();
                }

                if (exception.StackTrace != null)
                {
                    error.AppendLine("StackTrace:");
                    error.AppendLine(exception.StackTrace);
                    error.AppendLine();
                }

                if (exception.InnerException != null)
                {
                    error.AppendLine("InnerException:");
                    error.AppendLine(exception.InnerException.ToString());
                    error.AppendLine();

                    error.AppendLine("InnerException Message:");
                    error.AppendLine(exception.InnerException.Message);
                    error.AppendLine();

                    if (exception.InnerException.Source != null)
                    {
                        error.AppendLine("InnerException Source:");
                        error.AppendLine(exception.InnerException.Source);
                        error.AppendLine();
                    }

                    if (exception.InnerException.StackTrace != null)
                    {
                        error.AppendLine("InnerException StackTrace:");
                        error.AppendLine(exception.InnerException.StackTrace);
                        error.AppendLine();
                    }
                }
            }

            return error.ToString();
        }

        public Uri GetAbsoluteUri()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.Host;
            uriBuilder.Path = request.Path.ToString();
            uriBuilder.Query = request.QueryString.ToString();

            return uriBuilder.Uri;
        }

        public string GetRequestUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var host = request.Host;
            
            host = new HostString(host.Host, request.Host.Port.HasValue ? host.Port.Value : 0);
            var url = UriHelper.BuildAbsolute(
                request.IsHttps ? "https" : "http",
                host,
                request.PathBase,
                request.Path);

            return url;
        }
        
        public string? GetQueryString()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            return request.QueryString.HasValue ? request.QueryString.Value : String.Empty;
        }
        
        public string GetBaseUrl()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var host = request.Host;
            
            host = new HostString(host.Host, request.Host.Port.HasValue ? host.Port.Value : 0);
            var url = UriHelper.BuildAbsolute(
                request.IsHttps ? "https" : "http",
                host);

            return url;
        }

        public string GetIpAddress()
        {
            return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();
        }
    }