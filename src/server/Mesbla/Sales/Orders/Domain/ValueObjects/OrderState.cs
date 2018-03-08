using System;
using System.Collections.Generic;
using System.Text;

namespace Mesbla.Sales.Orders.Domain.ValueObjects
{
    public enum OrderState
    {
        AwaitingConfirmation = 1,
        AwaitingPayment,
        AwaitingProduction,
        AwaitingBilling,
        AwaitingDelivery,
        Closed,
    }

    public static class OrderStateExtensions
    {
        public static OrderState Next(this OrderState state)
        {
            var current = (int)state;
            var next = current + 1;
            return (OrderState)next;

            //switch (state)
            //{
            //    case OrderState.AwaitingConfirmation:
            //        return OrderState.AwaitingPayment;
            //    case OrderState.AwaitingPayment:
            //        break;
            //    case OrderState.AwaitingProduction:
            //        break;
            //    case OrderState.AwaitingBilling:
            //        break;
            //    case OrderState.AwaitingDelivery:
            //        break;
            //    case OrderState.Closed:
            //        break;
            //    default:
            //        break;
            //}
        }
    }
}
