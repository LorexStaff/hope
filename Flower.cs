using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hope
{
    public class Flower
    {
        public string Name { get; set; }
        public string Region { get; set; }
        public int Price { get; set; }
        public bool Rare { get; set; }

        public Flower(string name, string region, int price, bool rare)
        {
            Name = name;
            Region = region;
            Price = price;
            Rare = rare;
        }
    }
}
