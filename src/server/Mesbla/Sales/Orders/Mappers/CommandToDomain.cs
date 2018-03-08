using AutoMapper;
using Mesbla.Sales.Orders.Commands;
using Mesbla.Sales.Orders.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesbla.Sales.Orders.Mappers
{
    public class CommandToDomain : Profile
    {
        public CommandToDomain()
        {
            CreateMap<CreateOrder.Command, Order>();
            CreateMap<CreateOrder.Command.OrderItem, OrderItem>();
        }
    }
}
