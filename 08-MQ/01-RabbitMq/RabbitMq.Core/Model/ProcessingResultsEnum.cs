using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMq.Core.Model
{
    /// <summary>
    /// 数据被执行后的处理方式
    /// </summary>
    public enum ProcessingResultsEnum
    {
        /// <summary>
        /// 处理成功
        /// </summary>
        Accept,

        /// <summary>
        /// 可以重试的错误
        /// </summary>
        Retry,

        /// <summary>
        /// 无需重试的错误
        /// </summary>
        Reject,
    }
}
