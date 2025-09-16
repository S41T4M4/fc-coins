using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class VendedorOfertaRepository : IVendedorOfertaRepository
    {
        private readonly ConnectionContext _context;

        public VendedorOfertaRepository(ConnectionContext context)
        {
            _context = context;
        }

        public async Task<List<VendedorOferta>> GetAllOfertasAsync()
        {
            return await _context.VendedorOfertas
                .Include(o => o.Vendedor)
                .Include(o => o.Plataforma)
                .Include(o => o.Pedidos)
                .ToListAsync();
        }

        public async Task<List<VendedorOferta>> GetOfertasByVendedorAsync(int idVendedor)
        {
            return await _context.VendedorOfertas
                .Include(o => o.Vendedor)
                .Include(o => o.Plataforma)
                .Include(o => o.Pedidos)
                .Where(o => o.id_vendedor == idVendedor)
                .OrderByDescending(o => o.data_criacao)
                .ToListAsync();
        }

        public async Task<List<VendedorOferta>> GetOfertasByPlataformaAsync(int idPlataforma)
        {
            return await _context.VendedorOfertas
                .Include(o => o.Vendedor)
                .Include(o => o.Plataforma)
                .Include(o => o.Pedidos)
                .Where(o => o.plataforma_id == idPlataforma && o.status == "ativo")
                .OrderBy(o => o.preco_por_100k)
                .ToListAsync();
        }

        public async Task<List<VendedorOferta>> GetOfertasAtivasAsync()
        {
            return await _context.VendedorOfertas
                .Include(o => o.Vendedor)
                .Include(o => o.Plataforma)
                .Include(o => o.Pedidos)
                .Where(o => o.status == "ativo")
                .OrderBy(o => o.preco_por_100k)
                .ToListAsync();
        }

        public async Task<VendedorOferta?> GetOfertaByIdAsync(int idOferta)
        {
            return await _context.VendedorOfertas
                .Include(o => o.Vendedor)
                .Include(o => o.Plataforma)
                .Include(o => o.Pedidos)
                .FirstOrDefaultAsync(o => o.id_oferta == idOferta);
        }

        public async Task<VendedorOferta> CreateOfertaAsync(VendedorOferta oferta)
        {
            // Calcular preço final
            var plataforma = await _context.Plataforma
                .FirstOrDefaultAsync(p => p.id_plataforma == oferta.plataforma_id);
            
            if (plataforma != null)
            {
                oferta.taxa_plataforma = plataforma.taxa_por_100k;
                oferta.preco_final = await CalcularPrecoFinalAsync(
                    oferta.quantidade_coins, 
                    oferta.preco_por_100k, 
                    oferta.taxa_plataforma
                );
            }

            oferta.data_criacao = DateTime.UtcNow;
            oferta.status = "ativo";

            _context.VendedorOfertas.Add(oferta);
            await _context.SaveChangesAsync();
            return oferta;
        }

        public async Task<VendedorOferta> UpdateOfertaAsync(VendedorOferta oferta)
        {
            oferta.data_atualizacao = DateTime.UtcNow;
            
            // Recalcular preço final se necessário
            var plataforma = await _context.Plataforma
                .FirstOrDefaultAsync(p => p.id_plataforma == oferta.plataforma_id);
            
            if (plataforma != null)
            {
                oferta.taxa_plataforma = plataforma.taxa_por_100k;
                oferta.preco_final = await CalcularPrecoFinalAsync(
                    oferta.quantidade_coins, 
                    oferta.preco_por_100k, 
                    oferta.taxa_plataforma
                );
            }

            _context.VendedorOfertas.Update(oferta);
            await _context.SaveChangesAsync();
            return oferta;
        }

        public async Task<bool> DeleteOfertaAsync(int idOferta)
        {
            var oferta = await _context.VendedorOfertas.FindAsync(idOferta);
            if (oferta == null)
                return false;

            _context.VendedorOfertas.Remove(oferta);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateStatusOfertaAsync(int idOferta, string status)
        {
            var oferta = await _context.VendedorOfertas.FindAsync(idOferta);
            if (oferta == null)
                return false;

            oferta.status = status;
            oferta.data_atualizacao = DateTime.UtcNow;
            
            if (status == "vendido")
            {
                oferta.data_venda = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public Task<decimal> CalcularPrecoFinalAsync(decimal quantidadeCoins, decimal precoPor100k, decimal taxaPlataforma)
        {
            // Calcular preço base (quantidade * preço por 100k / 100000)
            decimal precoBase = (quantidadeCoins * precoPor100k) / 100000;
            
            // Calcular taxa da plataforma
            decimal totalTaxa = (quantidadeCoins * taxaPlataforma) / 100000;
            
            // Preço final = preço base + taxa da plataforma
            return Task.FromResult(precoBase + totalTaxa);
        }

        public async Task<List<VendedorOferta>> GetOfertasDisponiveisAsync()
        {
            return await _context.VendedorOfertas
                .Include(o => o.Vendedor)
                .Include(o => o.Plataforma)
                .Where(o => o.status == "ativo")
                .Where(o => o.quantidade_coins > 0)
                .OrderBy(o => o.preco_por_100k)
                .ToListAsync();
        }
    }
}
