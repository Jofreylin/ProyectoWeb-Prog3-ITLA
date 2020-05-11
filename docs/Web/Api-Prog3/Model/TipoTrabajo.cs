using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("TIPO_TRABAJO")]
    public class TipoTrabajo
    {
        public TipoTrabajo()
        {
            Post = new HashSet<Post>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [InverseProperty("NombreTipoTrabajoNavigation")]
        public virtual ICollection<Post> Post { get; set; }
    }
}
