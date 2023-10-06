using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog_Console.Models
{
    internal class LogContext : DbContext
    {
        public DbSet<Log> Logs { get; set; }
        //public static readonly ILoggerFactory MyLoggerFactory
            //= new LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer("Data Source=DESKTOP-FT3ECCO;Initial Catalog=FurkanSerilogDB;Integrated Security=SSPI");
        }
    }
}
