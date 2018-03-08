using AutoMapper;
using FluentValidation;
using Mesbla.Sales.Orders.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mesbla.Sales.Orders.Domain.ValueObjects;
using Writeless.Extensions.FluentValidation;
using Mesbla.Core.Data;

namespace Mesbla.Sales.Orders.Commands
{
    public class CreateOrder
    {
        public class Completed : INotification
        {
            public Command Command { get; private set; }

            public Order Entity { get; private set; }

            public Completed(Command command, Order entity)
            {
                Command = command;
                Entity = entity;
            }
        }

        public class Command : BaseOrder.Command
        {
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CommandContext context)
            {
                RuleFor(e => e).NotNull().SetValidator(new BaseOrder.OrderValidator());
                RuleForEach(e => e.Items).NotNull().SetValidator(new BaseOrder.OrderItemValidator());

                RuleFor(e => e.CustomerId).Unique(context.Orders);
            }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            protected readonly CommandContext _context;
            protected readonly IMediator _mediator;

            public CommandHandler(CommandContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = Mapper.Map<Order>(request);
                await _context.AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new Completed(request, entity));
            }
        }
    }
}
