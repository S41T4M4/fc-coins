using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly ConnectionContext _connectionContext;

        public PedidoRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public async Task<Pedido> GetByIdAsync(int id)
        {
            return await _connectionContext.Pedido
                .Include(p => p.Usuarios)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Moeda)
                        .ThenInclude(m => m.Plataforma)
                .Include(p => p.Pagamento)
                .FirstOrDefaultAsync(p => p.id_pedido == id);
        }

        public async Task<List<Pedido>> GetPedidoByUser(int userId)
        {
            return await _connectionContext.Pedido
                .Include(p => p.Usuarios)
                .Include(p => p.Itens)
                    .ThenInclude(i => i.Moeda)
                        .ThenInclude(m => m.Plataforma)
                .Include(p => p.Pagamento)
                .Where(p => p.id_user == userId)
                .OrderByDescending(p => p.data_pedido)
                .ToListAsync();
        }

        public async Task<Pedido> AddPedidoAsync(Pedido pedido)
        {
            _connectionContext.Pedido.Add(pedido);
            await _connectionContext.SaveChangesAsync();
            return pedido;
        }

        public async Task<Pedido> AddNewItemPedido(ItemPedido itemPedido)
        {
            _connectionContext.ItemPedido.Add(itemPedido);
            await _connectionContext.SaveChangesAsync();
            
            // Retorna o pedido atualizado
            return await GetByIdAsync(itemPedido.id_pedido);
        }

        public async Task<Pedido> RemoveItemPedido(int id_item)
        {
            var item = await _connectionContext.ItemPedido.FindAsync(id_item);
            if (item != null)
            {
                var pedidoId = item.id_pedido;
                _connectionContext.ItemPedido.Remove(item);
                await _connectionContext.SaveChangesAsync();
                
                return await GetByIdAsync(pedidoId);
            }
            return null;
        }
    }
}
