using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
{
    [Table("PAIS")]
    public class Pais
    {
        public Pais()
        {
            Ciudad = new HashSet<Ciudad>();
            Post = new HashSet<Post>();
            UserPoster = new HashSet<UserPoster>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [InverseProperty("NombrePaisNavigation")]
        public virtual ICollection<Ciudad> Ciudad { get; set; }
        [InverseProperty("NombrePaisNavigation")]
        public virtual ICollection<Post> Post { get; set; }
        [InverseProperty("NombrePaisNavigation")]
        public virtual ICollection<UserPoster> UserPoster { get; set; }
    }
}