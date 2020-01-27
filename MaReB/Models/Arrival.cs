using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaReB.Models
{
    public class Arrival
    {
        //Id
        public int Id { get; set; }

        //PARent
        public int ComunaId { get; set; }

        public virtual Comuna Comuna { get; set; }

        //Att
        public string Caleta { get; set; }

        [Display(Name = "Fecha de Llegada")]
        [DisplayFormat(DataFormatString = "{0:d/M/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Recurso")]
        public Species Species { get; set; }

        [Display(Name = "Toneladas")]
        [DisplayFormat(DataFormatString = "{0:0}")]
        public int Kg { get; set; }
    }
    public enum Species
    {
        Almeja = 0,
        Erizo = 1,
        Luga = 2
    }
}
