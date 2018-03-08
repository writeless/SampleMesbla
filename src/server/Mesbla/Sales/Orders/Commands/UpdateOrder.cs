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
    public class UpdateOrder
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
            public CommandValidator()
            {
                RuleFor(e => e).NotNull().SetValidator(new BaseOrder.OrderValidator());
                RuleForEach(e => e.Items).NotNull().SetValidator(new BaseOrder.OrderItemValidator());

                RuleFor(e => e.State).Equal((int)OrderState.AwaitingConfirmation);
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

                Mapper.Map(request, entity);
                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new Completed(request, entity));
            }
        }
    }
}
