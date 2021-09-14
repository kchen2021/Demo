using System.Web.Services.Protocols;

namespace WebService
{
    public class CertificateSoapHeader: SoapHeader
    {
        private string username;
        private string password;
        public string UserName
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public bool ValideUser(string in_UserName, string in_PassWord)
        {
            string cofig_user = "111";
            string config_pwd = "222";
            if ((in_UserName == cofig_user) && (in_PassWord == config_pwd))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}