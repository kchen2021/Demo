using System;

namespace PollyTest.Utils
{
    public class Test1Exception:Exception
    {
        public Test1Exception(int code) : base("Test1Exception")
        {
            Code = code;
        }
        public int Code { get; set; }
    }
}
