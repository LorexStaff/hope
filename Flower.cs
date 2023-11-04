using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DataBase_LB3
{
    public class Flower
    {
        [Key]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Region { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public bool Rare { get; set; }
    }
}
