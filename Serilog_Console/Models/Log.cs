using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog_Console.Models
{
    internal class Log : DbContext
    {
        // [Key]
        public int LogId { get; set; } // Id
        public int Time { get; set; }
        public string Message { get; set; }
    }
}
