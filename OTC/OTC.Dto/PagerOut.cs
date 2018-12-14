using System;
using System.Collections.Generic;
using System.Linq;

namespace OTC.Dto
{
    /// <summary>
    /// 分页返回
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class PagerOut<TEntity>
    {
        public PagerOut()
        {
        }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        ///// <summary>
        ///// 总页数
        ///// </summary>
        //public int PageCount { get { return (TotalCount % pageSize == 0 ? TotalCount / pageSize : TotalCount / pageSize + 1; } }

        /// <summary>
        /// 内容
        /// </summary>
        public List<TEntity> DataList { get; set; }
        ///// <summary>
        ///// 内容
        ///// </summary>
        //public IQueryable<TEntity> Data { get; set; }
        ///// <summary>
        ///// 内容
        ///// </summary>
        //public List<TEntity> DataList { get { return Data.ToList(); } }
    }
}
