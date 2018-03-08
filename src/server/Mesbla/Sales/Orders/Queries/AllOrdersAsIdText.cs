using AutoMapper.QueryableExtensions;
using Mesbla.Sales.Orders.Queries.Results;
using Mesbla.Core.Data;
using Writeless.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Queries
{
    public class AllOrdersAsIdText
    {
        public class Query : IRequest<List<IdText>>
        {
            //Se tiver parametro obrigatorio, deve ficar no construtor
        }

        public class QueryHandler : IRequestHandler<Query, List<IdText>>
        {
            protected readonly QueryContext _context;

            public QueryHandler(QueryContext context)
            {
                _context = context;
            }

            public async Task<List<IdText>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context
                        .Orders
                        .ProjectTo<IdText>()
                        .ToListAsync(cancellationToken);
            }
        }
    }
}
