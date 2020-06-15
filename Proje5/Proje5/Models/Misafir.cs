namespace Proje5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Misafir")]
    public partial class Misafir
    {
        [Key]
        public int Mkayitno { get; set; }

        [StringLength(20)]
        public string Mad { get; set; }

        [StringLength(50)]
        public string Msoyad { get; set; }

        [Required]
        [StringLength(10)]
        public string Mid { get; set; }

        public int Msifre { get; set; }

        public int? Mkitapno { get; set; }
    }
}
