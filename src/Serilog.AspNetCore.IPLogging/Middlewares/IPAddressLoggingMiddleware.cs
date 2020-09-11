using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System;
using System.Threading.Tasks;

namespace Serilog.AspNetCore.IPLogging.Middlewares
{
    internal class IPAddressLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public IPAddressLoggingMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext is null)
            {
                throw new ArgumentNullException(nameof(httpContext));
            }

            if (httpContext.Request.Headers.TryGetValue("X-Forwarded-For", out var headerValue))
            {
                await LogToContext(httpContext, headerValue.ToString());
            }
            else
            {
                await LogToContext(httpContext, httpContext?.Connection?.RemoteIpAddress.ToString());
            }
        }

        private async Task LogToContext(HttpContext httpContext, string content)
        {
            using (LogContext.PushProperty("IPAddress", content))
            {
                await _next(httpContext);
            }
        }
    }
}