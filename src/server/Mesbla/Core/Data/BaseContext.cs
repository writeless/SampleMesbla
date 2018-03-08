using Mesbla.Sales.Orders.Domain;
using Writeless.Data;
using Writeless.Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesbla.Core.Data
{
    public partial class BaseContext : EFContext
    {
        public BaseContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
