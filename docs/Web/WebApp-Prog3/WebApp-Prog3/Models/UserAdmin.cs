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
        public int Id { get; set; }
        [Required]
        [StringLength(90)]
        public string Usuario { get; set; }
        [Required]
        [StringLength(90)]
        public string Contraseña { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        [StringLength(90)]
        public string Email { get; set; }
    }
}