using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Infraestrutura
{
    public class ConnectionContext : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Carrinho> Carrinho { get; set; }   
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<ItemPedido> ItemPedido { get; set; }
        public DbSet<ItemCarrinho> ItemCarrinho { get; set; }
        public DbSet<Moeda> Moeda { get; set; }
        public DbSet<Plataforma> Plataforma { get; set; }
        public DbSet<Pagamento> Pagamento { get; set; }
        public DbSet<VendedorOferta> VendedorOfertas { get; set; }
       

        public ConnectionContext(DbContextOptions<ConnectionContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 🔹 Usuario 1:N Carrinho
            modelBuilder.Entity<Carrinho>()
                .HasOne(c => c.Usuario)
                .WithMany(u => u.Carrinhos)
                .HasForeignKey(c => c.id_user);

            // 🔹 Carrinho 1:N ItemCarrinho
            modelBuilder.Entity<ItemCarrinho>()
                .HasOne(ic => ic.Carrinho)
                .WithMany(c => c.Itens)
                .HasForeignKey(ic => ic.id_carrinho);

            // 🔹 Moeda 1:N ItemCarrinho
            modelBuilder.Entity<ItemCarrinho>()
                .HasOne(ic => ic.Moeda)
                .WithMany(m => m.ItensCarrinho)
                .HasForeignKey(ic => ic.id_moeda);

            // 🔹 Usuario 1:N Pedido
            modelBuilder.Entity<Pedido>()
                .HasOne(p => p.Usuarios)
                .WithMany(u => u.Pedidos)
                .HasForeignKey(p => p.id_user);

            // 🔹 Pedido 1:N ItemPedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Pedido)
                .WithMany(p => p.Itens)
                .HasForeignKey(ip => ip.id_pedido);

            // 🔹 Moeda 1:N ItemPedido
            modelBuilder.Entity<ItemPedido>()
                .HasOne(ip => ip.Moeda)
                .WithMany(m => m.ItensPedido)
                .HasForeignKey(ip => ip.id_moeda);

            // 🔹 Pedido 1:1 Pagamento
            modelBuilder.Entity<Pagamento>()
                .HasOne(p => p.Pedido)
                .WithOne(p => p.Pagamento)
                .HasForeignKey<Pagamento>(p => p.id_pedido);

            // 🔹 Plataforma 1:N Moeda
            modelBuilder.Entity<Moeda>()
                .HasOne(m => m.Plataforma)
                .WithMany(p => p.Moedas)
                .HasForeignKey(m => m.plataforma_id);

            // 🔹 Usuario 1:N VendedorOferta (Vendedor)
            modelBuilder.Entity<VendedorOferta>()
                .HasOne(vo => vo.Vendedor)
                .WithMany(u => u.OfertasVendedor)
                .HasForeignKey(vo => vo.id_vendedor);

            // 🔹 Plataforma 1:N VendedorOferta
            modelBuilder.Entity<VendedorOferta>()
                .HasOne(vo => vo.Plataforma)
                .WithMany(p => p.OfertasVendedores)
                .HasForeignKey(vo => vo.plataforma_id);
        }


        // String de conexão configurada no Program.cs via appsettings.json 
    }
}
