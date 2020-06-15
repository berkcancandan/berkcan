namespace Proje5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Admin")]
    public partial class Admin
    {
        [Key]
        public int Akayitno { get; set; }

        [StringLength(20)]
        public string Aad { get; set; }

        [StringLength(20)]
        public string Asoyad { get; set; }

        [StringLength(10)]
        public string Aid { get; set; }

        public int? Asifre { get; set; }
    }
}
