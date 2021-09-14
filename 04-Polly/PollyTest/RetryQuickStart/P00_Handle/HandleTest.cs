using System;
using Polly;

namespace PollyTest.RetryQuickStart.P00_Handle
{
    public class HandleTest
    {
        private static int sign = 0;
        public static void SingleExceptionType()
        {
            // Single exception type
            //var policyBuilder = Policy.Handle<Exception>();

            // Single exception type with condition
            //Policy
            //    .Handle<SqlException>(ex => ex.Number == 1205);

            //// Multiple exception types
            //Policy
            //    .Handle<HttpRequestException>()
            //    .Or<OperationCanceledException>();

            //// Multiple exception types with condition
            //Policy
            //    .Handle<SqlException>(ex => ex.Number == 1205)
            //    .Or<ArgumentException>(ex => ex.ParamName == "example");

            //// Inner exceptions of ordinary exceptions or AggregateException, with or without conditions
            //// (HandleInner matches exceptions at both the top-level and inner exceptions)
            //Policy
            //    .HandleInner<HttpRequestException>()
            //    .OrInner<OperationCanceledException>(ex => ex.CancellationToken != myToken);

            Policy.Handle<Exception>()
                .Retry(3)
                .Execute(() =>
                {
                    if (sign == 0)
                    {
                        sign++;
                        Console.WriteLine("throw");
                        throw new ArgumentException("aaaa");
                    }
                    Console.WriteLine("11111");
                });


            //var ret = Policy.HandleResult<string>(r=>string.IsNullOrEmpty(r))
            //    .Retry(3)
            //    .Execute(() =>
            //    {
            //        Console.WriteLine(sign + "");
            //        if (sign == 0)
            //        {
            //            sign++;

            //            return "";
            //        }
            //        return sign+"";
            //    });

            //Console.WriteLine(ret);

        }
    }
}
