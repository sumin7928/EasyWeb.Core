using EasyWeb.Core.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyWeb.Core.Extensions
{
    public static class ApplicationExtension
    {
        public static IApplicationBuilder UseWebLog(this IApplicationBuilder app)
        {
            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/swagger"), appBuilder =>
            {
                appBuilder.UseMiddleware<WebLogMiddleware>();
            });

            return app;
        }
    }
}
