using System;

namespace MaReB.Models
{
    public class Captura
    {
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public int ProcedenciaId { get; set; }
        public virtual Procedencia Procedencia { get; set; }

        public int Precio { get; set; }

        public int Horas_Buceo { get; set; }

        public int N_Buzos { get; set; }

        public int EmbarcacionId { get; set; }

        public int PuertoId { get; set; }
        public virtual Puerto Puerto { get; set; }

        public int Cantidad { get; set; }

        public Destino Destino { get; set; }
    }

    public enum Destino
    {
        Industria = 1,
        Fresco = 2
    }
}
