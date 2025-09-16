using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface IVendedorOfertaRepository
    {
        Task<List<VendedorOferta>> GetAllOfertasAsync();
        Task<List<VendedorOferta>> GetOfertasByVendedorAsync(int idVendedor);
        Task<List<VendedorOferta>> GetOfertasByPlataformaAsync(int idPlataforma);
        Task<List<VendedorOferta>> GetOfertasAtivasAsync();
        Task<VendedorOferta?> GetOfertaByIdAsync(int idOferta);
        Task<VendedorOferta> CreateOfertaAsync(VendedorOferta oferta);
        Task<VendedorOferta> UpdateOfertaAsync(VendedorOferta oferta);
        Task<bool> DeleteOfertaAsync(int idOferta);
        Task<bool> UpdateStatusOfertaAsync(int idOferta, string status);
        Task<decimal> CalcularPrecoFinalAsync(decimal quantidadeCoins, decimal precoPor100k, decimal taxaPlataforma);
        Task<List<VendedorOferta>> GetOfertasDisponiveisAsync(); // Ofertas ativas disponíveis para compra
    }
}
