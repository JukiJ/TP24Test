using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP24.Core.Models;

namespace TP24.Data
{
    public class TP24DbContext : DbContext
    {
        public TP24DbContext()
        {

        }

        public TP24DbContext(DbContextOptions<TP24DbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        }

        public DbSet<Receivable> Receivables { get; set; }
    }
}
