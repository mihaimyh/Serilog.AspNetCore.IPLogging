﻿using Microsoft.AspNetCore.Builder;
using Serilog.AspNetCore.IPLogging.Middlewares;
using System;

namespace Serilog.AspNetCore.IPLogging
{
    public static class LoggingApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseIPAddressLogging(this IApplicationBuilder app)
        {
            if (app == null) throw new ArgumentNullException(nameof(app));

            return app.UseMiddleware<IPAddressLoggingMiddleware>();
        }
    }
}