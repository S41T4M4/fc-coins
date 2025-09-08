using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly ConnectionContext _connectionContext;

        public CarrinhoRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public async Task AddItemCarrinho(ItemCarrinho itemCarrinho)
        {
            // Verificar se o carrinho existe
            var carrinhoExiste = await _connectionContext.Carrinho
                .AsNoTracking()
                .AnyAsync(c => c.id_carrinho == itemCarrinho.id_carrinho);

            if (!carrinhoExiste)
            {
                throw new Exception("Não é possivel adicionar item sem carrinho");
            }

            // Verificar se o item já existe no carrinho
            var itemExiste = await _connectionContext.ItemCarrinho
                .FirstOrDefaultAsync(i => 
                    i.id_moeda == itemCarrinho.id_moeda && 
                    i.id_carrinho == itemCarrinho.id_carrinho);
            
            if (itemExiste != null)
            {
                // Atualizar quantidade do item existente
                itemExiste.quantidade += itemCarrinho.quantidade;
                _connectionContext.ItemCarrinho.Update(itemExiste);
            }
            else
            {
                // Adicionar novo item
                _connectionContext.ItemCarrinho.Add(itemCarrinho);
            }
            
            await _connectionContext.SaveChangesAsync();
        }

        public Task CreateCarrinho(Carrinho carrinho)
        {
            _connectionContext.Carrinho.Add(carrinho);
            return _connectionContext.SaveChangesAsync();
        }

        public Task<List<Carrinho>> GetAll()
        {
            return _connectionContext.Carrinho.ToListAsync();
        }

        public Task<Carrinho?> GetById(int id)
        {
            return _connectionContext.Carrinho
                .Include(c => c.Usuario)
                .Include(c => c.Itens)
                    .ThenInclude(i => i.Moeda)
                        .ThenInclude(m => m.Plataforma)
                .FirstOrDefaultAsync(c => c.id_carrinho == id);
        }

        public async Task<Carrinho?> GetByUserId(int id)
        {
            var carrinho = await _connectionContext.Carrinho
                .Include(c => c.Usuario)
                .Include(c => c.Itens)
                    .ThenInclude(i => i.Moeda)
                        .ThenInclude(m => m.Plataforma)
                .FirstOrDefaultAsync(c => c.id_user == id);

            return carrinho;
        }

        public async Task RemoveItemCarrinho(int id_item)
        {
            var item = await _connectionContext.ItemCarrinho
                .FirstOrDefaultAsync(i => i.id_item == id_item);
            
            if (item != null)
            {
                _connectionContext.ItemCarrinho.Remove(item);
                await _connectionContext.SaveChangesAsync();
            }
        }

        public Task<ItemCarrinho?> GetItemById(int idItem)
        {
            return _connectionContext.ItemCarrinho
                .FirstOrDefaultAsync(i => i.id_item == idItem);
        }

        public async Task UpdateItemQuantity(int idItem, int novaQuantidade)
        {
            var item = await _connectionContext.ItemCarrinho
                .FirstOrDefaultAsync(i => i.id_item == idItem);
            
            if (item != null)
            {
                item.quantidade = novaQuantidade;
                _connectionContext.ItemCarrinho.Update(item);
                await _connectionContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Item não encontrado");
            }
        }

    }
}
