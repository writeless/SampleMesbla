using AutoMapper;
using Mesbla.Sales.Orders.Domain;
using Mesbla.Sales.Orders.Domain.ValueObjects;
using Mesbla.Sales.Orders.Queries.Results;
using Writeless.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mesbla.Sales.Orders.Mappers
{
    public class DomainToQuery : Profile
    {
        public DomainToQuery()
        {
            CreateMap<Order, OrderFlat>();
            CreateMap<Order, OrderDetail>();
            CreateMap<OrderItem, OrderDetail.OrderItem>();

            //CreateMap<OrderState, int>().ConvertUsing(e => (int)e);
            //CreateMap<OrderState, int?>().ConvertUsing(e => (int?)e);

            CreateMap<Order, IdText>()
                .ForMember(e => e.Id, e => e.MapFrom(d => d.Id))
                .ForMember(e => e.Text, e => e.MapFrom(d => d.State));
        }
    }
}
