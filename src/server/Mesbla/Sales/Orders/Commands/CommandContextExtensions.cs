using Mesbla.Sales.Orders.Domain;
using Mesbla.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Commands
{
    public static class CommandContextExtensions
    {
        //TODO: Lembrete
        //avoid SingleOrDefaultAsync ou o FirstOrDefaultAsync, like this:
        //var o = await orders.Include(e => e.Items).SingleOrDefaultAsync(e => e.Id == id);

        public static async Task<Order> GetOrderAsync(this CommandContext context, int id, CancellationToken cancellationToken)
        {
            var order = await context.Orders.FindAsync(new object[] { id }, cancellationToken);

            if (order != null)
            {
                await context.Entry(order)
                    .Collection(i => i.Items).LoadAsync(cancellationToken);
            }

            return order;
        }
    }
}
