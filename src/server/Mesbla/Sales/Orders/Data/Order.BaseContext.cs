using Mesbla.Sales.Orders.Data;
using Mesbla.Sales.Orders.Domain;
using Mesbla.Sales.Orders.Mappers;
using Writeless.Data;
using Writeless.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mesbla.Core.Data
{
    public partial class BaseContext : EFContext
    {
        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("Sales");

            Mapping.OrderMap(modelBuilder);
            Mapping.OrderItemMap(modelBuilder);
        }
    }
}
