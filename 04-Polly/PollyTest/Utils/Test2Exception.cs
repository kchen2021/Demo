using System;

namespace PollyTest.Utils
{
    public class Test2Exception:Exception
    {
        public Test2Exception(int code) : base("Test1Exception")
        {
            Code = code;
        }
        public int Code { get; set; }
    }
}
