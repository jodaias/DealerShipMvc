using DealerShipMvc.Data.Interfaces;
using DealerShipMvc.Data.Repositories;
using DealerShipMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Data
{
    public class CarRepository : Repository<Car>, ICarRepository
    {
        public CarRepository(DealerShipMvcContext context) : base(context)
        {

        }
    }
}
