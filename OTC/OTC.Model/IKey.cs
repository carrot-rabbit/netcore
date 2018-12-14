using System;
using System.Collections.Generic;
using System.Text;

namespace OTC.Data
{
    /// <summary>
    /// 主键
    /// </summary>
    /// <typeparam name="TKey">主键类型</typeparam>
    public interface IKey<out TKey>
    {
        /// <summary>
        /// 主键
        /// </summary>
        TKey Id { get; }
    }
}
