using IChipo.YJH.Model;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace IChipo.YJH.Server
{
    public class BaseService<T>
    {
        //接口地址https://github.com/ServiceStack/ServiceStack.OrmLite 
        private static readonly string connString = System.Configuration.ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        public readonly OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connString, MySqlDialect.Provider);

        public void InsertFilter()
        {
            OrmLiteConfig.InsertFilter = (dbCmd, row) =>
            {
                var auditRow = row as IBaseModel;
                if (auditRow != null)
                {
                    auditRow.CreateTime = DateTime.Now;
                }

            };
        }
        /// <summary>
        /// 软删除过滤
        /// </summary>
        public void RecycleFilter()
        {
            OrmLiteConfig.UpdateFilter = (dbCmd, row) =>
            {
                var auditRow = row as IBaseModel;
                if (auditRow != null)
                {
                    // auditRow.DeletionStateCode = 1;
                }

            };
        }


        public void UpdateFilter()
        {
            OrmLiteConfig.UpdateFilter = (dbCmd, row) =>
            {
                var auditRow = row as IBaseModel;
                if (auditRow != null)
                {
                    //  auditRow.ModifiedOn = DateTime.Now;
                }

            };
        }
        public void SqlExpressionSelectFilter()
        {
            OrmLiteConfig.SqlExpressionSelectFilter = q =>
            {
                if (q.ModelDef.ModelType.HasInterface(typeof(IBaseModel)))
                {
                    // q.Where<IBaseModel>(x => x.DeletionStateCode == 0);
                }
            };
        }


        /// <summary>
        /// 基本插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Insert(T model)
        {
            InsertFilter();
            using (var db = dbFactory.Open())
            {
                return db.Insert<T>(model);
            }
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> InsertAsync(T model)
        {
            InsertFilter();
            using (var db = dbFactory.Open())
            {
                return await db.InsertAsync<T>(model);
            }


        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByEnum"></param>
        /// <returns></returns>
        public List<T> Query(Expression<Func<T, bool>> expression, string orderBy = "Id", OrderByEnum orderByEnum = OrderByEnum.Default)
        {
            //SqlExpressionSelectFilter();
            using (var db = dbFactory.Open())
            {

                if (orderByEnum == OrderByEnum.Desc)
                {
                    var where = db.From<T>().Where(expression).OrderByDescending(orderBy);
                    return db.Select<T>(where);
                }
                else if (orderByEnum == OrderByEnum.Asc)
                {
                    var where = db.From<T>().Where(expression).OrderBy(orderBy);
                    return db.Select<T>(where);
                }
                return db.Select<T>(expression);
            }
        }

        /// <summary>
        /// 根据Id 获取结果
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryByIdsAsync(IEnumerable<int> ids)
        {
            //SqlExpressionSelectFilter();
            using (var db = dbFactory.Open())
            {
                return await db.SelectByIdsAsync<T>(ids);
            }
        }



        public long Count(Expression<Func<T, bool>> expression)
        {
            // SqlExpressionSelectFilter();
            using (var db = dbFactory.Open())
            {
                return db.Count<T>(expression);

            }

        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public List<T> PageList(Expression<Func<T, bool>> expression, int pageIndex, int pageSize, string OrderBy = "Id")
        {
            SqlExpressionSelectFilter();
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            //source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            using (var db = dbFactory.Open())
            {
                var where = db.From<T>().Where(expression).OrderByFieldsDescending(OrderBy).Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(where);
            }
        }
        /// <summary>
        /// 获取分页总条数
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<long> PageCountAsync(Expression<Func<T, bool>> expression)
        {
            SqlExpressionSelectFilter();
            using (var db = dbFactory.Open())
            {
                var q = db.From<T>().Where(expression).Select(Sql.Count("*"));
                return await db.ScalarAsync<long>(q);
            }
        }

        /// <summary>
        /// 异步分页查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        public async Task<List<T>> PageListAsync(Expression<Func<T, bool>> expression, int pageIndex, int pageSize, string OrderBy = "Id")
        {
            SqlExpressionSelectFilter();
            pageIndex = pageIndex == 0 ? 1 : pageIndex;
            using (var db = dbFactory.Open())
            {
                var where = db.From<T>().Where(expression).OrderByFieldsDescending(OrderBy).Limit((pageIndex - 1) * pageSize, pageSize);

                var result = await db.SelectAsync(where); 
                return result;
            }


        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public bool InsertAll(IEnumerable<T> models)
        {
            InsertFilter();
            using (var db = dbFactory.Open())
            {
                db.InsertAll<T>(models);
                return true;
            }
        }

        /// <summary>
        /// 批量插入
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public async Task<bool> InsertAllAsync(IEnumerable<T> models)
        {
            InsertFilter();
            using (var db = dbFactory.Open())
            {
                await db.InsertAllAsync<T>(models);
                return true;
            }
        }
        /// <summary>
        /// 通用修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long Update(T model, Expression<Func<T, bool>> expression)
        {
            UpdateFilter();
            using (var db = dbFactory.Open())
            {
                return db.Update<T>(model, expression);
            }
        }

        /// <summary>
        /// 通用修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> UpdateAsync(T model, Expression<Func<T, bool>> expression)
        {
            UpdateFilter();
            using (var db = dbFactory.Open())
            {
                return await db.UpdateAsync<T>(model, expression);
            }
        }


        /// <summary>
        /// 通用修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long UpdateOnly(Expression<Func<T>> updateFields, Expression<Func<T, bool>> where = null)
        {
            UpdateFilter();
            using (var db = dbFactory.Open())
            {
                return db.UpdateOnly<T>(updateFields, where);
            }

        }


        /// <summary>
        /// 通用修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<long> UpdateOnlyAsync(Expression<Func<T>> updateFields, Expression<Func<T, bool>> where = null)
        {
            UpdateFilter();
            using (var db = dbFactory.Open())
            {
                return await db.UpdateOnlyAsync<T>(updateFields, where);
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="where">删除条件</param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(Expression<Func<T, bool>> where)
        {
            using (var db = dbFactory.Open())
            {
                return await db.DeleteAsync<T>(where);
            }
        }
        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public async Task<T> SingleAsync(Expression<Func<T, bool>> expression)
        {
            //SqlExpressionSelectFilter();
            using (var db = dbFactory.Open())
            {
                return await db.SingleAsync<T>(expression);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderByEnum"></param>
        /// <returns></returns>
        public async Task<List<T>> QueryAsync(Expression<Func<T, bool>> expression, string orderBy = "Id", OrderByEnum orderByEnum = OrderByEnum.Default)
        {
            //SqlExpressionSelectFilter();
            using (var db = dbFactory.Open())
            {

                if (orderByEnum == OrderByEnum.Desc)
                {
                    var where = db.From<T>().Where(expression).OrderByDescending(orderBy);
                    return await db.SelectAsync<T>(where);
                }
                else if (orderByEnum == OrderByEnum.Asc)
                {
                    var where = db.From<T>().Where(expression).OrderBy(orderBy);
                    return await db.SelectAsync<T>(where);
                }
                return await db.SelectAsync<T>(expression);
            }


        }
        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public long Recycle(Expression<Func<T>> updateFields, Expression<Func<T, bool>> where = null)
        {
            RecycleFilter();
            using (var db = dbFactory.Open())
            {
                return db.UpdateOnly<T>(updateFields, where);
            }
        }

    }
}
