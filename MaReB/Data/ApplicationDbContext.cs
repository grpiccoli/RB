using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MaReB.Models;
using Microsoft.AspNetCore.Identity;

namespace MaReB.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<ApplicationRole>()
                .HasMany(e => e.Users)
                .WithOne()
                .HasForeignKey(e => e.RoleId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<ApplicationUser>()
                .HasMany(e => e.Roles)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Procedencia>()
                .HasMany(e => e.Capturas)
                .WithOne()
                .HasForeignKey(e => e.ProcedenciaId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<IdentityUserClaim<string>> IdentityUserClaims { get; set; }
        public DbSet<IdentityUserRole<string>> IdentityUserRoles { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        public DbSet<ApplicationRole> ApplicationRole { get; set; }

        public DbSet<AppUserRole> AppUserRole { get; set; }

        public DbSet<Captura> Capturas { get; set; }

        public DbSet<Continent> Continent { get; set; }

        public DbSet<Country> Country { get; set; }

        public DbSet<Arrival> Arrival { get; set; }

        public DbSet<Export> Export { get; set; }

        public DbSet<Coordinate> Coordinate { get; set; }

        public DbSet<Region> Region { get; set; }

        public DbSet<Procedencia> Procedencias { get; set; }

        public DbSet<Provincia> Provincia { get; set; }

        public DbSet<Puerto> Puertos { get; set; }

        public DbSet<Comuna> Comuna { get; set; }

        public DbSet<Station> Station { get; set; }
    }
}
