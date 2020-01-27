using System;

namespace MaReB.Models
{
    public class Coordinate
    {
        //Ids
        public int Id { get; set; }
        public int? ComunaId { get; set; }
        public int? ProvinciaId { get; set; }
        public int? RegionId { get; set; }
        public int? CountryId { get; set; }
        public int? ProcedenciaId { get; set; }
        public string StationId { get; set; }
        //Parents
        public virtual Comuna Comuna { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Region Region { get; set; }
        public virtual Country Country { get; set; }
        public virtual Station Station { get; set; }
        public virtual Procedencia Procedencia { get; set; }
        //ATT
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Vertex { get; set; }
        //CHILD
    }
}
