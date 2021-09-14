using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace WebService
{
    /// <summary>
    /// WebServiceDemo 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebServiceDemo : System.Web.Services.WebService
    {
        public CertificateSoapHeader SoapHeader = new CertificateSoapHeader();


        [SoapHeader("SoapHeader", Direction = SoapHeaderDirection.In)]
        //[WebMethod(Description = "HelloWorld")]
        [WebMethod(Description = "HelloWorld", EnableSession = false, BufferResponse = true, CacheDuration = 0)]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [SoapHeader("SoapHeader", Direction = SoapHeaderDirection.In)]
        [WebMethod(Description = "求和方法", EnableSession = false, BufferResponse = true, CacheDuration = 0)]
        public int Sum(int a, int b)
        {
            if (!SoapHeader.ValideUser(SoapHeader.UserName, SoapHeader.Password))
            {
                return 0;
            }

            return a + b;
        }
    }
}
