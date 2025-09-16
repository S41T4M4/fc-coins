using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAFCCoinsManager.Migrations
{
    /// <inheritdoc />
    public partial class AddMissingColumnsToPlataforma : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ativa",
                table: "plataforma",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "taxa_por_100k",
                table: "plataforma",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "VendedorOfertaid_oferta",
                table: "pedido",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "vendedor_oferta",
                columns: table => new
                {
                    id_oferta = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_vendedor = table.Column<int>(type: "integer", nullable: false),
                    plataforma_id = table.Column<int>(type: "integer", nullable: false),
                    quantidade_coins = table.Column<decimal>(type: "numeric", nullable: false),
                    preco_por_100k = table.Column<decimal>(type: "numeric", nullable: false),
                    taxa_plataforma = table.Column<decimal>(type: "numeric", nullable: false),
                    preco_final = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    data_criacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    data_atualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    data_venda = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vendedor_oferta", x => x.id_oferta);
                    table.ForeignKey(
                        name: "FK_vendedor_oferta_plataforma_plataforma_id",
                        column: x => x.plataforma_id,
                        principalTable: "plataforma",
                        principalColumn: "id_plataforma",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_vendedor_oferta_usuarios_id_vendedor",
                        column: x => x.id_vendedor,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pedido_VendedorOfertaid_oferta",
                table: "pedido",
                column: "VendedorOfertaid_oferta");

            migrationBuilder.CreateIndex(
                name: "IX_vendedor_oferta_id_vendedor",
                table: "vendedor_oferta",
                column: "id_vendedor");

            migrationBuilder.CreateIndex(
                name: "IX_vendedor_oferta_plataforma_id",
                table: "vendedor_oferta",
                column: "plataforma_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_vendedor_oferta_VendedorOfertaid_oferta",
                table: "pedido",
                column: "VendedorOfertaid_oferta",
                principalTable: "vendedor_oferta",
                principalColumn: "id_oferta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedido_vendedor_oferta_VendedorOfertaid_oferta",
                table: "pedido");

            migrationBuilder.DropTable(
                name: "vendedor_oferta");

            migrationBuilder.DropIndex(
                name: "IX_pedido_VendedorOfertaid_oferta",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "ativa",
                table: "plataforma");

            migrationBuilder.DropColumn(
                name: "taxa_por_100k",
                table: "plataforma");

            migrationBuilder.DropColumn(
                name: "VendedorOfertaid_oferta",
                table: "pedido");
        }
    }
}
