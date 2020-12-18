using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AssyChargeSEHC.ModelEF
{
    public class DbSEHCContext : DbContext
    {
        public DbSet<ModelList> ModelList { get; set; }
        public DbSet<ResultList> ResultList { get; set; }
        public DbSet<Account> Account { get; set; }
        public DbSet<CounterAmount> CounterAmount { get; set; }
        public DbSet<SystemLogs> SystemLogs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=DbSEHC.db");
        }
    }
}
