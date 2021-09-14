using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace SwaggerWebapi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class DefaultPageExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseDefaultPage(this IApplicationBuilder app)
        {
            DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            defaultFilesOptions.DefaultFileNames.Clear();
            defaultFilesOptions.DefaultFileNames.Add("swagger/index.html");
            app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.Run(ctx =>
            {
                ctx.Response.Redirect("/swagger/"); //可以支持虚拟路径或者index.html这类起始页.
                return Task.FromResult(0);
            });
        }
    }
}
