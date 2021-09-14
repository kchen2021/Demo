using System;

namespace PollyTest
{
    //https://github.com/App-vNext/Polly
    class Program
    {
        static void Main(string[] args)
        {
            //RetryQuickStart.P00_Handle._domain.Run();
            //RetryQuickStart.P01_RetryTest._domain.Run();
            RetryQuickStart.P01_RetryTest.RetryTest.Run();

            Console.WriteLine("Hello World!");
            Console.Read();
        }
    }
}


//https://www.bbsmax.com/A/Ae5RgmamdQ/