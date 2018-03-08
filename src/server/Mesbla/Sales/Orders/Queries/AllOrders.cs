using AutoMapper.QueryableExtensions;
using Mesbla.Sales.Orders.Queries.Results;
using Mesbla.Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Queries
{
    public class AllOrders
    {
        public class Query : IRequest<List<OrderFlat>>
        {
            //Se tiver parametro obrigatorio, deve ficar no construtor
        }

        public class QueryHandler : IRequestHandler<Query, List<OrderFlat>>
        {
            protected readonly QueryContext _context;

            public QueryHandler(QueryContext context)
            {
                _context = context;
            }

            public async Task<List<OrderFlat>> Handle(Query request, CancellationToken cancellationToken)
            {
                var response = await _context
                        .Orders
                        .ProjectTo<OrderFlat>()
                        .ToListAsync(cancellationToken);                

                return response;
            }
        }
    }
}
