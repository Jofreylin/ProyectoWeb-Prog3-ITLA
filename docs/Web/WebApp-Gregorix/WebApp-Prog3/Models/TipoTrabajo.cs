using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
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
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Nombre del tipo de libro")]
        public string Nombre { get; set; }

        [InverseProperty("NombreTipoTrabajoNavigation")]
        public virtual ICollection<Post> Post { get; set; }
    }
}