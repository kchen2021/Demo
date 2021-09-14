using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using SwaggerWebapi.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SwaggerWebapi.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class SwaggerExtention
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerExtentionConfigureService(this IServiceCollection services)
        {
            services.AddApiVersioning();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo()
                {
                    Title = "My API",
                    Version = "v1.0",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("http://www.google.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "kevin",
                        Email = "www.baidu.com",
                        Url = new Uri("http://www@google.com")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "License",
                        Url = new Uri("http://www.google.com")
                    }

                });
                c.SwaggerDoc("v2.0", new OpenApiInfo {Title = "MapMaster", Version = "v2.0"});


                c.OperationFilter<RemoveVersionParametersFilter>();
                c.DocumentFilter<SetVersionInPathsFilter>();

                c.DocInclusionPredicate((docName, apiDesc) =>
                    {
                        if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                        var versions = methodInfo.DeclaringType
                            .GetCustomAttributes(true)
                            .OfType<ApiVersionAttribute>()
                            .SelectMany(attr => attr.Versions);


                        var maps = methodInfo.DeclaringType
                            .GetCustomAttributes(true)
                            .OfType<MapToApiVersionAttribute>()
                            .Select(x => x.Versions);

                        return versions.Any(v => $"v{v.ToString()}" == docName) &&
                               (maps.Count() == 0 || maps.Any(v => $"v{v.ToString()}" == docName));
                    }
                );

                #region bind xml

                // 为 Swagger JSON and UI设置xml文档注释路径
                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location); //获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                var xmlPath = Path.Combine(basePath, "SwaggerWebapi.xml");
                if (File.Exists(Path.Combine(basePath, xmlPath)))
                {
                    c.IncludeXmlComments(xmlPath);
                }

                #endregion


                c.DescribeAllEnumsAsStrings();
                c.EnableAnnotations();

            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void UseSwaggerExtentionConfigure(this IApplicationBuilder app)
        {
            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "My API v1.0");
                c.SwaggerEndpoint("/swagger/v2.0/swagger.json", "My API v2.0");
            });
        }
    }
}
