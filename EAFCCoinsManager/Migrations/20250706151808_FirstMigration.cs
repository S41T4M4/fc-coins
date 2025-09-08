using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace EAFCCoinsManager.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "transacoes");

            migrationBuilder.CreateTable(
                name: "carrinho",
                columns: table => new
                {
                    id_carrinho = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    create_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_carrinho", x => x.id_carrinho);
                    table.ForeignKey(
                        name: "FK_carrinho_usuarios_id_user",
                        column: x => x.id_user,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "pedido",
                columns: table => new
                {
                    id_pedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_user = table.Column<int>(type: "integer", nullable: false),
                    data_pedido = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    total = table.Column<decimal>(type: "numeric", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    Usuariosid = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido", x => x.id_pedido);
                    table.ForeignKey(
                        name: "FK_pedido_usuarios_Usuariosid",
                        column: x => x.Usuariosid,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "plataforma",
                columns: table => new
                {
                    id_plataforma = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    descricao_plataforma = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_plataforma", x => x.id_plataforma);
                });

            migrationBuilder.CreateTable(
                name: "pagamento",
                columns: table => new
                {
                    id_pagamento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_pedido = table.Column<int>(type: "integer", nullable: false),
                    data_pag = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    valor_pago = table.Column<decimal>(type: "numeric", nullable: false),
                    metodo = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    transaction_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pagamento", x => x.id_pagamento);
                    table.ForeignKey(
                        name: "FK_pagamento_pedido_id_pedido",
                        column: x => x.id_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "moeda",
                columns: table => new
                {
                    id_moeda = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    plataforma_id = table.Column<int>(type: "integer", nullable: false),
                    quantidade = table.Column<decimal>(type: "numeric", nullable: false),
                    valor = table.Column<decimal>(type: "numeric", nullable: false),
                    Plataformaid_plataforma = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_moeda", x => x.id_moeda);
                    table.ForeignKey(
                        name: "FK_moeda_plataforma_Plataformaid_plataforma",
                        column: x => x.Plataformaid_plataforma,
                        principalTable: "plataforma",
                        principalColumn: "id_plataforma");
                });

            migrationBuilder.CreateTable(
                name: "item_carrinho",
                columns: table => new
                {
                    id_item = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_carrinho = table.Column<int>(type: "integer", nullable: false),
                    id_moeda = table.Column<int>(type: "integer", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    Carrinhoid_carrinho = table.Column<int>(type: "integer", nullable: false),
                    Moedaid_moeda = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_carrinho", x => x.id_item);
                    table.ForeignKey(
                        name: "FK_item_carrinho_carrinho_Carrinhoid_carrinho",
                        column: x => x.Carrinhoid_carrinho,
                        principalTable: "carrinho",
                        principalColumn: "id_carrinho",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_carrinho_moeda_Moedaid_moeda",
                        column: x => x.Moedaid_moeda,
                        principalTable: "moeda",
                        principalColumn: "id_moeda",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "item_pedido",
                columns: table => new
                {
                    id_item_pedido = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_pedido = table.Column<int>(type: "integer", nullable: false),
                    id_moeda = table.Column<int>(type: "integer", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    Preco_unitario = table.Column<decimal>(type: "numeric", nullable: false),
                    Pedidoid_pedido = table.Column<int>(type: "integer", nullable: false),
                    Moedaid_moeda = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_pedido", x => x.id_item_pedido);
                    table.ForeignKey(
                        name: "FK_item_pedido_moeda_Moedaid_moeda",
                        column: x => x.Moedaid_moeda,
                        principalTable: "moeda",
                        principalColumn: "id_moeda",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_pedido_pedido_Pedidoid_pedido",
                        column: x => x.Pedidoid_pedido,
                        principalTable: "pedido",
                        principalColumn: "id_pedido",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_carrinho_id_user",
                table: "carrinho",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_item_carrinho_Carrinhoid_carrinho",
                table: "item_carrinho",
                column: "Carrinhoid_carrinho");

            migrationBuilder.CreateIndex(
                name: "IX_item_carrinho_Moedaid_moeda",
                table: "item_carrinho",
                column: "Moedaid_moeda");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_Moedaid_moeda",
                table: "item_pedido",
                column: "Moedaid_moeda");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_Pedidoid_pedido",
                table: "item_pedido",
                column: "Pedidoid_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_moeda_Plataformaid_plataforma",
                table: "moeda",
                column: "Plataformaid_plataforma");

            migrationBuilder.CreateIndex(
                name: "IX_pagamento_id_pedido",
                table: "pagamento",
                column: "id_pedido",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_pedido_Usuariosid",
                table: "pedido",
                column: "Usuariosid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_carrinho");

            migrationBuilder.DropTable(
                name: "item_pedido");

            migrationBuilder.DropTable(
                name: "pagamento");

            migrationBuilder.DropTable(
                name: "carrinho");

            migrationBuilder.DropTable(
                name: "moeda");

            migrationBuilder.DropTable(
                name: "pedido");

            migrationBuilder.DropTable(
                name: "plataforma");

            migrationBuilder.CreateTable(
                name: "transacoes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_usuario = table.Column<int>(type: "integer", nullable: false),
                    data_transacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    tipo = table.Column<string>(type: "text", nullable: false),
                    valor_total = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transacoes", x => x.id);
                    table.ForeignKey(
                        name: "FK_transacoes_usuarios_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "usuarios",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_transacoes_id_usuario",
                table: "transacoes",
                column: "id_usuario");
        }
    }
}
