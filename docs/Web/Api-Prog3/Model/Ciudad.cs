using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
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
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }
        [Column("Nombre_Pais")]
        public int NombrePais { get; set; }

        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.Ciudad))]
        public virtual Pais NombrePaisNavigation { get; set; }
        [InverseProperty("NombreCiudadNavigation")]
        public virtual ICollection<Post> Post { get; set; }
        [InverseProperty("NombreCiudadNavigation")]
        public virtual ICollection<UserPoster> UserPoster { get; set; }
    }
}
