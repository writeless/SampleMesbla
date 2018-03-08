using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Domain
{
    public class OrderItem
    {
        public int Id { get; private set; }

        public int ProductId { get; private set; }

        //EF used by EF
        protected OrderItem()
        {
        }

        public OrderItem(int productId)
        {
            ProductId = productId;
        }
    }
}
