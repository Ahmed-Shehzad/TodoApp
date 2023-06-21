using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace Foundation.API.Extensions
{
    public static class SwaggerUIOptionsExtensions
    {
        /// <summary>
        ///     Configures swagger endpoint with base path.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="url"></param>
        /// <param name="name"></param>
        /// <param name="basePath"></param>
        public static void SwaggerEndpoint(this SwaggerUIOptions options, string url, string name, string? basePath)
        {
            if (!string.IsNullOrWhiteSpace(basePath))
            {
                basePath = basePath.Trim(' ', '/');
                url = url.Trim(' ', '/');
                if (!url.StartsWith(basePath))
                {
                    url = basePath + '/' + url;
                }
                url = '/' + url;
            }
            options.SwaggerEndpoint(url, name);
        } 
    }
}