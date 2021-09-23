using System;
using System.Linq;
using ShardingCore.Core.VirtualDatabase.VirtualTables;
using ShardingCore.Sharding;

namespace ShardingCore.Extensions
{
/*
* @Author: xjm
* @Description:
* @Date: Thursday, 02 September 2021 20:46:24
* @Email: 326308290@qq.com
*/
    public static class StreamMergeContextExtension
    {
        /// <summary>
        /// 本次查询是否涉及到分表
        /// </summary>
        /// <param name="streamMergeContext"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static bool IsNormalQuery<TEntity>(this StreamMergeContext<TEntity> streamMergeContext)
        {
            return streamMergeContext.QueryEntities.Any(o=>!o.IsShardingDataSource()&&!o.IsShardingTable());
        }
        /// <summary>
        /// 单路由查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="streamMergeContext"></param>
        /// <returns></returns>
        public static bool IsSingleRouteQuery<TEntity>(this StreamMergeContext<TEntity> streamMergeContext)
        {
            return streamMergeContext.DataSourceRouteResult.IntersectDataSources.Count==1&&streamMergeContext.TableRouteResults.Count()==1;
        }
        /// <summary>
        /// 单表查询
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="streamMergeContext"></param>
        /// <returns></returns>
        public static bool IsSingleShardingTableQuery<TEntity>(this StreamMergeContext<TEntity> streamMergeContext)
        {
            return streamMergeContext.TableRouteResults.First().ReplaceTables.Count(o => o.EntityType.IsShardingTable()) == 1;
        }
        /// <summary>
        /// 本次查询仅包含一个对象的分表分库
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="streamMergeContext"></param>
        /// <returns></returns>
        public static bool IsSingleShardingQuery<TEntity>(this StreamMergeContext<TEntity> streamMergeContext)
        {
            return streamMergeContext.GetOriginalQueryable().ParseQueryableRoute().Count(o=>o.IsShardingTable()||o.IsShardingDataSource())==1;
        }
        public static bool IsSupportPaginationQuery<TEntity>(this StreamMergeContext<TEntity> streamMergeContext)
        {
            var queryEntities = streamMergeContext.GetOriginalQueryable().ParseQueryableRoute();
            //仅一个对象支持分库或者分表的组合
            return queryEntities.Count(o=>(o.IsShardingDataSource()&&!o.IsShardingTable()) ||(o.IsShardingDataSource()&& o.IsShardingTable())|| (!o.IsShardingDataSource() && o.IsShardingTable())) ==1;
        }
    }
}