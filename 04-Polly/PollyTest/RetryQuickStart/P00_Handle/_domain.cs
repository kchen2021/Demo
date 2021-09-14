using System;
using Polly;
using PollyTest.Utils;

namespace PollyTest.RetryQuickStart.P00_Handle
{
    public class _domain
    {
        public static void Run()
        {
            HandleAndOr();
            //HandleResult();
        }

        private static int sign = 0;

        private static void HandleAndOr()
        {
            Policy
                //.Handle<Exception>()
                .Handle<Test1Exception>(ex =>
                {
                    Console.WriteLine($"sign:{sign},process:Test1Exception");

                    return ex.Code == 1;
                })
                .Or<Test2Exception>(ex =>
                {
                    Console.WriteLine($"sign:{sign},process:Test2Exception");

                    //return ex.Code == 1;
                    return false;
                })//运行到结束还是有异常，那么抛出异常
                .Retry(3, (ex, index)=> 
                {
                    Console.WriteLine($"msg:{ex.Message}, index:{index}");
                })
                .Execute(() =>
                {
                    Console.WriteLine(++sign);
                    if (sign <= 2)
                    {
                        throw new Test1Exception(1);
                    }
                    else if (sign <= 4)
                    {
                        throw new Test2Exception(1);
                    }

                    Console.WriteLine($"success! sign:{sign}");
                });
        }

        private static void HandleResult()
        {
            var ret = Polly.Policy.Handle<Exception>()
                .OrResult<int>(result=>//如果条件成真，那么继续运行到结束，不抛出异常
                {
                    return result > 1;
                })
                .Retry(5)
                .Execute(() =>
                {
                    Console.WriteLine(++sign);
                    if (sign <= 1)
                    {
                        throw new Test1Exception(1);
                    }
                    else if (sign <= 2)
                    {
                        throw new Test2Exception(1);
                    }

                    Console.WriteLine($"success! sign:{sign}");
                    return sign;
                });
        }
    }
}