using Mesbla.Sales.Orders.Commands;
using Mesbla.Sales.Orders.Queries;
using Writeless.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Controllers
{
    [Route("api/sales/[controller]")]
    public class OrdersController : Controller
    {
        protected readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var query = new AllOrders.Query();
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpGet("asidtext")]
        public async Task<IActionResult> GetAsIdText(CancellationToken cancellationToken)
        {
            var query = new AllOrdersAsIdText.Query();
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var query = new OrderById.Query(id);
            var response = await _mediator.Send(query, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateOrder.Command command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]UpdateOrder.Command command, CancellationToken cancellationToken)
        {
            command.Id = id;
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPatch("{id}/confirm")]
        public async Task<IActionResult> ConfirmOrder(int id, CancellationToken cancellationToken)
        {
            var command = new ConfirmOrder.Command() { Id = id };
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
