using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SQLiteSample.Models
{
    public class CustomerContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        private static readonly string dataSource = "customer.db";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlite($"Data Source={dataSource}");
    }
}
