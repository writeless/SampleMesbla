using Mesbla.Sales.Orders.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mesbla.Sales.Orders.Data
{
    public static class Mapping
    {
        public static void OrderMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>(map =>
            {
                map.HasKey(t => t.Id);
            });
        }

        public static void OrderItemMap(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>(map =>
            {
                map.HasKey(t => t.Id);
            });
        }
    }
}
