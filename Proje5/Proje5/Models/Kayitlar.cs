namespace Proje5.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Kayitlar")]
    public partial class Kayitlar
    {
        [Key]
        public int Bid { get; set; }

        public int Bkayitno { get; set; }

        public int Bkitapno { get; set; }
    }
}
