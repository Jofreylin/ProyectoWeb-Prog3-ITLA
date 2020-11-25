using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace apiGregorix.Models
{
    [Table("USER_ADMIN")]
    [Index(nameof(Usuario), Name = "UQ__USER_ADM__E3237CF72DA8FDC4", IsUnique = true)]
    public partial class UserAdmin
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
