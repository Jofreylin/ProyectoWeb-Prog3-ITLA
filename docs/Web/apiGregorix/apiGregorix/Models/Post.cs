using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace apiGregorix.Models
{
    [Table("POST")]
    [Index(nameof(NombreCategoria), Name = "IX_POST_Nombre_Categoria")]
    [Index(nameof(NombreCiudad), Name = "IX_POST_Nombre_Ciudad")]
    [Index(nameof(NombrePais), Name = "IX_POST_Nombre_Pais")]
    [Index(nameof(NombreTipoTrabajo), Name = "IX_POST_Nombre_Tipo_Trabajo")]
    public partial class Post
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [Column("Nombre_Categoria")]
        public int NombreCategoria { get; set; }
        [Column("Nombre_Tipo_Trabajo")]
        public int NombreTipoTrabajo { get; set; }
        public int Poster { get; set; }
        public byte[] Logo { get; set; }
        [Column("Direccion_URL")]
        public string DireccionUrl { get; set; }
        [Required]
        [Column("Nombre_Posicion")]
        [StringLength(90)]
        public string NombrePosicion { get; set; }
        [Column("Nombre_Calle")]
        [StringLength(90)]
        public string NombreCalle { get; set; }
        [Column("Nombre_Ciudad")]
        public int NombreCiudad { get; set; }
        [Column("Nombre_Pais")]
        public int NombrePais { get; set; }
        [Required]
        public string Descripcion { get; set; }
        [Column("Fecha_Creacion", TypeName = "datetime")]
        public DateTime FechaCreacion { get; set; }

        [ForeignKey(nameof(NombreCategoria))]
        [InverseProperty(nameof(Categoria.Posts))]
        public virtual Categoria NombreCategoriaNavigation { get; set; }
        [ForeignKey(nameof(NombreCiudad))]
        [InverseProperty(nameof(Ciudad.Posts))]
        public virtual Ciudad NombreCiudadNavigation { get; set; }
        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.Posts))]
        public virtual Pais NombrePaisNavigation { get; set; }
        [ForeignKey(nameof(NombreTipoTrabajo))]
        [InverseProperty(nameof(TipoTrabajo.Posts))]
        public virtual TipoTrabajo NombreTipoTrabajoNavigation { get; set; }
    }
}
