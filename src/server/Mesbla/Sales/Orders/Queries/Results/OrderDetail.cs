using Mesbla.Sales.Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mesbla.Sales.Orders.Queries.Results
{
    public class OrderDetail
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        //Pode ser usado tbm o ValueText
        public OrderState State { get; set; }

        public IEnumerable<OrderItem> Items { get; set; }

        public class OrderItem
        {
            public int Id { get; set; }

            public int ProductId { get; set; }
        }
    }
}
