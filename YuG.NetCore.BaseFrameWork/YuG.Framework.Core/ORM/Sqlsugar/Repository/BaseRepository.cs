using AutoMapper;
using YuG.Framework.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace YuG.Framework.ORM
{
    public class BaseRepository : IBaseRepository
    {
        #region 字段
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        protected SqlSugarScope _dbBase;

        protected ISqlSugarClient _db
        {
            get
            {
                return _dbBase;
            }
        }

        internal ISqlSugarClient Db
        {
            get { return _db; }
        }

        public BaseRepository(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _dbBase = unitOfWork.GetDbClient();
        }
        #endregion

        #region Add
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddAsync<T>(T model) where T : BaseEntity, new()
        {
            var insert = _db.Insertable(model);
            return await insert.ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<int> AddAsync<T>(List<T> list) where T : BaseEntity, new()
        {
            var insert = _db.Insertable(list);
            return await insert.ExecuteCommandAsync();
        }

        #endregion

        #region Edit
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<T>(T model) where T : BaseEntity, new()
        {
            return await _db.Updateable(model).ExecuteCommandHasChangeAsync();
        }

        /// <summary>
        /// 修改实体，指定修改的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<T>(T model, string[] columns) where T : BaseEntity, new()
        {
            return await _db.Updateable(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改实体，指定修改的字段，并且指定where的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="columns"></param>
        /// <param name="whereColumns"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<T>(T model, string[] columns, string[] whereColumns) where T : BaseEntity, new()
        {
            return await _db.Updateable(model).WhereColumns(whereColumns).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<T>(List<T> list) where T : BaseEntity, new()
        {
            return await _db.Updateable(list).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改实体集合，指定修改的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public async Task<bool> EditAsync<T>(List<T> list, string[] columns) where T : BaseEntity, new()
        {
            return await _db.Updateable(list).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改实体的状态字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditStatusAsync<T>(T model) where T : BaseStatusEntity, new()
        {
            string[] columns = GetTableFieldName<T>(new[] { nameof(model.Status),
                                                            nameof(model.UpdateTime),
                                                            nameof(model.UpdateUser) });
            return await _db.Updateable(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 修改实体集合的状态字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> EditStatusAsync<T>(List<T> list) where T : BaseStatusEntity, new()
        {
            var model = list.First();
            string[] columns = GetTableFieldName<T>(new[] { nameof(model.Status),
                                                            nameof(model.UpdateTime),
                                                            nameof(model.UpdateUser) });
            return await _db.Updateable(list).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }
        #endregion

        #region Delete
        /// <summary>
        /// 实体逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> EditDelFlagAsync<T>(T model) where T : BaseEntity, new()
        {
            model.DelFlag = DbEnum.DelFlag.Delete;
            string[] columns = GetTableFieldName<T>(new[] { nameof(model.DelFlag),
                                                            nameof(model.UpdateTime),
                                                            nameof(model.UpdateUser) });
            return await _db.Updateable(model).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 实体集合逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> EditDelFlagAsync<T>(List<T> list) where T : BaseEntity, new()
        {
            for (int i = 0; i < list.Count; i++)
            {
                var model = list[i];
                model.DelFlag = DbEnum.DelFlag.Delete;
            }
            var type = list.First();
            string[] columns = GetTableFieldName<T>(new[] { nameof(type.DelFlag),
                                                            nameof(type.UpdateTime),
                                                            nameof(type.UpdateUser) });
            return await _db.Updateable(list).UpdateColumns(columns).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 实体物理删除，根据id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<T>(int id) where T : BaseEntity, new()
        {
            return await _db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 实体物理删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<T>(T model) where T : BaseEntity, new()
        {
            return await _db.Deleteable(model).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 实体物理删除，根据id集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<T>(object[] id) where T : BaseEntity, new()
        {
            return await _db.Deleteable<T>().In(id).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 实体集合物理删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync<T>(List<T> list) where T : BaseEntity, new()
        {
            return await _db.Deleteable(list).ExecuteCommandAsync() > 0;
        }
        #endregion

        #region Get
        /// <summary>
        /// 查询，不分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        public async Task<List<TResult>> GetAsync<T, TResult>(Expression<Func<T, bool>> expression,
                                                              Expression<Func<T, TResult>> select,
                                                              string order = null)
            where T : BaseEntity, new()
        {
            return await _db.Queryable<T>()
                            .WhereIF(expression is not null, expression)
                            .Select(select)
                            .OrderByIF(order is not null, order)
                            .ToListAsync();
        }

        /// <summary>
        /// 查询，不分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression)
            where T : BaseEntity, new()
        {
            return await _db.Queryable<T>()
                            .WhereIF(expression is not null, expression)
                            .ToListAsync();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<T> GetFindAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new()
        {
            return await _db.Queryable<T>().WhereIF(expression is not null, expression).FirstAsync();
        }

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public async Task<TResult> GetFindAsync<T, TResult>(Expression<Func<T, bool>> expression,
                                                            Expression<Func<T, TResult>> select) where T : BaseEntity, new()
        {
            return await _db.Queryable<T>().WhereIF(expression is not null, expression).Select(select).FirstAsync();
        }

        /// <summary>
        /// 查询实体(带逻辑删除条件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public async Task<T> GetFindByIDAsync<T>(int id) where T : BaseEntity, new()
        {
            return await _db.Queryable<T>().Where(e => e.ID == id && e.DelFlag == DbEnum.DelFlag.UnDelete).FirstAsync();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public async Task<PageModel<TResult>> GetPageAsync<T, TResult>(BaseQuery query,
                                                                       Expression<Func<T, bool>> expression,
                                                                       Expression<Func<T, TResult>> select)
            where T : BaseEntity, new()
        {
            string order = $" {query.Order} {query.Desc} ";
            RefAsync<int> total = 0;
            var data = await _db.Queryable<T>()
                                .WhereIF(expression is not null, expression)
                                .Select(select)
                                .OrderByIF(!string.IsNullOrWhiteSpace(query.Order), order)
                                .ToPageListAsync(query.PageNum, query.PageSize, total);

            return new PageModel<TResult>
            {
                PageNum = query.PageNum,
                PageSize = query.PageSize,
                DataCount = total,
                Data = data
            };
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<PageModel<TResult>> GetPageAsync<T, TResult>(BaseQuery query,
                                                                       Expression<Func<T, bool>> expression)
            where T : BaseEntity, new()
        {
            string order = $" {query.Order} {query.Desc} ";
            RefAsync<int> total = 0;
            var data = await _db.Queryable<T>()
                                .WhereIF(expression is not null, expression)
                                .OrderByIF(!string.IsNullOrWhiteSpace(query.Order), order)
                                .ToPageListAsync(query.PageNum, query.PageSize, total);

            var res = new List<TResult>();
            _mapper.Map(data, res);

            return new PageModel<TResult>
            {
                PageNum = query.PageNum,
                PageSize = query.PageSize,
                DataCount = total,
                Data = res
            };
        }

        /// <summary>
        /// 多表分页查询(两表)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public async Task<PageModel<TResult>> GetPageJoinAsync<T, T2, TResult>(BaseQuery query,
                                                                               Expression<Func<T, T2, object[]>> joinExpression,
                                                                               Expression<Func<T, T2, bool>> expression,
                                                                               Expression<Func<T, T2, TResult>> select)
            where T : BaseEntity, new()
            where T2 : BaseEntity, new()
        {
            string order = $" {query.Order} {query.Desc} ";
            RefAsync<int> total = 0;
            var data = await _db.Queryable(joinExpression)
                                .WhereIF(expression is not null, expression)
                                .Select(select)
                                .OrderByIF(!string.IsNullOrWhiteSpace(query.Order), order)
                                .ToPageListAsync(query.PageNum, query.PageSize, total);

            return new PageModel<TResult>
            {
                PageNum = query.PageNum,
                PageSize = query.PageSize,
                DataCount = total,
                Data = data
            };
        }

        /// <summary>
        /// 多表分页查询(三表)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public async Task<PageModel<TResult>> GetPageJoinAsync<T, T2, T3, TResult>(BaseQuery query,
                                                                                   Expression<Func<T, T2, T3, object[]>> joinExpression,
                                                                                   Expression<Func<T, T2, T3, bool>> expression,
                                                                                   Expression<Func<T, T2, T3, TResult>> select)
            where T : BaseEntity, new()
            where T2 : BaseEntity, new()
            where T3 : BaseEntity, new()
        {
            string order = $" {query.Order} {query.Desc} ";
            RefAsync<int> total = 0;
            var data = await _db.Queryable(joinExpression)
                                .WhereIF(expression is not null, expression)
                                .Select(select)
                                .OrderByIF(!string.IsNullOrWhiteSpace(query.Order), order)
                                .ToPageListAsync(query.PageNum, query.PageSize, total);

            return new PageModel<TResult>
            {
                PageNum = query.PageNum,
                PageSize = query.PageSize,
                DataCount = total,
                Data = data
            };
        }

        /// <summary>
        /// 多表分页查询(四表)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        public async Task<PageModel<TResult>> GetPageJoinAsync<T, T2, T3, T4, TResult>(BaseQuery query,
                                                                                       Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
                                                                                       Expression<Func<T, T2, T3, T4, bool>> expression,
                                                                                       Expression<Func<T, T2, T3, T4, TResult>> select)
            where T : BaseEntity, new()
            where T2 : BaseEntity, new()
            where T3 : BaseEntity, new()
            where T4 : BaseEntity, new()
        {
            string order = $" {query.Order} {query.Desc} ";
            RefAsync<int> total = 0;
            var data = await _db.Queryable(joinExpression)
                                .WhereIF(expression is not null, expression)
                                .Select(select)
                                .OrderByIF(!string.IsNullOrWhiteSpace(query.Order), order)
                                .ToPageListAsync(query.PageNum, query.PageSize, total);

            return new PageModel<TResult>
            {
                PageNum = query.PageNum,
                PageSize = query.PageSize,
                DataCount = total,
                Data = data
            };
        }

        /// <summary>
        /// 多表查询(Left Join)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public async Task<PageModel<TResult>> GetPageLeftJoinAsync<T, T2, TResult>(BaseQuery query,
                                                                                   Expression<Func<T, T2, bool>> joinExpression,
                                                                                   Expression<Func<T, T2, bool>> whereExpression,
                                                                                   Expression<Func<T, T2, TResult>> selectExpression)
                                                                                   where T : BaseEntity, new() where T2 : BaseEntity, new()
        {
            string order = $" {query.Order} {query.Desc} ";
            RefAsync<int> total = 0;
            var data = await _db.Queryable<T>()
                                .LeftJoin<T2>(joinExpression)
                                .WhereIF(whereExpression is not null, whereExpression)
                                .Select(selectExpression)
                                .OrderByIF(!string.IsNullOrWhiteSpace(query.Order), order)
                                .ToPageListAsync(query.PageNum, query.PageSize, total);

            return new PageModel<TResult>
            {
                PageNum = query.PageNum,
                PageSize = query.PageSize,
                DataCount = total,
                Data = data
            };
        }

        /// <summary>
        /// 多表查询(Left Join)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="joinExpression"></param>
        /// <param name="join2Expression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="selectExpression"></param>
        /// <returns></returns>
        public async Task<PageModel<TResult>> GetPageLeftJoinAsync<T, T2, T3, TResult>(BaseQuery query,
                                                                                       Expression<Func<T, T2, bool>> joinExpression,
                                                                                       Expression<Func<T, T2, T3, bool>> join2Expression,
                                                                                       Expression<Func<T, T2, T3, bool>> whereExpression,
                                                                                       Expression<Func<T, T2, T3, TResult>> selectExpression)
                                                                                       where T : BaseEntity, new() where T2 : BaseEntity, new() where T3 : BaseEntity, new()
        {
            string order = $" {query.Order} {query.Desc} ";
            RefAsync<int> total = 0;
            var data = await _db.Queryable<T>()
                                .LeftJoin<T2>(joinExpression)
                                .LeftJoin<T3>(join2Expression)
                                .WhereIF(whereExpression is not null, whereExpression)
                                .Select(selectExpression)
                                .OrderByIF(!string.IsNullOrWhiteSpace(query.Order), order)
                                .ToPageListAsync(query.PageNum, query.PageSize, total);

            return new PageModel<TResult>
            {
                PageNum = query.PageNum,
                PageSize = query.PageSize,
                DataCount = total,
                Data = data
            };
        }
        #endregion

        #region Ado
        /// <summary>
        /// 执行sql查询，返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<List<T>> AdoExecQuery<T>(string sql)
        {
            return await _db.Ado.SqlQueryAsync<T>(sql);
        }

        /// <summary>
        /// 执行sql查询，返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<Tuple<List<T>, List<T2>>> AdoExecQuery<T, T2>(string sql)
        {
            return await _db.Ado.SqlQueryAsync<T, T2>(sql);
        }

        /// <summary>
        /// 执行sql查询，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<T> AdoExecQuerySingle<T>(string sql)
        {
            return await _db.Ado.SqlQuerySingleAsync<T>(sql);
        }

        /// <summary>
        /// 执行sql查询，返回DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<DataTable> AdoExecQueryReturnDT(string sql)
        {
            return await _db.Ado.GetDataTableAsync(sql);
        }

        /// <summary>
        /// 执行sql，返回受影响行数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        public async Task<int> AdoExecuteCommand(string sql)
        {
            return await _db.Ado.ExecuteCommandAsync(sql);
        }
        #endregion

        /// <summary>
        /// 获取属性对应的字段名称(如果字段没有特性标注字段名，那么返回属性名)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private string[] GetTableFieldName<T>(string[] propertyName)
        {
            var property = typeof(T).GetProperties().Where(e => propertyName.Contains(e.Name)).ToList();
            var arr = new string[propertyName.Length];
            for (int i = 0; i < property.Count; i++)
            {
                var attribute = property[i].GetCustomAttribute(typeof(SugarColumn)) as SugarColumn;
                if (attribute is not null)
                {
                    arr[i] = attribute.ColumnName;
                }
                arr[i] = property[i].Name;
            }
            return arr;
        }
    }
}
