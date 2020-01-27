using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaReB.Models
{
    public class Station
    {
        [Display(Name = "Código de Región")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        [Display(Name = "Región")]
        public int RegionId { get; set; }

        [Display(Name = "Región")]
        public virtual Region Region { get; set; }

        [Display(Name = "Área")]
        public string Area { get; set; }

        [Display(Name = "Nombre")]
        public string Name { get; set; }

        public ICollection<Coordinate> Coordinates { get; set; }
    }
}
