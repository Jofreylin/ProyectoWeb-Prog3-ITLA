using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
{
    [Table("CIUDAD")]
    public class Ciudad
    {
        public Ciudad()
        {
            Post = new HashSet<Post>();
            UserPoster = new HashSet<UserPoster>();
        }

        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre de ciudad")]
        public string Nombre { get; set; }
        [Column("Nombre_Pais")]
        [Display(Name = "Pais de la ciudad")]
        public int NombrePais { get; set; }

        public int Cantidad { get; set; }

        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.Ciudad))]
        public virtual Pais NombrePaisNavigation { get; set; }
        [InverseProperty("NombreCiudadNavigation")]
        public virtual ICollection<Post> Post { get; set; }
        [InverseProperty("NombreCiudadNavigation")]
        public virtual ICollection<UserPoster> UserPoster { get; set; }
    }
}