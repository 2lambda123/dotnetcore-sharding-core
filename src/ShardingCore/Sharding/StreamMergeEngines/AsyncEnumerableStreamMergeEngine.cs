using ShardingCore.Sharding.ShardingQueryExecutors;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using ShardingCore.Core.TrackerManagers;
using ShardingCore.Sharding.Abstractions;
using ShardingCore.Sharding.StreamMergeEngines.TrackerEnumerators;

namespace ShardingCore.Sharding.StreamMergeEngines
{
    /*
    * @Author: xjm
    * @Description:
    * @Date: Saturday, 14 August 2021 22:07:28
    * @Email: 326308290@qq.com
    */
    public class AsyncEnumerableStreamMergeEngine<TShardingDbContext,T> : IAsyncEnumerable<T>, IEnumerable<T>
    where  TShardingDbContext:DbContext,IShardingDbContext
    {
        private readonly StreamMergeContext<T> _mergeContext;
        private readonly ITrackerManager<TShardingDbContext> _trackerManager;

        public AsyncEnumerableStreamMergeEngine(StreamMergeContext<T> mergeContext)
        {
            _mergeContext = mergeContext;
            _trackerManager = ShardingContainer.GetService<ITrackerManager<TShardingDbContext>>();
        }

        private bool IsUseManualTrack => GetIsUseManualTrack();

        private bool GetIsUseManualTrack()
        {
            if (!_mergeContext.IsCrossTable)
                return false;
            if (_mergeContext.IsNoTracking.HasValue)
            {
                return !_mergeContext.IsNoTracking.Value;
            }
            else
            {
                return ((DbContext) _mergeContext.GetShardingDbContext()).ChangeTracker.QueryTrackingBehavior ==
                       QueryTrackingBehavior.TrackAll;
            }
        }

#if !EFCORE2
        public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = new CancellationToken())
        {
            var asyncEnumerator = new EnumeratorShardingQueryExecutor<TShardingDbContext,T>(_mergeContext).ExecuteAsync(cancellationToken)
                .GetAsyncEnumerator(cancellationToken);

            if (IsUseManualTrack&&_trackerManager.EntityUseTrack(typeof(T)))
            {
                return new AsyncTrackerEnumerator<T>(_mergeContext, asyncEnumerator);
            }

            return asyncEnumerator;
        }
#endif

#if EFCORE2
        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetEnumerator()
        {
            var asyncEnumerator = ((IAsyncEnumerable<T>)new EnumeratorShardingQueryExecutor<TShardingDbContext,T>(_mergeContext).ExecuteAsync())
                .GetEnumerator();
            if (IsUseManualTrack&&_trackerManager.EntityUseTrack(typeof(T)))
            {
                return new AsyncTrackerEnumerator<T>(_mergeContext, asyncEnumerator);
            }
            return asyncEnumerator;
        }
#endif


        public IEnumerator<T> GetEnumerator()
        {
            var enumerator = ((IEnumerable<T>)new EnumeratorShardingQueryExecutor<TShardingDbContext,T>(_mergeContext).ExecuteAsync())
                .GetEnumerator();

            if (IsUseManualTrack&&_trackerManager.EntityUseTrack(typeof(T)))
            {
                return new TrackerEnumerator<T>(_mergeContext, enumerator);
            }
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}