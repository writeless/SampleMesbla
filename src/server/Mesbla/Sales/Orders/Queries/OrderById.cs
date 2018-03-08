using AutoMapper.QueryableExtensions;
using Mesbla.Sales.Orders.Queries.Results;
using Mesbla.Core.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Queries
{
    public class OrderById
    {
        public class Query : IRequest<OrderDetail>
        {
            public int Id { get; set; }

            public Query(int id)
            {
                Id = id;
            }
        }

        public class QueryHandler : IRequestHandler<Query, OrderDetail>
        {
            protected readonly QueryContext _context;

            public QueryHandler(QueryContext context)
            {
                _context = context;
            }

            public async Task<OrderDetail> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _context
                        .Orders
                        .ProjectTo<OrderDetail>()
                        .FirstOrDefaultAsync(cancellationToken);
            }
        }
    }
}
