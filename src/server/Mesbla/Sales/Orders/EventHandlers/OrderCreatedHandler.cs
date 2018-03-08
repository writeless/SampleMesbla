using Mesbla.Sales.Orders.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.EventHandlers
{
    public class OrderCreatedHandler
    {
        public class SendEmail : INotificationHandler<CreateOrder.Completed>
        {
            public Task Handle(CreateOrder.Completed notification, CancellationToken cancellationToken)
            {
                System.Diagnostics.Debug.WriteLine("Send Email Order Created");
                return Task.CompletedTask;
            }
        }
    }
}
