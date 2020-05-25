using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
{
    [Table("CATEGORIA")]
    public class Categoria
    {
        public Categoria()
        {
            Post = new HashSet<Post>();
        }

        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [InverseProperty("NombreCategoriaNavigation")]
        public virtual ICollection<Post> Post { get; set; }
    }
}