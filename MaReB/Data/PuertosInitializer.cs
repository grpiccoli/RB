using System.Linq;
using MaReB.Models;

namespace MaReB.Data
{
    public class PuertosInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            #region Estaciones 16
            if (!context.Puertos.Any())
            {
                var Puertos = new Puerto[]
                {
                    new Puerto {Id=46,Name="Quemchi",ComunaId=10209},
                    new Puerto {Id=65,Name="Curanué",ComunaId=10208},
                    new Puerto {Id=900,Name="Pesqueras Dalcahue",ComunaId=10205},
                    new Puerto {Id=905,Name="Quetalmahue",ComunaId=10202},
                    new Puerto {Id=910,Name="Yuste",ComunaId=10202},
                    new Puerto {Id=915,Name="Caulin",ComunaId=10202},
                    new Puerto {Id=938,Name="San Rafael (Calbuco)",ComunaId=10102},
                    new Puerto {Id=939,Name="Maullín",ComunaId=10108},
                    new Puerto {Id=941,Name="Caleta Chaular",ComunaId=10202},
                    new Puerto {Id=942,Name="Carelmapu",ComunaId=10108},
                    new Puerto {Id=947,Name="Ancud",ComunaId=10202},
                    new Puerto {Id=949,Name="Pudeto",ComunaId=10202},
                    new Puerto {Id=950,Name="Dalcahue",ComunaId=10205},
                    new Puerto {Id=953,Name="Queilén",ComunaId=10207},
                    new Puerto {Id=958,Name="Quellón",ComunaId=10208},
                    new Puerto {Id=959,Name="Pesqueras de Quellón",ComunaId=10208},
                    new Puerto {Id=960,Name="Pto. Chacabuco",ComunaId=11201},
                    new Puerto {Id=961,Name="Melinka",ComunaId=11203},
                    new Puerto {Id=1003,Name="La Vega (Calbuco)",ComunaId=10102}
                };
                context.BulkInsert(Puertos);
            }
            #endregion
        }
    }
}