using System.Reflection;
using Nancy;

namespace HelloNancy.Modules
{
    public class IndexModule : NancyModule
    {
        public IndexModule()
        {
            //string projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
            //var resourcePath = projectName + ".FactSet.FactSetSymbologySettings.json";
            //var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourcePath);



            //Get["/"] = parameters => "IndexModule";
            //Get["/List"] = parameters => "IndexModule/List";
            //Delete["/Delete"] = parameters => "IndexModule/Delete";

            Get("/", _ => { return "IndexModule"; } );
            Get("/List", parameters => "IndexModule/List");
            Delete("/Delete", parameters => "IndexModule/Delete");
        }
    }
}