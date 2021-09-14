using Nancy;
using Nancy.Bootstrapper;
using Nancy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Responses.Negotiation;

namespace NancyConsole
{
    public class AutofacBootstrapper : AutofacNancyBootstrapper
    {
        public AutofacBootstrapper() : base()
        {
        }

        protected override void ConfigureApplicationContainer(ILifetimeScope container)
        {
            SwaggerMetadataProvider.SetInfo("Nancy Swagger w/ AutoFac Example", "v0", "Our awesome service", new Contact()
            {
                EmailAddress = "exampleEmail@example.com"
            });

            SwaggerAnnotationsConfig.ShowOnlyAnnotatedRoutes = true;
            this.ApplicationPipelines.AfterRequest.AddItemToEndOfPipeline(x => x.Response.Headers.Add("Access-Control-Allow-Origin", "*"));
            this.ApplicationPipelines.AfterRequest.AddItemToEndOfPipeline(x => x.Response.Headers.Add("Access-Control-Allow-Headers", "*"));
            this.ApplicationPipelines.AfterRequest.AddItemToEndOfPipeline(x => x.Response.Headers.Add("Access-Control-Allow-Methods", "*"));
        }
    }
}
