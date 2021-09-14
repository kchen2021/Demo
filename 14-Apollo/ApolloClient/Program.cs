using System.Threading.Tasks;
using Autofac;
using Com.Ctrip.Framework.Apollo;

namespace ApolloClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //Defualt namespace
            IConfig config = await ApolloConfigurationManager.GetAppConfig(); //config instance is singleton for each namespace and is never null
            string someKey = "someKeyFromDefaultNamespace";
            string someDefaultValue = "someDefaultValueForTheKey";
            string value = config.GetProperty(someKey, someDefaultValue);
            
            //Public namespace
            //string somePublicNamespace = "CAT";
            //IConfig config = await ApolloConfigurationManager.GetConfig(somePublicNamespace); //config instance is singleton for each namespace and is never null
            //string someKey = "someKeyFromPublicNamespace";
            //string someDefaultValue = "someDefaultValueForTheKey";
            //string value = config.GetProperty(someKey, someDefaultValue);
            
            ////merege multi namespaces
            //string somePublicNamespace = "CAT";
            //IConfig config = await ApolloConfigurationManager.GetConfig(new[] { somePublicNamespace， ConfigConsts.NamespaceApplication }); //config instance is singleton for each namespace and is never null
            //string someKey = "someKeyFromPublicNamespace";
            //string someDefaultValue = "someDefaultValueForTheKey";
            //string value = config.GetProperty(someKey, someDefaultValue);

        }
    }
}
