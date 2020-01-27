using System.ComponentModel.DataAnnotations.Schema;

namespace MaReB.Models
{
    public class Port
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int CommuneId { get; set; }
        public virtual Commune Commune { get; set; }

        public string Name { get; set; }
    }
}
