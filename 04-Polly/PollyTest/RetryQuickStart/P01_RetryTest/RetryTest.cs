using System;
using System.Linq;
using Polly;

namespace PollyTest.RetryQuickStart.P01_RetryTest
{
    public class RetryTest
    {
        private static int _sign;

        public static void Run()
        {
            Retry();
            //RetryForHandleResult();
            //RetryForException();
        }


        #region Retry for handleResult

        public static void Retry()
        {
            Policy.Handle<Exception>()
                .WaitAndRetry(Enumerable.Repeat(TimeSpan.FromMilliseconds(1), 3))
                .Execute(()=> {
                    Console.WriteLine("111111111111111111");
                    throw new Exception("123");
                });
        }

        public static void RetryForHandleResult()
        {
            var ret = Policy
                .HandleResult<string>(string.IsNullOrEmpty) // 如果是这样的，那就不是我们想要的。
                .Retry(3)
                .Execute(() =>
                {
                    Console.WriteLine(_sign);
                    if (_sign == 0)
                    {
                        _sign++;
                        return "";
                    }

                    return _sign + "";
                });

            Console.WriteLine();
            Console.WriteLine(ret);
        }

        public static void RetryForException()
        {
            var ret = Policy.Handle<Exception>()
                .Retry(3, (exception, retryCount, context) =>//context记录的是当前polly实例的上下文信息
                {
                    Console.WriteLine($"{exception.Message}--{retryCount}");
                })
                .Execute(() =>
                {
                    Console.WriteLine(_sign);
                    //if (_sign == 0)
                    {
                        _sign++;
                        throw  new Exception("_sign error!--" + _sign);
                    }

                    return _sign + "";
                });


            Console.WriteLine();
            Console.WriteLine(ret);
        }
        #endregion

    }
}