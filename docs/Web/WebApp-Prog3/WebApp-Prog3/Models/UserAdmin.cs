using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
{
    [Table("USER_ADMIN")]
    public class UserAdmin
    {
        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [StringLength(90)]
        [Display(Name = "Nombre de usuario")]
        public string Usuario { get; set; }
        [Required]
        [StringLength(90)]
        [Display(Name = "Contraseña")]
        public string Contraseña { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [StringLength(120)]
        [Display(Name = "Nombre completo")]
        public string Nombre { get; set; }
        [StringLength(90)]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}