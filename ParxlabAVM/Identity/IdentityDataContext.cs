using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ParxlabAVM.Identity
{
    public class IdentityDataContext:IdentityDbContext<ApplicationUser>
    {
        public IdentityDataContext(): base("name = NewModel")
        {

        }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //Tablo adı değiştirmek için
            builder.Entity<ApplicationUser>().ToTable("kullanici");
            builder.Entity<IdentityRole>().ToTable("yetki");
            builder.Entity<IdentityUserRole>().ToTable("kullaniciyetkisi");
            builder.Entity<ApplicationUser>().Property(p => p.Id).HasColumnName("kullaniciid");
            builder.Entity<ApplicationUser>().Property(p => p.UserName).HasColumnName("kullaniciadi");
            builder.Entity<ApplicationUser>().Property(p => p.PasswordHash).HasColumnName("sifre");
            builder.Entity<ApplicationUser>().Property(p => p.PhoneNumber).HasColumnName("telefonnumarasi");



        }

    }
}