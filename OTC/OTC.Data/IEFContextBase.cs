using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using OTC.CommonHelper.StringUtil;
using OTC.Dto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace OTC.Data
{//:IKey<int>
    public interface IEFContextBase<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
        /// <summary>
        /// 获取未跟踪查询对象
        /// </summary>
        IQueryable<TEntity> FindAsNoTracking();

        /// <summary>
        /// 获取查询对象
        /// </summary>
        IQueryable<TEntity> Find();
        /// <summary>
        /// 获取未跟踪查询对象
        /// </summary>
        /// <param name="predicate">条件</param>
        IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate">条件</param>
        IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        #region FindOne
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">标识</param>
        TEntity Find(object id);

        /// <summary>
        /// 查找单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        TEntity Single(Expression<Func<TEntity, bool>> predicate);
        /// <summary>
        /// 查找单个实体,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        TEntity SingleNoTracking(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task<TEntity> FindAsync(object id, CancellationToken cancellationToken = default(CancellationToken));
        #endregion

        #region FindListByPredicate
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="predicate">条件</param>
        List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        List<TEntity> FindAllNoTracking(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="predicate">条件</param>
        Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        Task<List<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region Exists And Count
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        bool Exists(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// 查找数量
        /// </summary>
        /// <param name="predicate">条件</param>
        int Count(Expression<Func<TEntity, bool>> predicate = null);

        /// <summary>
        /// 查找数量
        /// </summary>
        /// <param name="predicate">条件</param>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null);
        #endregion

        #region FindListByIds
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        List<TEntity> FindByIds(params TKey[] ids);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        List<TEntity> FindByIds(IEnumerable<TKey> ids);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        List<TEntity> FindByIds(string ids);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task<List<TEntity>> FindByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task<List<TEntity>> FindByIdsAsync(params TKey[] ids);

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        Task<List<TEntity>> FindByIdsAsync(string ids);

        /// <summary>
        /// 查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        TEntity FindByIdNoTracking(TKey id);

        /// <summary>
        /// 查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task<TEntity> FindByIdNoTrackingAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken));
        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        List<TEntity> FindByIdsNoTracking(IEnumerable<TKey> ids);
        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        List<TEntity> FindByIdsNoTracking(params TKey[] ids);

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        List<TEntity> FindByIdsNoTracking(string ids);

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync(params TKey[] ids);

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        Task<List<TEntity>> FindByIdsNoTrackingAsync(string ids);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids">标识列表</param>
        bool Exists(params TKey[] ids);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids">标识列表</param>
        Task<bool> ExistsAsync(params TKey[] ids);
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request">分页参数</param>
        PagerOut<TEntity> PagerQuery(PagerIn<TEntity, TKey> request);
        #endregion

        #region cmd

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        void Add(TEntity entity);
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        Task AddAsync(TEntity entity);
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        void Add(IEnumerable<TEntity> entities);
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        Task AddAsync(IEnumerable<TEntity> entities);
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">新实体</param>
        void Update(TEntity entity);

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">新实体</param>
        Task UpdateAsync(TEntity entity);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        void Delete(object id);

        /// <summary>
        /// 删除
        /// </summary>
        void Delete(TEntity entity);

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        Task DeleteAsync(object id, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 删除实体集合
        /// </summary>
        void Delete(List<TEntity> list);

        /// <summary>
        /// 删除实体集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        void Delete(Expression<Func<TEntity, bool>> predicate);

        #region 局部修改

        /// <summary>
        /// 局部修改-部分包含 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        void EditEntityPartContains(TEntity entity, params string[] property);

        /// <summary>
        /// 局部修改-部分排除 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        void EditEntityPartNotContains(TEntity entity, params string[] property);

        /// <summary>
        /// 局部修改-部分包含 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        Task EditEntityPartContainsAsync(TEntity entity, params string[] property);

        /// <summary>
        /// 局部修改-部分排除 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        Task EditEntityPartNotContainsAsync(TEntity entity, params string[] property);

        #endregion 局部修改

        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        int ContextCommit();
        /// <summary>
        /// 异步提交,返回影响的行数
        /// </summary>
        Task<int> ContextCommitAsync();
        /// <summary>
        /// 关闭释放
        /// </summary>
        void ContextDispose();
        #endregion cmd

        #region 事务处理
        /// <summary>
        /// 开启事务
        /// </summary>
        void TransactionBegin();
        /// <summary>
        /// 回滚
        /// </summary>
        void TransactionRollback();
        /// <summary>
        /// 释放
        /// </summary>
        void TransactionDispose();
        /// <summary>
        /// 提交
        /// </summary>
        void TransactionCommit();
        #endregion 事务处理

        #region 执行SQL
        /// <summary>
        /// 执行 SQL 语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="Parms">SQL 语句的参数</param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        int ExecuteSql(string sql, List<MySqlParameter> parms);
        /// <summary>
        /// 执行查询SQL 语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="Parms">SQL 语句的参数</param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        List<TEntity> QueryProcedure(string sql, List<MySqlParameter> parms, CommandType cmdType = CommandType.Text);
        #endregion
    }
}
