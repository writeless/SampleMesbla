using Mesbla.Sales.Orders.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Domain
{
    //TODO: criar o base entity, contendo Id e Equals()
    //Fazer a extension do AutoMapper para NxN e 1xN

    public class Order
    {
        public int Id { get; private set; }

        public int CustomerId { get; private set; }

        public OrderState State { get; private set; }

        public ICollection<OrderItem> Items { get; private set; }

        //EF used by EF
        protected Order()
        {
            Items = new List<OrderItem>();
        }

        public Order(int customerId, ICollection<OrderItem> items)
        {
            CustomerId = customerId;
            State = OrderState.AwaitingConfirmation;
            Items = items;
        }

        public void SetState(OrderState state)
        {
            if (State.Next() != state)
                throw new ArgumentException("setstate");

            State = state;
        }

        public void SetItems(ICollection<OrderItem> items)
        {
            //TODO: implementar alguma regra
            Items = items;
        }
    }
}
