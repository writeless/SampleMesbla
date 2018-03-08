using Mesbla.Sales.Orders.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.EventHandlers
{
    public class OrderSavedHandler
    {
        public class UpdateBalance : INotificationHandler<CreateOrder.Completed>, INotificationHandler<UpdateOrder.Completed>
        {
            public Task Handle(CreateOrder.Completed notification, CancellationToken cancellationToken)
            {
                System.Diagnostics.Debug.WriteLine("Update Balance Order Created");
                return this.Update();
            }

            public Task Handle(UpdateOrder.Completed notification, CancellationToken cancellationToken)
            {
                System.Diagnostics.Debug.WriteLine("Update Balance Order Updated");
                return this.Update();
            }

            public Task Update()
            {
                System.Diagnostics.Debug.WriteLine("Update");
                return Task.CompletedTask;
            }
        }
    }
}
