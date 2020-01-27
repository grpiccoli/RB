using System.ComponentModel.DataAnnotations.Schema;

namespace MaReB.Models
{
    public class Puerto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int ComunaId { get; set; }
        public virtual Comuna Comuna { get; set; }

        public string Name { get; set; }
    }
}
