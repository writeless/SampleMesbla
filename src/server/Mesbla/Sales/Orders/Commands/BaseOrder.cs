using Mesbla.Sales.Orders.Domain;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Commands
{
    public class BaseOrder
    {
        public abstract class Command : IRequest
        {
            public int Id { get; set; }

            public int CustomerId { get; set; }

            public int State { get; set; }

            public IEnumerable<OrderItem> Items { get; set; }

            public class OrderItem
            {
                public int Id { get; set; }

                public int ProductId { get; set; }
            }
        }

        public class OrderValidator : AbstractValidator<Command>
        {
            public OrderValidator()
            {
                RuleFor(e => e.CustomerId).GreaterThanOrEqualTo(1);
                RuleFor(e => e.Items).Must(e => e.Count() > 0);
            }
        }        

        public class OrderItemValidator : AbstractValidator<Command.OrderItem>
        {
            public OrderItemValidator()
            {
                RuleFor(e => e.ProductId).GreaterThanOrEqualTo(1);
            }
        }
    }
}
