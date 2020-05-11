[Table("USER_POSTER")]
    public class UserPoster
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
        [InverseProperty(nameof(Ciudad.UserPoster))]
        public virtual Ciudad NombreCiudadNavigation { get; set; }
        [ForeignKey(nameof(NombrePais))]
        [InverseProperty(nameof(Pais.UserPoster))]
        public virtual Pais NombrePaisNavigation { get; set; }
    }