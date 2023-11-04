using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase_LB3
{
    class FlowerContext : DbContext
    {
        public FlowerContext()
            : base("DbConnection")
        { }

        public DbSet<Flower> Flowers { get; set; }
    }
}
