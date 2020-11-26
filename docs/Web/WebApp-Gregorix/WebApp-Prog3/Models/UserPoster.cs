using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
{
    [Table("USER_POSTER")]
    public class UserPoster
    {
        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [Column("Nombre_Empresa")]
        [StringLength(120)]
        [Display(Name = "Nombre del autor")]
        public string NombreEmpresa { get; set; }
        [Required]
        [StringLength(90)]
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Confirmar email")]
        public string ConfirmarEmail { get; set; }
        [Required]
        [StringLength(90)]
        [Display(Name = "Contraseña")]
        public string Contra { get; set; }

        [Display(Name = "Confirmar contraseña")]
        public string ConfirmarContra { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
        [Column("Nombre_Calle")]
        [StringLength(90)]
        [Display(Name = "Calle")]
        public string NombreCalle { get; set; }
        [Column("Nombre_Ciudad")]
        [Display(Name = "Ciudad")]
        [Required]
        public int NombreCiudad { get; set; }
        [Column("Nombre_Pais")]
        [Display(Name = "Pais")]
        [Required]
        public int NombrePais { get; set; }

        [ForeignKey(nameof(NombreCiudad))]
        [InverseProperty(nameof(Ciudad.UserPoster))]
        public virtual Ciudad NombreCiudadNavigation { get; set; }
        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.UserPoster))]
        public virtual Pais NombrePaisNavigation { get; set; }

        public string Ciudades { get; set; }
        public string Paises { get; set; }
    }
}