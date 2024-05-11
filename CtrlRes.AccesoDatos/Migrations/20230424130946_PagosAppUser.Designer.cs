﻿// <auto-generated />
using System;
using CtrlRes.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CtrlRes.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230424130946_PagosAppUser")]
    partial class PagosAppUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CtrlRes.Models.ArchivosPDF", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Archivo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ArchivosPDF");
                });

            modelBuilder.Entity("CtrlRes.Models.Arrendatarios", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehiculo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vivienda_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Vivienda_Id");

                    b.ToTable("Arrendatarios");
                });

            modelBuilder.Entity("CtrlRes.Models.Asambleas", b =>
                {
                    b.Property<string>("IdAsam")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Arrendatario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fecha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NomAsam")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Privada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Propietario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdAsam");

                    b.ToTable("Asambleas");
                });

            modelBuilder.Entity("CtrlRes.Models.Herramientas", b =>
                {
                    b.Property<string>("IdHerr")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Contenido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdHerr");

                    b.ToTable("Herramientas");
                });

            modelBuilder.Entity("CtrlRes.Models.Lecturas", b =>
                {
                    b.Property<string>("Folio")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fecha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IdUsu")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Privada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipLec")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Folio");

                    b.ToTable("Lecturas");
                });

            modelBuilder.Entity("CtrlRes.Models.Mantenimientos", b =>
                {
                    b.Property<string>("Folio")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fecha")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Privada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Propietario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Folio");

                    b.ToTable("Mantenimientos");
                });

            modelBuilder.Entity("CtrlRes.Models.Mediciones", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Folio")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Fotografia")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Medicion")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Folio");

                    b.ToTable("Mediciones");
                });

            modelBuilder.Entity("CtrlRes.Models.Pagos", b =>
                {
                    b.Property<long>("Referencia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Referencia"));

                    b.Property<string>("Concepto")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaLimite")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaRegistro")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FechaRegistroPago")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Monto")
                        .HasColumnType("real");

                    b.Property<string>("Para")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vivienda_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Referencia");

                    b.HasIndex("Vivienda_Id");

                    b.ToTable("Pagos");
                });

            modelBuilder.Entity("CtrlRes.Models.Privadas", b =>
                {
                    b.Property<string>("IdPriv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("ArchivosPDFId")
                        .HasColumnType("int");

                    b.Property<int?>("ArchivosPDFIdCU")
                        .HasColumnType("int");

                    b.Property<string>("MedLuz")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observaciones")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdPriv");

                    b.HasIndex("ArchivosPDFId");

                    b.HasIndex("ArchivosPDFIdCU");

                    b.ToTable("Privadas");
                });

            modelBuilder.Entity("CtrlRes.Models.Propietarios", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Celular")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vehiculo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Vivienda_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Vivienda_Id");

                    b.ToTable("Propietarios");
                });

            modelBuilder.Entity("CtrlRes.Models.Proveedores", b =>
                {
                    b.Property<string>("IdProv")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Servicios")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdProv");

                    b.ToTable("Proveedores");
                });

            modelBuilder.Entity("CtrlRes.Models.Sanciones", b =>
                {
                    b.Property<string>("IdSan")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sancion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdSan");

                    b.ToTable("Sanciones");
                });

            modelBuilder.Entity("CtrlRes.Models.TAGs", b =>
                {
                    b.Property<string>("TAG")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Privada")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Propietario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TAG");

                    b.ToTable("TAGs");
                });

            modelBuilder.Entity("CtrlRes.Models.Usuarios", b =>
                {
                    b.Property<string>("IdUsu")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Contrasena")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUsu");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("CtrlRes.Models.Viviendas", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("MedidorAgua")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Privada_Id")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Privada_Id");

                    b.ToTable("Viviendas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("CtrlRes.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Contrasena")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PropOrArr_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UsuarioTipo")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ApplicationUser");
                });

            modelBuilder.Entity("CtrlRes.Models.Arrendatarios", b =>
                {
                    b.HasOne("CtrlRes.Models.Viviendas", "Viviendas")
                        .WithMany()
                        .HasForeignKey("Vivienda_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Viviendas");
                });

            modelBuilder.Entity("CtrlRes.Models.Mediciones", b =>
                {
                    b.HasOne("CtrlRes.Models.Lecturas", "Lecturas")
                        .WithMany()
                        .HasForeignKey("Folio")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Lecturas");
                });

            modelBuilder.Entity("CtrlRes.Models.Pagos", b =>
                {
                    b.HasOne("CtrlRes.Models.Viviendas", "Viviendas")
                        .WithMany()
                        .HasForeignKey("Vivienda_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Viviendas");
                });

            modelBuilder.Entity("CtrlRes.Models.Privadas", b =>
                {
                    b.HasOne("CtrlRes.Models.ArchivosPDF", "ArchivosPDF")
                        .WithMany()
                        .HasForeignKey("ArchivosPDFId");

                    b.HasOne("CtrlRes.Models.ArchivosPDF", "ArchivosPDFCU")
                        .WithMany()
                        .HasForeignKey("ArchivosPDFIdCU");

                    b.Navigation("ArchivosPDF");

                    b.Navigation("ArchivosPDFCU");
                });

            modelBuilder.Entity("CtrlRes.Models.Propietarios", b =>
                {
                    b.HasOne("CtrlRes.Models.Viviendas", "Viviendas")
                        .WithMany()
                        .HasForeignKey("Vivienda_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Viviendas");
                });

            modelBuilder.Entity("CtrlRes.Models.Viviendas", b =>
                {
                    b.HasOne("CtrlRes.Models.Privadas", "Privadas")
                        .WithMany()
                        .HasForeignKey("Privada_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Privadas");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
