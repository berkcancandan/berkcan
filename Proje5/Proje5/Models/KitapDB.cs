namespace Proje5.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class KitapDB : DbContext
    {
        public KitapDB()
            : base("name=KitapDB")
        {
        }

        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Kayitlar> Kayitlars { get; set; }
        public virtual DbSet<Kitaplar> Kitaplars { get; set; }
        public virtual DbSet<Misafir> Misafirs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Misafir>()
                .Property(e => e.Mid)
                .IsFixedLength();
        }
    }
}
