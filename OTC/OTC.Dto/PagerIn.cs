using System;
using System.Linq.Expressions;

namespace OTC.Dto
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class PagerIn<TEntity, TKey>
    {
        public PagerIn()
        {
        }

        public PagerIn(int _PageIndex, int _PageSize)
        {
            PageIndex = _PageIndex;
            PageSize = _PageSize;
        }
        /// <summary>
        /// 页索引，即第几页，从1开始
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页索引，即第几页，从1开始
        /// </summary>
        public int Page { get { return PageIndex < 1 ? 1 : PageIndex; } }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 条件
        /// </summary>
        public Expression<Func<TEntity, bool>> Predicate { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public Expression<Func<TEntity, TKey>> Order { get; set; }

        /// <summary>
        /// 排序-是否降序
        /// </summary>
        public bool IsDesc { get; set; }

        /// <summary>
        /// 是否进行无跟踪查询
        /// </summary>
        public bool IsNoTracking { get; set; }
    }
}
