using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealerShipMvc.Models
{
    public class Car : BaseClass
    {
        public string Model { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
    }
}
