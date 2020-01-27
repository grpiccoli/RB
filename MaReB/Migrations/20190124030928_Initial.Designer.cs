﻿// <auto-generated />
using System;
using MaReB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MaReB.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190124030928_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MaReB.Models.ApplicationRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedDate");

                    b.Property<string>("Description");

                    b.Property<string>("IPAddress");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("MaReB.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("ProfileImageUrl");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MaReB.Models.Arrival", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Caleta");

                    b.Property<int>("ComunaId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("Kg");

                    b.Property<int>("Species");

                    b.HasKey("Id");

                    b.HasIndex("ComunaId");

                    b.ToTable("Arrival");
                });

            modelBuilder.Entity("MaReB.Models.Captura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Cantidad");

                    b.Property<int>("Destino");

                    b.Property<int>("EmbarcacionId");

                    b.Property<DateTime>("Fecha");

                    b.Property<int>("Horas_Buceo");

                    b.Property<int>("N_Buzos");

                    b.Property<int>("Precio");

                    b.Property<int>("ProcedenciaId");

                    b.Property<int?>("ProcedenciaId1");

                    b.Property<int>("PuertoId");

                    b.HasKey("Id");

                    b.HasIndex("ProcedenciaId");

                    b.HasIndex("ProcedenciaId1");

                    b.HasIndex("PuertoId");

                    b.ToTable("Capturas");
                });

            modelBuilder.Entity("MaReB.Models.Comuna", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("CS");

                    b.Property<int>("DE");

                    b.Property<string>("Name");

                    b.Property<int>("ProvinciaId");

                    b.HasKey("Id");

                    b.HasIndex("ProvinciaId");

                    b.ToTable("Comuna");
                });

            modelBuilder.Entity("MaReB.Models.Continent", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("ISO");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Continent");
                });

            modelBuilder.Entity("MaReB.Models.Coordinate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ComunaId");

                    b.Property<int?>("CountryId");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int?>("ProcedenciaId");

                    b.Property<int?>("ProvinciaId");

                    b.Property<int?>("RegionId");

                    b.Property<string>("StationId");

                    b.Property<int>("Vertex");

                    b.HasKey("Id");

                    b.HasIndex("ComunaId");

                    b.HasIndex("CountryId");

                    b.HasIndex("ProcedenciaId")
                        .IsUnique()
                        .HasFilter("[ProcedenciaId] IS NOT NULL");

                    b.HasIndex("ProvinciaId");

                    b.HasIndex("RegionId");

                    b.HasIndex("StationId");

                    b.ToTable("Coordinate");
                });

            modelBuilder.Entity("MaReB.Models.Country", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Capital");

                    b.Property<int>("ContinentId");

                    b.Property<string>("ISO2");

                    b.Property<string>("ISO3");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ContinentId");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("MaReB.Models.Export", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("FOB");

                    b.Property<int>("Kg");

                    b.Property<int>("Processing");

                    b.Property<int>("RegionId");

                    b.Property<int>("Species");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("RegionId");

                    b.ToTable("Export");
                });

            modelBuilder.Entity("MaReB.Models.Procedencia", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.Property<string>("Observaciones");

                    b.Property<int>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Procedencias");
                });

            modelBuilder.Entity("MaReB.Models.Provincia", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.Property<int>("Population");

                    b.Property<int>("RegionId");

                    b.Property<int>("Surface");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Provincia");
                });

            modelBuilder.Entity("MaReB.Models.Puerto", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("ComunaId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ComunaId");

                    b.ToTable("Puertos");
                });

            modelBuilder.Entity("MaReB.Models.Region", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("MapCode");

                    b.Property<string>("Name");

                    b.Property<int>("Pop2002");

                    b.Property<int>("Pop2010");

                    b.Property<int>("Surface");

                    b.HasKey("Id");

                    b.ToTable("Region");
                });

            modelBuilder.Entity("MaReB.Models.Station", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("Area");

                    b.Property<string>("Name");

                    b.Property<int>("RegionId");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Station");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApplicationRoleId");

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationRoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUserRole<string>");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MaReB.Models.AppUserRole", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUserRole<string>");

                    b.Property<string>("RoleAssigner");

                    b.HasDiscriminator().HasValue("AppUserRole");
                });

            modelBuilder.Entity("MaReB.Models.Arrival", b =>
                {
                    b.HasOne("MaReB.Models.Comuna", "Comuna")
                        .WithMany("Arrivals")
                        .HasForeignKey("ComunaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Captura", b =>
                {
                    b.HasOne("MaReB.Models.Procedencia")
                        .WithMany("Capturas")
                        .HasForeignKey("ProcedenciaId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MaReB.Models.Procedencia", "Procedencia")
                        .WithMany()
                        .HasForeignKey("ProcedenciaId1");

                    b.HasOne("MaReB.Models.Puerto", "Puerto")
                        .WithMany()
                        .HasForeignKey("PuertoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Comuna", b =>
                {
                    b.HasOne("MaReB.Models.Provincia", "Provincia")
                        .WithMany("Comunas")
                        .HasForeignKey("ProvinciaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Coordinate", b =>
                {
                    b.HasOne("MaReB.Models.Comuna", "Comuna")
                        .WithMany("Coordinates")
                        .HasForeignKey("ComunaId");

                    b.HasOne("MaReB.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId");

                    b.HasOne("MaReB.Models.Procedencia", "Procedencia")
                        .WithOne("Coordinate")
                        .HasForeignKey("MaReB.Models.Coordinate", "ProcedenciaId");

                    b.HasOne("MaReB.Models.Provincia", "Provincia")
                        .WithMany("Coordinates")
                        .HasForeignKey("ProvinciaId");

                    b.HasOne("MaReB.Models.Region", "Region")
                        .WithMany("Coordinates")
                        .HasForeignKey("RegionId");

                    b.HasOne("MaReB.Models.Station", "Station")
                        .WithMany("Coordinates")
                        .HasForeignKey("StationId");
                });

            modelBuilder.Entity("MaReB.Models.Country", b =>
                {
                    b.HasOne("MaReB.Models.Continent", "Continent")
                        .WithMany("Countries")
                        .HasForeignKey("ContinentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Export", b =>
                {
                    b.HasOne("MaReB.Models.Country", "Country")
                        .WithMany("Exports")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MaReB.Models.Region", "Region")
                        .WithMany("Exports")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Procedencia", b =>
                {
                    b.HasOne("MaReB.Models.Region", "Region")
                        .WithMany()
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Provincia", b =>
                {
                    b.HasOne("MaReB.Models.Region", "Region")
                        .WithMany("Provincias")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Puerto", b =>
                {
                    b.HasOne("MaReB.Models.Comuna", "Comuna")
                        .WithMany("Puertos")
                        .HasForeignKey("ComunaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MaReB.Models.Station", b =>
                {
                    b.HasOne("MaReB.Models.Region", "Region")
                        .WithMany("Stations")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("MaReB.Models.ApplicationRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MaReB.Models.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("ApplicationRoleId");

                    b.HasOne("MaReB.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MaReB.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("MaReB.Models.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MaReB.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MaReB.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
