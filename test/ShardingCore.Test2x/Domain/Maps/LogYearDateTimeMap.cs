﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShardingCore.Test2x.Domain.Entities;

namespace ShardingCore.Test2x.Domain.Maps
{
    public class LogYearDateTimeMap : IEntityTypeConfiguration<LogYearDateTime>
    {
        public void Configure(EntityTypeBuilder<LogYearDateTime> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.LogBody).IsRequired().HasMaxLength(256);
            builder.ToTable(nameof(LogYearDateTime));
        }
    }
}
