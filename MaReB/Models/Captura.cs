using System;

namespace MaReB.Models
{
    public class Capture
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int OriginId { get; set; }
        public virtual Origin Origin { get; set; }

        public int Precio { get; set; }

        public int Horas_Buceo { get; set; }

        public int N_Buzos { get; set; }

        public int EmbarcacionId { get; set; }

        public int PortId { get; set; }
        public virtual Port Port { get; set; }

        public int Cantidad { get; set; }

        public Destino Destino { get; set; }
    }

    public enum Destino
    {
        Industria = 1,
        Fresco = 2
    }
}
