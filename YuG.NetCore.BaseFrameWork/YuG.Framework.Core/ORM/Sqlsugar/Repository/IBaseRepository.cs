using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace YuG.Framework.ORM
{
    /// <summary>
    /// 泛型仓储
    /// </summary>
    public interface IBaseRepository
    {
        #region Add
        /// <summary>
        /// 添加实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<int> AddAsync<T>(T model) where T : BaseEntity, new();

        /// <summary>
        /// 添加实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<int> AddAsync<T>(List<T> list) where T : BaseEntity, new();
        #endregion

        #region Edit
        /// <summary>
        /// 修改实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> EditAsync<T>(T model) where T : BaseEntity, new();

        /// <summary>
        /// 修改实体，指定修改的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        Task<bool> EditAsync<T>(T model, string[] columns) where T : BaseEntity, new();

        /// <summary>
        /// 修改实体，指定修改的字段，并且指定where的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="columns"></param>
        /// <param name="whereColumns"></param>
        /// <returns></returns>
        Task<bool> EditAsync<T>(T model, string[] columns, string[] whereColumns) where T : BaseEntity, new();

        /// <summary>
        /// 修改实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        Task<bool> EditAsync<T>(List<T> list) where T : BaseEntity, new();

        /// <summary>
        /// 修改实体集合，指定修改的字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        Task<bool> EditAsync<T>(List<T> list, string[] columns) where T : BaseEntity, new();

        /// <summary>
        /// 修改实体的状态字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> EditStatusAsync<T>(T model) where T : BaseStatusEntity, new();

        /// <summary>
        /// 修改实体集合的状态字段
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> EditStatusAsync<T>(List<T> list) where T : BaseStatusEntity, new();
        #endregion

        #region Delete

        /// <summary>
        /// 实体逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> EditDelFlagAsync<T>(T model) where T : BaseEntity, new();

        /// <summary>
        /// 实体集合逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> EditDelFlagAsync<T>(List<T> list) where T : BaseEntity, new();

        /// <summary>
        /// 实体物理删除，根据id
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(int id) where T : BaseEntity, new();

        /// <summary>
        /// 实体物理删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(T model) where T : BaseEntity, new();

        /// <summary>
        /// 实体物理删除，根据id集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(object[] id) where T : BaseEntity, new();

        /// <summary>
        /// 实体集合物理删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<bool> DeleteAsync<T>(List<T> list) where T : BaseEntity, new();
        #endregion

        #region Get
        /// <summary>
        /// 查询，不分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<T>> GetAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new();

        /// <summary>
        /// 查询，不分页
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <param name="order"></param>
        /// <returns></returns>
        Task<List<TResult>> GetAsync<T, TResult>(Expression<Func<T, bool>> expression,
                                                 Expression<Func<T, TResult>> select,
                                                 string order = null) where T : BaseEntity, new();

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        Task<PageModel<TResult>> GetPageAsync<T, TResult>(BaseQuery query,
                                                          Expression<Func<T, bool>> expression,
                                                          Expression<Func<T, TResult>> select) where T : BaseEntity, new();

        /// <summary>
        /// 分页查询，查询结果通过AutoMapper映射(需要配置映射关系)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<PageModel<TResult>> GetPageAsync<T, TResult>(BaseQuery query,
                                                          Expression<Func<T, bool>> expression) where T : BaseEntity, new();

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="joinExpression"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        Task<PageModel<TResult>> GetPageJoinAsync<T, T2, TResult>(BaseQuery query,
                                                                  Expression<Func<T, T2, object[]>> joinExpression,
                                                                  Expression<Func<T, T2, bool>> expression,
                                                                  Expression<Func<T, T2, TResult>> select) where T : BaseEntity, new() where T2 : BaseEntity, new();

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="joinExpression"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        Task<PageModel<TResult>> GetPageJoinAsync<T, T2, T3, TResult>(BaseQuery query,
                                                                      Expression<Func<T, T2, T3, object[]>> joinExpression,
                                                                      Expression<Func<T, T2, T3, bool>> expression,
                                                                      Expression<Func<T, T2, T3, TResult>> select) where T : BaseEntity, new() where T2 : BaseEntity, new() where T3 : BaseEntity, new();

        /// <summary>
        /// 多表查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="joinExpression"></param>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        Task<PageModel<TResult>> GetPageJoinAsync<T, T2, T3, T4, TResult>(BaseQuery query,
                                                                          Expression<Func<T, T2, T3, T4, object[]>> joinExpression,
                                                                          Expression<Func<T, T2, T3, T4, bool>> expression,
                                                                          Expression<Func<T, T2, T3, T4, TResult>> select) where T : BaseEntity, new() where T2 : BaseEntity, new() where T3 : BaseEntity, new() where T4 : BaseEntity, new();

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<T> GetFindAsync<T>(Expression<Func<T, bool>> expression) where T : BaseEntity, new();

        /// <summary>
        /// 查询实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="expression"></param>
        /// <param name="select"></param>
        /// <returns></returns>
        Task<TResult> GetFindAsync<T, TResult>(Expression<Func<T, bool>> expression,
                                               Expression<Func<T, TResult>> select) where T : BaseEntity, new();

        /// <summary>
        /// 查询实体(带逻辑删除条件)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        Task<T> GetFindByIDAsync<T>(int id) where T : BaseEntity, new();

        /// <summary>
        /// 多表查询，LeftJoin
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="joinExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="selectExpression"></param>
        /// <returns></returns>
        Task<PageModel<TResult>> GetPageLeftJoinAsync<T, T2, TResult>(BaseQuery query,
                                                                      Expression<Func<T, T2, bool>> joinExpression,
                                                                      Expression<Func<T, T2, bool>> whereExpression,
                                                                      Expression<Func<T, T2, TResult>> selectExpression)
                                                                      where T : BaseEntity, new() where T2 : BaseEntity, new();

        /// <summary>
        /// 多表查询，LeftJoin
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="query"></param>
        /// <param name="joinExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="selectExpression"></param>
        /// <returns></returns>
        Task<PageModel<TResult>> GetPageLeftJoinAsync<T, T2, T3, TResult>(BaseQuery query,
                                                                          Expression<Func<T, T2, bool>> joinExpression,
                                                                          Expression<Func<T, T2, T3, bool>> join2Expression,
                                                                          Expression<Func<T, T2, T3, bool>> whereExpression,
                                                                          Expression<Func<T, T2, T3, TResult>> selectExpression)
                                                                          where T : BaseEntity, new() where T2 : BaseEntity, new() where T3 : BaseEntity, new();
        #endregion

        #region Ado
        /// <summary>
        /// 执行sql查询，返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<List<T>> AdoExecQuery<T>(string sql);

        /// <summary>
        /// 执行sql查询，返回结果集
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<Tuple<List<T>, List<T2>>> AdoExecQuery<T, T2>(string sql);

        /// <summary>
        /// 执行sql查询，返回第一行数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<T> AdoExecQuerySingle<T>(string sql);

        /// <summary>
        /// 执行sql查询，返回DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<DataTable> AdoExecQueryReturnDT(string sql);

        /// <summary>
        /// 执行sql，返回受影响行数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <returns></returns>
        Task<int> AdoExecuteCommand(string sql);
        #endregion
    }
}
