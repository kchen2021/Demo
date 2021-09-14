using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NancyConsole
{
    public enum EErrorCode
    {
        OK,
        Error,
    }
    public class ApiResult
    {
        int _ResultCode = (int)EErrorCode.OK;

        public int ResultCode
        {
            get { return _ResultCode; }
            set { _ResultCode = value; }
        }


        string _Message = "";

        public string Message
        {
            get { return _Message; }
            set { _Message = value; }
        }
    }

    public class ApiResultWithData<T> : ApiResult
    {
        T _Data = default(T);

        public T Data
        {
            get { return _Data; }
            set { _Data = value; }
        }
    }
}
