﻿using System;

namespace ShardingCore.Test3x.Domain.Entities
{
    public class MultiShardingOrder
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
