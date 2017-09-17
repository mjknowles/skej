using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Scheduling.Domain.AggregatesModel.ScheduleAggregate;
using Scheduling.Infrastructure.Idempotency;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scheduling.Infrastructure.EntityConfiguration
{
    class ScheduleEntityTypeConfiguration
        : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> scheduleConfiguration)
        {
            scheduleConfiguration.ToTable("schedules", SchedulingContext.DEFAULT_SCHEMA);

            scheduleConfiguration.HasKey(s => s.Id);

            scheduleConfiguration.Ignore(b => b.DomainEvents);

            scheduleConfiguration.Property(o => o.Id)
                .ForSqlServerUseSequenceHiLo("scheduleseq", SchedulingContext.DEFAULT_SCHEMA);

            /*orderConfiguration.OwnsOne(o => o.Address);

            orderConfiguration.Property<DateTime>("OrderDate").IsRequired();
            orderConfiguration.Property<int?>("BuyerId").IsRequired(false);
            orderConfiguration.Property<int>("OrderStatusId").IsRequired();
            orderConfiguration.Property<int?>("PaymentMethodId").IsRequired(false);
            orderConfiguration.Property<string>("Description").IsRequired(false);*/

            var navigation = scheduleConfiguration.Metadata.FindNavigation(nameof(Schedule.ScheduleElements));

            // DDD Patterns comment:
            //Set as Field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            /*orderConfiguration.HasOne<PaymentMethod>()
                .WithMany()
                .HasForeignKey("PaymentMethodId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);

            orderConfiguration.HasOne<Buyer>()
                .WithMany()
                .IsRequired(false)
                .HasForeignKey("BuyerId");

            orderConfiguration.HasOne(o => o.OrderStatus)
                .WithMany()
                .HasForeignKey("OrderStatusId");*/
        }
    }
}
