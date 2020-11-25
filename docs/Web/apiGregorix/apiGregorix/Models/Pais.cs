using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace apiGregorix.Models
{
    [Table("PAIS")]
    [Index(nameof(Nombre), Name = "UQ__PAIS__75E3EFCFD08B0375", IsUnique = true)]
    public partial class Pais
    {
        public Pais()
        {
            Ciudads = new HashSet<Ciudad>();
            Posts = new HashSet<Post>();
            UserPosters = new HashSet<UserPoster>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [InverseProperty(nameof(Ciudad.NombrePaisNavigation))]
        public virtual ICollection<Ciudad> Ciudads { get; set; }
        [InverseProperty(nameof(Post.NombrePaisNavigation))]
        public virtual ICollection<Post> Posts { get; set; }
        [InverseProperty(nameof(UserPoster.NombrePaisNavigation))]
        public virtual ICollection<UserPoster> UserPosters { get; set; }
    }
}
