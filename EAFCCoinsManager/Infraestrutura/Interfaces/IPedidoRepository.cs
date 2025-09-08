using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface IPedidoRepository
    {
        Task<Pedido> GetByIdAsync(int id);
        Task<List<Pedido>> GetPedidoByUser(int userId);
        Task<Pedido> AddPedidoAsync(Pedido pedido);
        Task<Pedido> AddNewItemPedido(ItemPedido itemPedido);
        Task<Pedido> RemoveItemPedido(int id_pedido);

    }
}
