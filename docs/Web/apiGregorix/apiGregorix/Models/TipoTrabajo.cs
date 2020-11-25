using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace apiGregorix.Models
{
    [Table("TIPO_TRABAJO")]
    [Index(nameof(Nombre), Name = "UQ__TIPO_TRA__75E3EFCFA3F31137", IsUnique = true)]
    public partial class TipoTrabajo
    {
        public TipoTrabajo()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [InverseProperty(nameof(Post.NombreTipoTrabajoNavigation))]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
