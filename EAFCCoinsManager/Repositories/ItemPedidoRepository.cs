using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class ItemPedidoRepository : IItemPedido
    {
        private readonly ConnectionContext _connectionContext;

        public ItemPedidoRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public async Task<ItemPedido> AddItemPedido(ItemPedido itemPedido)
        {
            _connectionContext.ItemPedido.Add(itemPedido);
            await _connectionContext.SaveChangesAsync();
            return itemPedido;
        }
    }
}
