using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<Carrinho?> GetById(int id);
        Task<Carrinho?> GetByUserId(int id);
        Task<List<Carrinho>> GetAll();
        Task CreateCarrinho(Carrinho carrinho);
        Task RemoveItemCarrinho(int id_item);
        Task AddItemCarrinho(ItemCarrinho itemCarrinho);
        Task<ItemCarrinho?> GetItemById(int idItem);
        Task UpdateItemQuantity(int idItem, int novaQuantidade);
    }
}
