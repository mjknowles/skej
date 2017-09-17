using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.AggregatesModel.ScheduleAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.EntityConfiguration
{
    class ScheduleElementEntityTypeConfiguration
         : IEntityTypeConfiguration<ScheduleElement>
    {
        public void Configure(EntityTypeBuilder<ScheduleElement> scheduleElementConfiguration)
        {
            scheduleElementConfiguration.ToTable("scheduleElements", SchedulingContext.DEFAULT_SCHEMA);

            scheduleElementConfiguration.HasKey(s => s.Id);

            scheduleElementConfiguration.Ignore(b => b.DomainEvents);

            scheduleElementConfiguration.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("scheduleelementseq");

            scheduleElementConfiguration.Property<int>("ScheduleElementId")
                .IsRequired();

            /*scheduleElementConfiguration.Property<decimal>("Discount")
                .IsRequired();

            scheduleElementConfiguration.Property<int>("ProductId")
                .IsRequired();

            scheduleElementConfiguration.Property<string>("ProductName")
                .IsRequired();

            scheduleElementConfiguration.Property<decimal>("UnitPrice")
                .IsRequired();

            scheduleElementConfiguration.Property<int>("Units")
                .IsRequired();

            scheduleElementConfiguration.Property<string>("PictureUrl")
                .IsRequired(false);*/
        }
    }
}
