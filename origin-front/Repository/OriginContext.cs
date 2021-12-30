using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using origin_front.Domain;

namespace origin_front.Repository
{
    public class OriginContext : DbContext
    {
        
        public OriginContext(DbContextOptions<OriginContext> options) : base(options) { }

        public DbSet<Tarjeta> Tarjetas { get; set; }
    }
}
