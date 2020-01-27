using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaReB.Models
{
    public class Origin
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int RegionId { get; set; }
        public virtual Region Region { get; set; }

        public string Name { get; set; }

        public string Observaciones { get; set; }

        public virtual Coordinate Coordinate { get; set; }

        public virtual ICollection<Capture> Captures { get; set; }
    }
}