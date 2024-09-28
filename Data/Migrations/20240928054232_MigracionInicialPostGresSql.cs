using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MUSEODESCALZOS.Data.Migrations
{
    /// <inheritdoc />
    public partial class MigracionInicialPostGresSql : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_contacto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Message = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_contacto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tb_Admin",
                columns: table => new
                {
                    IDAdministrador = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: true),
                    Apellidos = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    NumDoc = table.Column<string>(type: "text", nullable: true),
                    Distrito = table.Column<string>(type: "text", nullable: true),
                    Provincia = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: true),
                    Contraseña = table.Column<string>(type: "text", nullable: true),
                    Imagen = table.Column<string>(type: "text", nullable: true),
                    PasswordResetToken = table.Column<string>(type: "text", nullable: true),
                    ResetTokenExpiration = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Admin", x => x.IDAdministrador);
                });

            migrationBuilder.CreateTable(
                name: "tb_Alquiler",
                columns: table => new
                {
                    IDAlquileres = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Capacidad = table.Column<int>(type: "integer", nullable: false),
                    Caracteristicas = table.Column<string>(type: "text", nullable: true),
                    Hora_Disponible = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Alquiler", x => x.IDAlquileres);
                });

            migrationBuilder.CreateTable(
                name: "tb_Evento",
                columns: table => new
                {
                    IDEvento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: true),
                    Descripción = table.Column<string>(type: "text", nullable: true),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Precio = table.Column<decimal>(type: "numeric", nullable: false),
                    Capacidad = table.Column<int>(type: "integer", nullable: false),
                    NombreImagen = table.Column<string>(type: "text", nullable: true),
                    RutalImagen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Evento", x => x.IDEvento);
                });

            migrationBuilder.CreateTable(
                name: "tb_Guía",
                columns: table => new
                {
                    IDGuía = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: true),
                    Apellidos = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    TipPago = table.Column<string>(type: "text", nullable: true),
                    Sueldo = table.Column<decimal>(type: "numeric", nullable: false),
                    ContraseñaGenerada = table.Column<string>(type: "text", nullable: true),
                    Disponible = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Guía", x => x.IDGuía);
                });

            migrationBuilder.CreateTable(
                name: "tb_Noticia",
                columns: table => new
                {
                    IDNoticia = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: true),
                    Descripción = table.Column<string>(type: "text", nullable: true),
                    Rutalmagen = table.Column<string>(type: "text", nullable: true),
                    FechaPublicación = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Noticia", x => x.IDNoticia);
                });

            migrationBuilder.CreateTable(
                name: "tb_Usuario",
                columns: table => new
                {
                    IDUsuario = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    usuario = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Contraseña = table.Column<string>(type: "text", nullable: true),
                    Reestablecer = table.Column<string>(type: "text", nullable: true),
                    Rutalmagen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Usuario", x => x.IDUsuario);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_ImagenAlquiler",
                columns: table => new
                {
                    IDimgAlquiler = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDAlquiler = table.Column<long>(type: "bigint", nullable: false),
                    Rutalmagen = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_ImagenAlquiler", x => x.IDimgAlquiler);
                    table.ForeignKey(
                        name: "FK_tb_ImagenAlquiler_tb_Alquiler_IDAlquiler",
                        column: x => x.IDAlquiler,
                        principalTable: "tb_Alquiler",
                        principalColumn: "IDAlquileres",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Actividades",
                columns: table => new
                {
                    IDActividades = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Hora_Inicial = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Hora_Final = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Actividad = table.Column<string>(type: "text", nullable: true),
                    GuíaIDGuía = table.Column<long>(type: "bigint", nullable: true),
                    IDEvento = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Actividades", x => x.IDActividades);
                    table.ForeignKey(
                        name: "FK_tb_Actividades_tb_Evento_IDEvento",
                        column: x => x.IDEvento,
                        principalTable: "tb_Evento",
                        principalColumn: "IDEvento",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_Actividades_tb_Guía_GuíaIDGuía",
                        column: x => x.GuíaIDGuía,
                        principalTable: "tb_Guía",
                        principalColumn: "IDGuía");
                });

            migrationBuilder.CreateTable(
                name: "tb_Tarea",
                columns: table => new
                {
                    IDTarea = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GuíaID = table.Column<long>(type: "bigint", nullable: false),
                    Descripción = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Tarea", x => x.IDTarea);
                    table.ForeignKey(
                        name: "FK_tb_Tarea_tb_Guía_GuíaID",
                        column: x => x.GuíaID,
                        principalTable: "tb_Guía",
                        principalColumn: "IDGuía",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Calificacion",
                columns: table => new
                {
                    IDCalifNoticia = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDCalifcacion = table.Column<long>(type: "bigint", nullable: false),
                    calificación_NoticiaIDCalifNoticia = table.Column<long>(type: "bigint", nullable: true),
                    IDNoticia = table.Column<long>(type: "bigint", nullable: false),
                    IDUsuario = table.Column<long>(type: "bigint", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Calificacion", x => x.IDCalifNoticia);
                    table.ForeignKey(
                        name: "FK_tb_Calificacion_tb_Calificacion_calificación_NoticiaIDCalif~",
                        column: x => x.calificación_NoticiaIDCalifNoticia,
                        principalTable: "tb_Calificacion",
                        principalColumn: "IDCalifNoticia");
                    table.ForeignKey(
                        name: "FK_tb_Calificacion_tb_Noticia_IDNoticia",
                        column: x => x.IDNoticia,
                        principalTable: "tb_Noticia",
                        principalColumn: "IDNoticia",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_Calificacion_tb_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "tb_Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_Cliente",
                columns: table => new
                {
                    IDCliente = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombres = table.Column<string>(type: "text", nullable: true),
                    Apellidos = table.Column<string>(type: "text", nullable: true),
                    TipoDoc = table.Column<string>(type: "text", nullable: true),
                    NumDoc = table.Column<string>(type: "text", nullable: true),
                    Pais = table.Column<string>(type: "text", nullable: true),
                    NumTarjeta = table.Column<string>(type: "text", nullable: true),
                    Titular = table.Column<string>(type: "text", nullable: true),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IDUsuario = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_Cliente", x => x.IDCliente);
                    table.ForeignKey(
                        name: "FK_tb_Cliente_tb_Usuario_IDUsuario",
                        column: x => x.IDUsuario,
                        principalTable: "tb_Usuario",
                        principalColumn: "IDUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_PedidoAlquiler",
                columns: table => new
                {
                    IDPedidoAlq = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDCliente = table.Column<long>(type: "bigint", nullable: false),
                    IDAlquiler = table.Column<long>(type: "bigint", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Hora_Inicio = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Hora_Fin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CantPersona = table.Column<int>(type: "integer", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PedidoAlquiler", x => x.IDPedidoAlq);
                    table.ForeignKey(
                        name: "FK_tb_PedidoAlquiler_tb_Alquiler_IDAlquiler",
                        column: x => x.IDAlquiler,
                        principalTable: "tb_Alquiler",
                        principalColumn: "IDAlquileres",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_PedidoAlquiler_tb_Cliente_IDCliente",
                        column: x => x.IDCliente,
                        principalTable: "tb_Cliente",
                        principalColumn: "IDCliente",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_PedidoEvento",
                columns: table => new
                {
                    IDPedidoEvento = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDCliente = table.Column<long>(type: "bigint", nullable: false),
                    IDEvento = table.Column<long>(type: "bigint", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: true),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "numeric", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PedidoEvento", x => x.IDPedidoEvento);
                    table.ForeignKey(
                        name: "FK_tb_PedidoEvento_tb_Cliente_IDCliente",
                        column: x => x.IDCliente,
                        principalTable: "tb_Cliente",
                        principalColumn: "IDCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_PedidoEvento_tb_Evento_IDEvento",
                        column: x => x.IDEvento,
                        principalTable: "tb_Evento",
                        principalColumn: "IDEvento",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_PedidoVisita",
                columns: table => new
                {
                    IDPedidoVisit = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IDCliente = table.Column<long>(type: "bigint", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: true),
                    Cantidad = table.Column<int>(type: "integer", nullable: false),
                    PrecioUnitario = table.Column<decimal>(type: "numeric", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    PrecioTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    IDGuía = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_PedidoVisita", x => x.IDPedidoVisit);
                    table.ForeignKey(
                        name: "FK_tb_PedidoVisita_tb_Cliente_IDCliente",
                        column: x => x.IDCliente,
                        principalTable: "tb_Cliente",
                        principalColumn: "IDCliente",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_PedidoVisita_tb_Guía_IDGuía",
                        column: x => x.IDGuía,
                        principalTable: "tb_Guía",
                        principalColumn: "IDGuía",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Actividades_GuíaIDGuía",
                table: "tb_Actividades",
                column: "GuíaIDGuía");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Actividades_IDEvento",
                table: "tb_Actividades",
                column: "IDEvento");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Calificacion_calificación_NoticiaIDCalifNoticia",
                table: "tb_Calificacion",
                column: "calificación_NoticiaIDCalifNoticia");

            migrationBuilder.CreateIndex(
                name: "IX_tb_Calificacion_IDNoticia",
                table: "tb_Calificacion",
                column: "IDNoticia",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Calificacion_IDUsuario",
                table: "tb_Calificacion",
                column: "IDUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Cliente_IDUsuario",
                table: "tb_Cliente",
                column: "IDUsuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_ImagenAlquiler_IDAlquiler",
                table: "tb_ImagenAlquiler",
                column: "IDAlquiler");

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoAlquiler_IDAlquiler",
                table: "tb_PedidoAlquiler",
                column: "IDAlquiler",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoAlquiler_IDCliente",
                table: "tb_PedidoAlquiler",
                column: "IDCliente");

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoEvento_IDCliente",
                table: "tb_PedidoEvento",
                column: "IDCliente");

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoEvento_IDEvento",
                table: "tb_PedidoEvento",
                column: "IDEvento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoVisita_IDCliente",
                table: "tb_PedidoVisita",
                column: "IDCliente");

            migrationBuilder.CreateIndex(
                name: "IX_tb_PedidoVisita_IDGuía",
                table: "tb_PedidoVisita",
                column: "IDGuía",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tb_Tarea_GuíaID",
                table: "tb_Tarea",
                column: "GuíaID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "t_contacto");

            migrationBuilder.DropTable(
                name: "tb_Actividades");

            migrationBuilder.DropTable(
                name: "tb_Admin");

            migrationBuilder.DropTable(
                name: "tb_Calificacion");

            migrationBuilder.DropTable(
                name: "tb_ImagenAlquiler");

            migrationBuilder.DropTable(
                name: "tb_PedidoAlquiler");

            migrationBuilder.DropTable(
                name: "tb_PedidoEvento");

            migrationBuilder.DropTable(
                name: "tb_PedidoVisita");

            migrationBuilder.DropTable(
                name: "tb_Tarea");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "tb_Noticia");

            migrationBuilder.DropTable(
                name: "tb_Alquiler");

            migrationBuilder.DropTable(
                name: "tb_Evento");

            migrationBuilder.DropTable(
                name: "tb_Cliente");

            migrationBuilder.DropTable(
                name: "tb_Guía");

            migrationBuilder.DropTable(
                name: "tb_Usuario");
        }
    }
}
