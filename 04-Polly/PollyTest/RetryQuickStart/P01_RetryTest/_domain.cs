using System;
using System.Threading;
using Polly;
using Polly.CircuitBreaker;
using Polly.Wrap;
using PollyTest.Utils;

namespace PollyTest.RetryQuickStart.P01_RetryTest
{
    public class _domain
    {
        #region Cto.

        private static int sign = 0;

        private static PolicyWrap<string> _policyWrap;

        static _domain()
        {
            InitWrap();
        }

        //Circuit:[电子] 电路，回路；巡回；一圈；环道
        private static void InitWrap()
        {
            //超时
            var timeout = Policy
                .Timeout(1, Polly.Timeout.TimeoutStrategy.Pessimistic, (context, ts, task) =>
                {
                    //超时执行的方法
                    Console.WriteLine("timeout");
                });
            //熔断
            var breaker = Policy
                .Handle<Exception>()//exception 出现后熔断并降级
                .CircuitBreaker(1,  //允许异常的次数
                    TimeSpan.FromSeconds(1),   //在单位时间内未抛出异常
                    (ex, ts) =>
                    {
                        //熔断执行的方法
                        Console.WriteLine("OnBreaker");
                    },
                    () =>
                    {
                        //熔断状态重置，不使用降级的方法而使用excute的结果
                        Console.WriteLine("OnReset");
                    });

            //重试
            var retry = Policy.Handle<Exception>().Retry(2);//在重试3次后降级

            //Fallback + 熔断 + 超时
            _policyWrap = Policy<string>.Handle<Exception>().Fallback(GetFallback(), delegateResult =>
                    {
                        //使用了降级方法
                        Console.WriteLine("delegateResult");
                    })
                    .Wrap(breaker)
                //.Wrap(timeout)
                //.Wrap(retry)
                ;
        }

        #endregion

        public static void Run()
        {
            //Retry();
            CircuitBreaker();
        }
        
        #region Retry

        public static void Retry()
        {
            Policy.Handle<Exception>()
                .Retry(5, (exception, i, context) =>
                {
                    Console.WriteLine(i);
                })
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

                    Console.WriteLine($"sign:{sign}");
                });
        }

        #endregion

        #region Circuit Breaker

        public static void CircuitBreaker()
        {
            var ret = _policyWrap.Execute(() =>
            {
                Console.WriteLine($"sign:{++sign}");
                //Thread.Sleep(2000);
                throw  new Test1Exception(11);

                return "normal";
            });
            Console.WriteLine($"result:{ret}");
        }

        //降级处理
        private static string GetFallback()
        {
            return "fallback";
        }

        #endregion
    }
}