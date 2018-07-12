namespace ParxlabAVM.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model : DbContext
    {
        public Model()
            : base("name=ModelMsSql")
        {
        }

        public virtual DbSet<anatablo> anatablo { get; set; }
        public virtual DbSet<cihaz> cihaz { get; set; }
        public virtual DbSet<firma> firma { get; set; }
        public virtual DbSet<il> il { get; set; }
        public virtual DbSet<ilce> ilce { get; set; }
        public virtual DbSet<kullanici> kullanici { get; set; }
        public virtual DbSet<parkyeri> parkyeri { get; set; }
        public virtual DbSet<yetki> yetki { get; set; }
        public virtual DbSet<yetkili> yetkili { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<anatablo>()
                .Property(e => e.giriszamani)
                .HasPrecision(0);

            modelBuilder.Entity<anatablo>()
                .Property(e => e.cikiszamani)
                .HasPrecision(0);

            modelBuilder.Entity<anatablo>()
                .Property(e => e.aracplakasi)
                .IsUnicode(false);

            modelBuilder.Entity<anatablo>()
                .Property(e => e.kullaniciid)
                .IsUnicode(false);

            modelBuilder.Entity<cihaz>()
                .HasMany(e => e.anatablo)
                .WithRequired(e => e.cihaz)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<firma>()
                .Property(e => e.firmaadi)
                .IsUnicode(false);

            modelBuilder.Entity<firma>()
                .HasMany(e => e.anatablo)
                .WithRequired(e => e.firma)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<firma>()
                .HasMany(e => e.parkyeri)
                .WithRequired(e => e.firma)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<il>()
                .Property(e => e.iladi)
                .IsUnicode(false);

            modelBuilder.Entity<il>()
                .HasMany(e => e.firma)
                .WithRequired(e => e.il)
                .HasForeignKey(e => e.ilid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<il>()
                .HasMany(e => e.ilce)
                .WithRequired(e => e.il)
                .HasForeignKey(e => e.ilid)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ilce>()
                .Property(e => e.ilceadi)
                .IsUnicode(false);

            modelBuilder.Entity<ilce>()
                .HasMany(e => e.firma)
                .WithRequired(e => e.ilce)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<kullanici>()
                .Property(e => e.kullaniciid)
                .IsUnicode(false);

            modelBuilder.Entity<kullanici>()
                .Property(e => e.sifre)
                .IsUnicode(false);

            modelBuilder.Entity<kullanici>()
                .HasMany(e => e.anatablo)
                .WithRequired(e => e.kullanici)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<parkyeri>()
                .Property(e => e.parkadi)
                .IsUnicode(false);

            modelBuilder.Entity<parkyeri>()
                .HasMany(e => e.anatablo)
                .WithRequired(e => e.parkyeri)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<parkyeri>()
                .HasMany(e => e.cihaz)
                .WithRequired(e => e.parkyeri)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<yetki>()
                .Property(e => e.yetki1)
                .IsUnicode(false);

            modelBuilder.Entity<yetki>()
                .HasMany(e => e.yetkili)
                .WithRequired(e => e.yetki)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<yetkili>()
                .Property(e => e.yetkiliadi)
                .IsUnicode(false);

            modelBuilder.Entity<yetkili>()
                .HasMany(e => e.firma)
                .WithRequired(e => e.yetkili)
                .WillCascadeOnDelete(false);
        }
    }
}
