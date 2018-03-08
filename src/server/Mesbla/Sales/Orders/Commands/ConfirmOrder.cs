using AutoMapper;
using Mesbla.Sales.Orders.Domain;
using Mesbla.Sales.Orders.Domain.ValueObjects;
using Mesbla.Core.Data;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Commands
{
    public class ConfirmOrder
    {
        const OrderState EXPECTED_CURRENT_STATE = OrderState.AwaitingConfirmation;
        const OrderState NEXT_STATE = OrderState.AwaitingPayment;

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

        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator(CommandContext context)
            {
                RuleFor(e => e).MustAsync(async (e, cancellationToken) =>
                {
                    var current = await context.GetOrderAsync(e.Id, cancellationToken);

                    return current.State == EXPECTED_CURRENT_STATE;
                });                
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
                var entity = await _context.GetOrderAsync(request.Id, cancellationToken);

                entity.SetState(NEXT_STATE);
                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new Completed(request, entity));
            }
        }
    }
}
