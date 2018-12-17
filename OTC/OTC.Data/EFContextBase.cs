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
{
    public class EFContextBase<TEntity,TKey> : DbContext, IEFContextBase<TEntity,TKey> where TEntity : class, IKey<TKey>
    {
        private DataContext _DbContext;
        /// <summary>
        /// 实体集
        /// </summary>
        protected DbSet<TEntity> _Set;
        public EFContextBase()
        {
            _DbContext = new DataContext();
            _Set = _DbContext.Set<TEntity>();
        }
        /// <summary>
        /// 获取未跟踪查询对象
        /// </summary>
        public IQueryable<TEntity> FindAsNoTracking()
        {
            return _Set.AsNoTracking();
        }

        /// <summary>
        /// 获取查询对象
        /// </summary>
        public IQueryable<TEntity> Find()
        {
            return _Set;
        }
        /// <summary>
        /// 获取未跟踪查询对象
        /// </summary>
        /// <param name="predicate">条件</param>
        public IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAsNoTracking().Where(predicate);
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="predicate">条件</param>
        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _Set.Where(predicate);
        }

        #region FindOne
        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">标识</param>
        public TEntity Find(object id)
        {
            if (string.IsNullOrWhiteSpace(id.TrimString()))
                return null;
            return _Set.Find(id);
        }
       
        /// <summary>
        /// 查找单个实体
        /// </summary>
        /// <param name="predicate">条件</param>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return _Set.FirstOrDefault(predicate);
        }
        /// <summary>
        /// 查找单个实体,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        public TEntity SingleNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return FindAsNoTracking().FirstOrDefault(predicate);
        }

        /// <summary>
        /// 查找实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task<TEntity> FindAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrWhiteSpace(id.TrimString()))
                return null;
            return await _Set.FindAsync(new[] { id });
        }
        #endregion

        #region FindListByPredicate
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="predicate">条件</param>
        public List<TEntity> FindAll(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _Set.ToList();
            return Find(predicate).ToList();
        }

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        public List<TEntity> FindAllNoTracking(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return FindAsNoTracking().ToList();
            return FindAsNoTracking().Where(predicate).ToList();
        }

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="predicate">条件</param>
        public async Task<List<TEntity>> FindAllAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await _Set.ToListAsync();
            return await Find(predicate).ToListAsync();
        }

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="predicate">条件</param>
        public async Task<List<TEntity>> FindAllNoTrackingAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await FindAsNoTracking().ToListAsync();
            return await FindAsNoTracking().Where(predicate).ToListAsync();
        }
        #endregion

        #region Exists And Count
        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        public bool Exists(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return false;
            return _Set.Any(predicate);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="predicate">条件</param>
        public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
        {
            if (predicate == null)
                return false;
            return await _Set.AnyAsync(predicate);
        }

        /// <summary>
        /// 查找数量
        /// </summary>
        /// <param name="predicate">条件</param>
        public int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return _Set.Count();
            return _Set.Count(predicate);
        }

        /// <summary>
        /// 查找数量
        /// </summary>
        /// <param name="predicate">条件</param>
        public async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate = null)
        {
            if (predicate == null)
                return await _Set.CountAsync();
            return await _Set.CountAsync(predicate);
        }
        #endregion

        #region FindListByIds
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIds(params TKey[] ids)
        {
            return FindByIds((IEnumerable<TKey>)ids);
        }

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIds(IEnumerable<TKey> ids)
        {
            if (ids == null)
                return null;
            return Find(t => ((IList)ids).Contains(t.Id)).ToList();
        }

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        public List<TEntity> FindByIds(string ids)
        {
            var idList = ids.ToList<TKey>();
            return FindByIds(idList);
        }

        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task<List<TEntity>> FindByIdsAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (ids == null)
                return null;
            return await Find(t => ((IList)ids).Contains(t.Id)).ToListAsync();
        }
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">标识列表</param>
        public async Task<List<TEntity>> FindByIdsAsync(params TKey[] ids)
        {
            return await FindByIdsAsync((IEnumerable<TKey>)ids);
        }
        
        /// <summary>
        /// 查找实体列表
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        public async Task<List<TEntity>> FindByIdsAsync(string ids)
        {
            var idList = ids.ToList<TKey>();
            return await FindByIdsAsync(idList);
        }

        /// <summary>
        /// 查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        public TEntity FindByIdNoTracking(TKey id)
        {
            var entities = FindByIdsNoTracking(id);
            if (entities == null || entities.Count == 0)
                return null;
            return entities[0];
        }

        /// <summary>
        /// 查找未跟踪单个实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task<TEntity> FindByIdNoTrackingAsync(TKey id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entities = await FindByIdsNoTrackingAsync(id);
            if (entities == null || entities.Count == 0)
                return null;
            return entities[0];
        }
        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIdsNoTracking(IEnumerable<TKey> ids)
        {
            if (ids == null)
                return null;
            return FindAsNoTracking().Where(t => ((IList)ids).Contains(t.Id)).ToList();
        }
        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        public List<TEntity> FindByIdsNoTracking(params TKey[] ids)
        {
            return FindByIdsNoTracking((IEnumerable<TKey>)ids);
        }

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        public List<TEntity> FindByIdsNoTracking(string ids)
        {
            var idList = ids.ToList<TKey>();
            return FindByIdsNoTracking(idList);
        }

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(params TKey[] ids)
        {
            return await FindByIdsNoTrackingAsync((IEnumerable<TKey>)ids);
        }

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">标识列表</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(IEnumerable<TKey> ids, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (ids == null)
                return null;
            return await FindAsNoTracking().Where(t => ((IList)ids).Contains(t.Id)).ToListAsync();
        }

        /// <summary>
        /// 查找实体列表,不跟踪
        /// </summary>
        /// <param name="ids">逗号分隔的标识列表，范例："1,2"</param>
        public async Task<List<TEntity>> FindByIdsNoTrackingAsync(string ids)
        {
            var idList = ids.ToList<TKey>();
            return await FindByIdsNoTrackingAsync(idList);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids">标识列表</param>
        public bool Exists(params TKey[] ids)
        {
            if (ids == null)
                return false;
            return Exists(t => ((IList)ids).Contains(t.Id));
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="ids">标识列表</param>
        public async Task<bool> ExistsAsync(params TKey[] ids)
        {
            if (ids == null)
                return false;
            return await ExistsAsync(t => ((IList)ids).Contains(t.Id));
        }
        #endregion

        #region 分页
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="request">分页参数</param>
        public PagerOut<TEntity> PagerQuery(PagerIn<TEntity, TKey> request)
        {
            PagerOut<TEntity> ret = new PagerOut<TEntity>();
            IQueryable<TEntity> query;

            if (request.IsNoTracking)
            {
                if (request.Predicate == null)
                {
                    query = FindAsNoTracking();
                }
                else
                {
                    query = FindAsNoTracking(request.Predicate);
                }
            }
            else
            {
                if (request.Predicate == null)
                {
                    query = Find();
                }
                else
                {
                    query = Find(request.Predicate);
                }
            }
            ret.TotalCount = query.Count();
            if (request.Order != null)
            {
                if (request.IsDesc)
                {
                    query = query.OrderByDescending(request.Order);
                }
                else
                {
                    query = query.OrderBy(request.Order);
                }
            }
            ret.DataList = query.Skip((request.Page - 1) * request.PageSize).Take(request.PageSize).ToList();
            return ret;
        }
        #endregion

        #region cmd

        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            _Set.Add(entity);
        }
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <param name="entity">实体</param>
        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            await _Set.AddAsync(entity);
        }
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        public void Add(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            _Set.AddRange(entities);//生成多条 insert into ....语句
        }
        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <param name="entities">实体集合</param>
        public async Task AddAsync(IEnumerable<TEntity> entities)
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));
            await _Set.AddRangeAsync(entities);//生成多条 insert into ....语句
        }
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">新实体</param>
        public void Update(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            //var oldEntity = Find(entity.Id);
            //_DbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
            _Set.Update(entity);
        }

        /// <summary>
        /// 修改实体
        /// </summary>
        /// <param name="entity">新实体</param>
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));
            var oldEntity = await FindAsync(entity.Id);
            _DbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">实体标识</param>
        public void Delete(object id)
        {
            var entity = Find(id);
            Delete(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        public void Delete(TEntity entity)
        {
            if (entity == null)
                return;
            _Set.Remove(entity);
        }

        /// <summary>
        /// 移除实体
        /// </summary>
        /// <param name="id">标识</param>
        /// <param name="cancellationToken">取消令牌</param>
        public async Task DeleteAsync(object id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var entity = await FindAsync(id);
            Delete(entity);
        }

        /// <summary>
        /// 删除实体集合
        /// </summary>
        public void Delete(List<TEntity> list)
        {
            if (list == null)
                return;
            if (!list.Any())
                return;
            _Set.RemoveRange(list);//生成多条delete from ... 语句执行的
        }

        /// <summary>
        /// 删除实体集合
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            Delete(Find().Where(predicate).ToList());
        }

        #region 局部修改

        /// <summary>
        /// 局部修改 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isPartContains">是否部分包含</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        protected void EditEntityPart(TEntity newNntity, bool isPartContains, params string[] property)
        {
            var oldEntity = Find(newNntity.Id);
            EntityEntry entry = _DbContext.Entry(oldEntity);
            entry.CurrentValues.SetValues(newNntity);
            entry.State = EntityState.Modified;
            foreach (IProperty p in entry.CurrentValues.Properties)
            {
                if (property.Contains(p.Name))
                {
                    if (!isPartContains)
                    {
                        entry.Property(p.Name).IsModified = false;
                    }
                }
                else
                {
                    if (isPartContains && p.Name.ToLower() != "id")
                    {
                        entry.Property(p.Name).IsModified = false;
                    }
                }
            }
        }

        /// <summary>
        /// 局部修改-部分包含 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        public void EditEntityPartContains(TEntity entity, params string[] property)
        {
            EditEntityPart(entity, true, property);
        }

        /// <summary>
        /// 局部修改-部分排除 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        public void EditEntityPartNotContains(TEntity entity, params string[] property)
        {
            EditEntityPart(entity, false, property);
        }

        /// <summary>
        /// 局部修改 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isPartContains">是否部分包含</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        protected async Task EditEntityPartAsync(TEntity newNntity, bool isPartContains, params string[] property)
        {
            var oldEntity = await FindAsync(newNntity.Id);
            EntityEntry entry = _DbContext.Entry(oldEntity);
            entry.CurrentValues.SetValues(newNntity);
            entry.State = EntityState.Modified;
            foreach (IProperty p in entry.CurrentValues.Properties)
            {
                if (property.Contains(p.Name))
                {
                    if (!isPartContains)
                    {
                        entry.Property(p.Name).IsModified = false;
                    }
                }
                else
                {
                    if (isPartContains && p.Name.ToLower() != "id")
                    {
                        entry.Property(p.Name).IsModified = false;
                    }
                }
            }
        }

        /// <summary>
        /// 局部修改-部分包含 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        public async Task EditEntityPartContainsAsync(TEntity entity, params string[] property)
        {
            await EditEntityPartAsync(entity, true, property);
        }

        /// <summary>
        /// 局部修改-部分排除 
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="property">需要处理的字段</param>
        /// <returns></returns>
        public async Task EditEntityPartNotContainsAsync(TEntity entity, params string[] property)
        {
            await EditEntityPartAsync(entity, false, property);
        }

        #endregion 局部修改

        /// <summary>
        /// 提交,返回影响的行数
        /// </summary>
        public int ContextCommit()
        {
            return _DbContext.SaveChanges();
        }
        /// <summary>
        /// 异步提交,返回影响的行数
        /// </summary>
        public async Task<int> ContextCommitAsync()
        {
            return await _DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 关闭释放
        /// </summary>
        public void ContextDispose()
        {
            _DbContext.Dispose();
        }
        #endregion cmd

        #region 事务处理
        private IDbContextTransaction _dbTransaction;
        /// <summary>
        /// 开启事务
        /// </summary>
        public void TransactionBegin()
        {
            _dbTransaction = _DbContext.Database.BeginTransaction();
        }
        /// <summary>
        /// 回滚
        /// </summary>
        public void TransactionRollback()
        {
            if (_dbTransaction != null)
                _dbTransaction.Rollback();
        }
        /// <summary>
        /// 释放
        /// </summary>
        public void TransactionDispose()
        {
            if (_dbTransaction != null)
                _dbTransaction.Dispose();
        }
        /// <summary>
        /// 提交
        /// </summary>
        public void TransactionCommit()
        {
            if (_dbTransaction != null)
                _dbTransaction.Commit();
        }
        
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
        public int ExecuteSql(string sql, List<MySqlParameter> parms)
        {
            return _DbContext.Database.ExecuteSqlCommand(sql, parms);
        }
        /// <summary>
        /// 执行查询SQL 语句
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="sql">执行的SQL语句</param>
        /// <param name="Parms">SQL 语句的参数</param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public List<TEntity> QueryProcedure(string sql, List<MySqlParameter> parms, CommandType cmdType = CommandType.Text)
        {
            //进行执行存储过程
            if (cmdType == CommandType.StoredProcedure)
            {
                StringBuilder paraNames = new StringBuilder();
                foreach (var item in parms)
                {
                    paraNames.Append($" @{item},");
                }
                //EXECUTE 
                sql = paraNames.Length > 0 ? $"exec {sql} {paraNames.ToString().Trim(',')}" : $"exec {sql} ";
            }
            return _Set.FromSql(sql, parms).ToList();
        }
        #endregion
    }
}
