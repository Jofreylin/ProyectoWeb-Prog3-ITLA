using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace apiGregorix.Models
{
    [Table("CATEGORIA")]
    [Index(nameof(Nombre), Name = "UQ__CATEGORI__75E3EFCF7C426275", IsUnique = true)]
    public partial class Categoria
    {
        public Categoria()
        {
            Posts = new HashSet<Post>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public byte[] Logo { get; set; }

        [InverseProperty(nameof(Post.NombreCategoriaNavigation))]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
