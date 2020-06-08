
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;


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
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre de categoria")]
        public string Nombre { get; set; }
        public int Cantidad { get; set; }

        [Display(Name = "Logo de la categoria")]
        public byte[] Logo { get; set; }

        [InverseProperty("NombreCategoriaNavigation")]
        public virtual ICollection<Post> Post { get; set; }
    }
}