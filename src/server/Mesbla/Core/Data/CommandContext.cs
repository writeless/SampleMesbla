using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Mesbla.Core.Data
{
    public class CommandContext : BaseContext
    {
        public CommandContext(DbContextOptions<CommandContext> options)
            : base(options)
        {
        }
    }
}
