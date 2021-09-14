using System;

namespace C02_RemoteDemo.Common.Objects
{
    public class Hello
    {
        public Hello(string message)
        {
            Message = message;
        }

        public string Message { get; private set; }
    }
}
