using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApp_Prog3.Models
{
    [Table("POST")]
    public class Post
    {
        [Key]
        [Column("ID")]
        [Display(Name = "ID")]
        public int Id { get; set; }
        [Required]
        [Column("Nombre_Categoria")]
        [Display(Name = "Categoria")]
        public int NombreCategoria { get; set; }
        [Required]
        [Column("Nombre_Tipo_Trabajo")]
        [Display(Name = "Tipo de trabajo")]
        public int NombreTipoTrabajo { get; set; }
        [Required]
        [Display(Name = "Poster")]
        public int Poster { get; set; }
        [Display(Name = "Logo (Recomendable: 400px X 400px)")]
        public byte[] Logo { get; set; }
        [Display(Name = "Direccion URL de Sitio Web")]
        [Column("Direccion_URL")]
        public string DireccionUrl { get; set; }
        [Required]
        [Column("Nombre_Posicion")]
        [StringLength(90)]
        [Display(Name = "Posicion de trabajo")]
        public string NombrePosicion { get; set; }
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
        [Required]
        [Display(Name = "Descripcion del empleo")]
        public string Descripcion { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }

        [ForeignKey(nameof(NombreCategoria))]
        [InverseProperty(nameof(Categoria.Post))]
        public virtual Categoria NombreCategoriaNavigation { get; set; }
        [ForeignKey(nameof(NombreCiudad))]
        [InverseProperty(nameof(Ciudad.Post))]
        public virtual Ciudad NombreCiudadNavigation { get; set; }
        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.Post))]
        public virtual Pais NombrePaisNavigation { get; set; }
        [ForeignKey(nameof(NombreTipoTrabajo))]
        [InverseProperty(nameof(TipoTrabajo.Post))]
        public virtual TipoTrabajo NombreTipoTrabajoNavigation { get; set; }

        public string Categorias { get; set; }

        [Display(Name ="Empresa")]
        public string Posters { get; set; }
        public string Ciudades { get; set; }
        public string Paises { get; set; }
        public string TipoTrabajos { get; set; }
    }
}