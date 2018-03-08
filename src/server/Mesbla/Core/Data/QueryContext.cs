using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Mesbla.Core.Data
{
    public class QueryContext : BaseContext
    {
        public QueryContext(DbContextOptions options)
            : base(options)
        {
        }
    }
}
