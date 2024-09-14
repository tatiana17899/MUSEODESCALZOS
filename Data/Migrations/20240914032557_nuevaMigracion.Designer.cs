﻿// <auto-generated />
using System;
using MUSEODESCALZOS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240914032557_nuevaMigracion")]
    partial class nuevaMigracion
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Actividades", b =>
                {
                    b.Property<long>("IDActividades")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDActividades"));

                    b.Property<string>("Actividad")
                        .HasColumnType("text");

                    b.Property<long?>("GuíaIDGuía")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Hora_Final")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Hora_Inicial")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("IDEvento")
                        .HasColumnType("bigint");

                    b.HasKey("IDActividades");

                    b.HasIndex("GuíaIDGuía");

                    b.HasIndex("IDEvento");

                    b.ToTable("tb_Actividades");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Alquiler", b =>
                {
                    b.Property<long>("IDAlquileres")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDAlquileres"));

                    b.Property<int>("Capacidad")
                        .HasColumnType("integer");

                    b.Property<string>("Caracteristicas")
                        .HasColumnType("text");

                    b.Property<string>("Descripcion")
                        .HasColumnType("text");

                    b.Property<bool>("Disponible")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Hora_Disponible")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Titulo")
                        .HasColumnType("text");

                    b.HasKey("IDAlquileres");

                    b.ToTable("tb_Alquiler");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Calificación_Noticia", b =>
                {
                    b.Property<long>("IDCalifNoticia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDCalifNoticia"));

                    b.Property<long>("IDCalifcacion")
                        .HasColumnType("bigint");

                    b.Property<long>("IDNoticia")
                        .HasColumnType("bigint");

                    b.Property<long>("IDUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("Tipo")
                        .HasColumnType("text");

                    b.Property<long?>("calificación_NoticiaIDCalifNoticia")
                        .HasColumnType("bigint");

                    b.HasKey("IDCalifNoticia");

                    b.HasIndex("IDNoticia")
                        .IsUnique();

                    b.HasIndex("IDUsuario")
                        .IsUnique();

                    b.HasIndex("calificación_NoticiaIDCalifNoticia");

                    b.ToTable("tb_Calificacion");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Cliente", b =>
                {
                    b.Property<long>("IDCliente")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDCliente"));

                    b.Property<string>("Apellidos")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("IDUsuario")
                        .HasColumnType("bigint");

                    b.Property<string>("Nombres")
                        .HasColumnType("text");

                    b.Property<string>("NumDoc")
                        .HasColumnType("text");

                    b.Property<string>("NumTarjeta")
                        .HasColumnType("text");

                    b.Property<string>("Pais")
                        .HasColumnType("text");

                    b.Property<string>("TipoDoc")
                        .HasColumnType("text");

                    b.Property<string>("Titular")
                        .HasColumnType("text");

                    b.HasKey("IDCliente");

                    b.HasIndex("IDUsuario")
                        .IsUnique();

                    b.ToTable("tb_Cliente");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Evento", b =>
                {
                    b.Property<long>("IDEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDEvento"));

                    b.Property<int>("Capacidad")
                        .HasColumnType("integer");

                    b.Property<string>("Descripción")
                        .HasColumnType("text");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Nombre")
                        .HasColumnType("text");

                    b.Property<string>("NombreImagen")
                        .HasColumnType("text");

                    b.Property<decimal>("Precio")
                        .HasColumnType("numeric");

                    b.Property<string>("RutalImagen")
                        .HasColumnType("text");

                    b.HasKey("IDEvento");

                    b.ToTable("tb_Evento");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Guía", b =>
                {
                    b.Property<long>("IDGuía")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDGuía"));

                    b.Property<string>("Apellidos")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Nombres")
                        .HasColumnType("text");

                    b.Property<decimal>("Sueldo")
                        .HasColumnType("numeric");

                    b.Property<string>("TipPago")
                        .HasColumnType("text");

                    b.HasKey("IDGuía");

                    b.ToTable("tb_Guía");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Imagen_Alquiler", b =>
                {
                    b.Property<long>("IDimgAlquiler")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDimgAlquiler"));

                    b.Property<long>("IDAlquiler")
                        .HasColumnType("bigint");

                    b.Property<string>("Rutalmagen")
                        .HasColumnType("text");

                    b.HasKey("IDimgAlquiler");

                    b.HasIndex("IDAlquiler");

                    b.ToTable("tb_ImagenAlquiler");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Noticia", b =>
                {
                    b.Property<long>("IDNoticia")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDNoticia"));

                    b.Property<string>("Descripción")
                        .HasColumnType("text");

                    b.Property<DateTime>("FechaPublicación")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Rutalmagen")
                        .HasColumnType("text");

                    b.Property<string>("Titulo")
                        .HasColumnType("text");

                    b.HasKey("IDNoticia");

                    b.ToTable("tb_Noticia");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.PedidoAlquiler", b =>
                {
                    b.Property<long>("IDPedidoAlq")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDPedidoAlq"));

                    b.Property<int>("CantPersona")
                        .HasColumnType("integer");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Hora_Fin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Hora_Inicio")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("IDAlquiler")
                        .HasColumnType("bigint");

                    b.Property<long>("IDCliente")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PrecioTotal")
                        .HasColumnType("numeric");

                    b.HasKey("IDPedidoAlq");

                    b.HasIndex("IDAlquiler")
                        .IsUnique();

                    b.HasIndex("IDCliente");

                    b.ToTable("tb_PedidoAlquiler");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.PedidoEvento", b =>
                {
                    b.Property<long>("IDPedidoEvento")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDPedidoEvento"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<string>("Detalle")
                        .HasColumnType("text");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("IDCliente")
                        .HasColumnType("bigint");

                    b.Property<long>("IDEvento")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PrecioTotal")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.HasKey("IDPedidoEvento");

                    b.HasIndex("IDCliente");

                    b.HasIndex("IDEvento")
                        .IsUnique();

                    b.ToTable("tb_PedidoEvento");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.PedidoVisita", b =>
                {
                    b.Property<long>("IDPedidoVisit")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDPedidoVisit"));

                    b.Property<int>("Cantidad")
                        .HasColumnType("integer");

                    b.Property<string>("Detalle")
                        .HasColumnType("text");

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long>("IDCliente")
                        .HasColumnType("bigint");

                    b.Property<long>("IDGuía")
                        .HasColumnType("bigint");

                    b.Property<decimal>("PrecioTotal")
                        .HasColumnType("numeric");

                    b.Property<decimal>("PrecioUnitario")
                        .HasColumnType("numeric");

                    b.HasKey("IDPedidoVisit");

                    b.HasIndex("IDCliente");

                    b.HasIndex("IDGuía")
                        .IsUnique();

                    b.ToTable("tb_PedidoVisita");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Usuario", b =>
                {
                    b.Property<long>("IDUsuario")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IDUsuario"));

                    b.Property<string>("Contraseña")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Reestablecer")
                        .HasColumnType("text");

                    b.Property<string>("Rutalmagen")
                        .HasColumnType("text");

                    b.Property<string>("usuario")
                        .HasColumnType("text");

                    b.HasKey("IDUsuario");

                    b.ToTable("tb_Usuario");
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

            modelBuilder.Entity("MuseoDescalzos.Models.Actividades", b =>
                {
                    b.HasOne("MuseoDescalzos.Models.Guía", "Guía")
                        .WithMany("Actividades")
                        .HasForeignKey("GuíaIDGuía");

                    b.HasOne("MuseoDescalzos.Models.Evento", "Evento")
                        .WithMany("Actividades")
                        .HasForeignKey("IDEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Evento");

                    b.Navigation("Guía");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Calificación_Noticia", b =>
                {
                    b.HasOne("MuseoDescalzos.Models.Noticia", "Noticia")
                        .WithOne("Calificación_Noticia")
                        .HasForeignKey("MuseoDescalzos.Models.Calificación_Noticia", "IDNoticia")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MuseoDescalzos.Models.Usuario", "Usuario")
                        .WithOne("Calificación_Noticia")
                        .HasForeignKey("MuseoDescalzos.Models.Calificación_Noticia", "IDUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MuseoDescalzos.Models.Calificación_Noticia", "calificación_Noticia")
                        .WithMany()
                        .HasForeignKey("calificación_NoticiaIDCalifNoticia");

                    b.Navigation("Noticia");

                    b.Navigation("Usuario");

                    b.Navigation("calificación_Noticia");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Cliente", b =>
                {
                    b.HasOne("MuseoDescalzos.Models.Usuario", "Usuario")
                        .WithOne("Cliente")
                        .HasForeignKey("MuseoDescalzos.Models.Cliente", "IDUsuario")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuario");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Imagen_Alquiler", b =>
                {
                    b.HasOne("MuseoDescalzos.Models.Alquiler", "Alquiler")
                        .WithMany("Imagenes")
                        .HasForeignKey("IDAlquiler")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alquiler");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.PedidoAlquiler", b =>
                {
                    b.HasOne("MuseoDescalzos.Models.Alquiler", "Alquiler")
                        .WithOne("PedidoAlquiler")
                        .HasForeignKey("MuseoDescalzos.Models.PedidoAlquiler", "IDAlquiler")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MuseoDescalzos.Models.Cliente", "Cliente")
                        .WithMany("PedidoAlquiler")
                        .HasForeignKey("IDCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alquiler");

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.PedidoEvento", b =>
                {
                    b.HasOne("MuseoDescalzos.Models.Cliente", "Cliente")
                        .WithMany("PedidoEvento")
                        .HasForeignKey("IDCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MuseoDescalzos.Models.Evento", "Evento")
                        .WithOne("PedidoEvento")
                        .HasForeignKey("MuseoDescalzos.Models.PedidoEvento", "IDEvento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Evento");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.PedidoVisita", b =>
                {
                    b.HasOne("MuseoDescalzos.Models.Cliente", "Cliente")
                        .WithMany("PedidoVisita")
                        .HasForeignKey("IDCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MuseoDescalzos.Models.Guía", "Guía")
                        .WithOne("PedidoVisita")
                        .HasForeignKey("MuseoDescalzos.Models.PedidoVisita", "IDGuía")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");

                    b.Navigation("Guía");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Alquiler", b =>
                {
                    b.Navigation("Imagenes");

                    b.Navigation("PedidoAlquiler")
                        .IsRequired();
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Cliente", b =>
                {
                    b.Navigation("PedidoAlquiler");

                    b.Navigation("PedidoEvento");

                    b.Navigation("PedidoVisita");
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Evento", b =>
                {
                    b.Navigation("Actividades");

                    b.Navigation("PedidoEvento")
                        .IsRequired();
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Guía", b =>
                {
                    b.Navigation("Actividades");

                    b.Navigation("PedidoVisita")
                        .IsRequired();
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Noticia", b =>
                {
                    b.Navigation("Calificación_Noticia")
                        .IsRequired();
                });

            modelBuilder.Entity("MuseoDescalzos.Models.Usuario", b =>
                {
                    b.Navigation("Calificación_Noticia")
                        .IsRequired();

                    b.Navigation("Cliente")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
