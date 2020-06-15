namespace Proje5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kitaplar")]
    public partial class Kitaplar
    {
        [Key]
        public int Kitapno { get; set; }

        [StringLength(50)]
        public string Kadi { get; set; }

        [StringLength(50)]
        public string Kyazar { get; set; }

        [StringLength(50)]
        public string Ketiketleri { get; set; }

        [StringLength(10)]
        public string Kdurum { get; set; }

        public int? Kkayitli { get; set; }
    }
}
