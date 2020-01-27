using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaReB.Models
{
    public class Continent
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string ISO { get; set; }
        public string Name { get; set; }
        public Double Latitude { get; set; }
        public Double Longitude { get; set; }
        public ICollection<Country> Countries { get; set; }
    }
}
