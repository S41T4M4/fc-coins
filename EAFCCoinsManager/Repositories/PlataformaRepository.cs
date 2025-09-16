using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class PlataformaRepository : IPlataformaRepository
    {
        private readonly ConnectionContext _connectionContext;

        public PlataformaRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public async Task<Plataforma> AddNewPlataforma(Plataforma plataforma)
        {
            _connectionContext.Plataforma.Add(plataforma);
            await _connectionContext.SaveChangesAsync();
            return plataforma;
        }

        public async Task<List<Plataforma>> GetAllPlataforma()
        {
            return await _connectionContext.Plataforma
                .Include(p => p.Moedas)
                .ToListAsync();
        }

        public async Task<Plataforma> GetPlataformaById(int id_plataforma)
        {
            return await _connectionContext.Plataforma
                .Include(p => p.Moedas)
                .FirstOrDefaultAsync(p => p.id_plataforma == id_plataforma);
        }

        public async Task<List<Plataforma>> GetAllPlataformasAsync()
        {
            return await _connectionContext.Plataforma
                .Include(p => p.Moedas)
                .Include(p => p.OfertasVendedores)
                .ToListAsync();
        }

        public async Task<Plataforma?> GetPlataformaByIdAsync(int id_plataforma)
        {
            return await _connectionContext.Plataforma
                .Include(p => p.Moedas)
                .Include(p => p.OfertasVendedores)
                .FirstOrDefaultAsync(p => p.id_plataforma == id_plataforma);
        }
    }
}
