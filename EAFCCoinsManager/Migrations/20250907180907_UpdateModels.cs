using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EAFCCoinsManager.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_item_carrinho_carrinho_Carrinhoid_carrinho",
                table: "item_carrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_item_carrinho_moeda_Moedaid_moeda",
                table: "item_carrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_item_pedido_moeda_Moedaid_moeda",
                table: "item_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_item_pedido_pedido_Pedidoid_pedido",
                table: "item_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_moeda_plataforma_Plataformaid_plataforma",
                table: "moeda");

            migrationBuilder.DropForeignKey(
                name: "FK_pedido_usuarios_Usuariosid",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_pedido_Usuariosid",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_moeda_Plataformaid_plataforma",
                table: "moeda");

            migrationBuilder.DropIndex(
                name: "IX_item_pedido_Moedaid_moeda",
                table: "item_pedido");

            migrationBuilder.DropIndex(
                name: "IX_item_pedido_Pedidoid_pedido",
                table: "item_pedido");

            migrationBuilder.DropIndex(
                name: "IX_item_carrinho_Carrinhoid_carrinho",
                table: "item_carrinho");

            migrationBuilder.DropIndex(
                name: "IX_item_carrinho_Moedaid_moeda",
                table: "item_carrinho");

            migrationBuilder.DropColumn(
                name: "Usuariosid",
                table: "pedido");

            migrationBuilder.DropColumn(
                name: "Plataformaid_plataforma",
                table: "moeda");

            migrationBuilder.DropColumn(
                name: "Moedaid_moeda",
                table: "item_pedido");

            migrationBuilder.DropColumn(
                name: "Pedidoid_pedido",
                table: "item_pedido");

            migrationBuilder.DropColumn(
                name: "Carrinhoid_carrinho",
                table: "item_carrinho");

            migrationBuilder.DropColumn(
                name: "Moedaid_moeda",
                table: "item_carrinho");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_id_user",
                table: "pedido",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "IX_moeda_plataforma_id",
                table: "moeda",
                column: "plataforma_id");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_id_moeda",
                table: "item_pedido",
                column: "id_moeda");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_id_pedido",
                table: "item_pedido",
                column: "id_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_item_carrinho_id_carrinho",
                table: "item_carrinho",
                column: "id_carrinho");

            migrationBuilder.CreateIndex(
                name: "IX_item_carrinho_id_moeda",
                table: "item_carrinho",
                column: "id_moeda");

            migrationBuilder.AddForeignKey(
                name: "FK_item_carrinho_carrinho_id_carrinho",
                table: "item_carrinho",
                column: "id_carrinho",
                principalTable: "carrinho",
                principalColumn: "id_carrinho",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_item_carrinho_moeda_id_moeda",
                table: "item_carrinho",
                column: "id_moeda",
                principalTable: "moeda",
                principalColumn: "id_moeda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_item_pedido_moeda_id_moeda",
                table: "item_pedido",
                column: "id_moeda",
                principalTable: "moeda",
                principalColumn: "id_moeda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_item_pedido_pedido_id_pedido",
                table: "item_pedido",
                column: "id_pedido",
                principalTable: "pedido",
                principalColumn: "id_pedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_moeda_plataforma_plataforma_id",
                table: "moeda",
                column: "plataforma_id",
                principalTable: "plataforma",
                principalColumn: "id_plataforma",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_usuarios_id_user",
                table: "pedido",
                column: "id_user",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_item_carrinho_carrinho_id_carrinho",
                table: "item_carrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_item_carrinho_moeda_id_moeda",
                table: "item_carrinho");

            migrationBuilder.DropForeignKey(
                name: "FK_item_pedido_moeda_id_moeda",
                table: "item_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_item_pedido_pedido_id_pedido",
                table: "item_pedido");

            migrationBuilder.DropForeignKey(
                name: "FK_moeda_plataforma_plataforma_id",
                table: "moeda");

            migrationBuilder.DropForeignKey(
                name: "FK_pedido_usuarios_id_user",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_pedido_id_user",
                table: "pedido");

            migrationBuilder.DropIndex(
                name: "IX_moeda_plataforma_id",
                table: "moeda");

            migrationBuilder.DropIndex(
                name: "IX_item_pedido_id_moeda",
                table: "item_pedido");

            migrationBuilder.DropIndex(
                name: "IX_item_pedido_id_pedido",
                table: "item_pedido");

            migrationBuilder.DropIndex(
                name: "IX_item_carrinho_id_carrinho",
                table: "item_carrinho");

            migrationBuilder.DropIndex(
                name: "IX_item_carrinho_id_moeda",
                table: "item_carrinho");

            migrationBuilder.AddColumn<int>(
                name: "Usuariosid",
                table: "pedido",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Plataformaid_plataforma",
                table: "moeda",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Moedaid_moeda",
                table: "item_pedido",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pedidoid_pedido",
                table: "item_pedido",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Carrinhoid_carrinho",
                table: "item_carrinho",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Moedaid_moeda",
                table: "item_carrinho",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_pedido_Usuariosid",
                table: "pedido",
                column: "Usuariosid");

            migrationBuilder.CreateIndex(
                name: "IX_moeda_Plataformaid_plataforma",
                table: "moeda",
                column: "Plataformaid_plataforma");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_Moedaid_moeda",
                table: "item_pedido",
                column: "Moedaid_moeda");

            migrationBuilder.CreateIndex(
                name: "IX_item_pedido_Pedidoid_pedido",
                table: "item_pedido",
                column: "Pedidoid_pedido");

            migrationBuilder.CreateIndex(
                name: "IX_item_carrinho_Carrinhoid_carrinho",
                table: "item_carrinho",
                column: "Carrinhoid_carrinho");

            migrationBuilder.CreateIndex(
                name: "IX_item_carrinho_Moedaid_moeda",
                table: "item_carrinho",
                column: "Moedaid_moeda");

            migrationBuilder.AddForeignKey(
                name: "FK_item_carrinho_carrinho_Carrinhoid_carrinho",
                table: "item_carrinho",
                column: "Carrinhoid_carrinho",
                principalTable: "carrinho",
                principalColumn: "id_carrinho",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_item_carrinho_moeda_Moedaid_moeda",
                table: "item_carrinho",
                column: "Moedaid_moeda",
                principalTable: "moeda",
                principalColumn: "id_moeda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_item_pedido_moeda_Moedaid_moeda",
                table: "item_pedido",
                column: "Moedaid_moeda",
                principalTable: "moeda",
                principalColumn: "id_moeda",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_item_pedido_pedido_Pedidoid_pedido",
                table: "item_pedido",
                column: "Pedidoid_pedido",
                principalTable: "pedido",
                principalColumn: "id_pedido",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_moeda_plataforma_Plataformaid_plataforma",
                table: "moeda",
                column: "Plataformaid_plataforma",
                principalTable: "plataforma",
                principalColumn: "id_plataforma");

            migrationBuilder.AddForeignKey(
                name: "FK_pedido_usuarios_Usuariosid",
                table: "pedido",
                column: "Usuariosid",
                principalTable: "usuarios",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
