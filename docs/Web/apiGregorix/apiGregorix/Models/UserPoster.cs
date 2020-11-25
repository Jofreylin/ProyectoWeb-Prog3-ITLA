using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace apiGregorix.Models
{
    [Table("USER_POSTER")]
    [Index(nameof(NombreCiudad), Name = "IX_USER_POSTER_Nombre_Ciudad")]
    [Index(nameof(NombrePais), Name = "IX_USER_POSTER_Nombre_Pais")]
    [Index(nameof(Email), Name = "UQ__USER_POS__A9D10534E7793F11", IsUnique = true)]
    public partial class UserPoster
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Required]
        [Column("Nombre_Empresa")]
        [StringLength(120)]
        public string NombreEmpresa { get; set; }
        [Required]
        [StringLength(90)]
        public string Email { get; set; }
        [Required]
        [StringLength(90)]
        public string Contra { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }
        [Column("Nombre_Calle")]
        [StringLength(90)]
        public string NombreCalle { get; set; }
        [Column("Nombre_Ciudad")]
        public int NombreCiudad { get; set; }
        [Column("Nombre_Pais")]
        public int NombrePais { get; set; }

        [ForeignKey(nameof(NombreCiudad))]
        [InverseProperty(nameof(Ciudad.UserPosters))]
        public virtual Ciudad NombreCiudadNavigation { get; set; }
        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.UserPosters))]
        public virtual Pais NombrePaisNavigation { get; set; }
    }
}
