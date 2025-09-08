using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface IItemPedido
    {
        Task<ItemPedido> AddItemPedido(ItemPedido itemPedido);


    }
}
