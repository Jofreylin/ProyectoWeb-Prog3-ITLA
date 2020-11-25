using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace apiGregorix.Models
{
    [Table("CIUDAD")]
    [Index(nameof(NombrePais), Name = "IX_CIUDAD_Nombre_Pais")]
    public partial class Ciudad
    {
        public Ciudad()
        {
            Posts = new HashSet<Post>();
            UserPosters = new HashSet<UserPoster>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [Column("Nombre_Pais")]
        public int NombrePais { get; set; }

        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.Ciudads))]
        public virtual Pais NombrePaisNavigation { get; set; }
        [InverseProperty(nameof(Post.NombreCiudadNavigation))]
        public virtual ICollection<Post> Posts { get; set; }
        [InverseProperty(nameof(UserPoster.NombreCiudadNavigation))]
        public virtual ICollection<UserPoster> UserPosters { get; set; }
    }
}
