using Exchange.Core.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.Storage
{
    public class ExchangeDbContext : DbContext
    {
        public string DbPath { get; }
        public ExchangeDbContext(DbContextOptions<ExchangeDbContext> options) : base(options)
        {
        }
        public DbSet<ExchangeEntity> Exchanges { get; set; }
    }
}
