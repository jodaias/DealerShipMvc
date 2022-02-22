using DealerShipMvc.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Data
{
    public class DealerShipMvcContext : DbContext
    {
        public DealerShipMvcContext(DbContextOptions<DealerShipMvcContext> options)
           : base(options)
        {
        }
        public DbSet<Car> Cars { get; set; }
    }
}
