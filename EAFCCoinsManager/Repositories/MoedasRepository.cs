using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class MoedasRepository : IMoedaRepository
    {
        private readonly ConnectionContext _connectionContext;

        public MoedasRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public async Task<Moeda> AddNewValues(Moeda moeda)
        {
            _connectionContext.Moeda.Add(moeda);
            await _connectionContext.SaveChangesAsync();
            return moeda;
        }

        public async Task<List<Moeda>> GetAllMoedas()
        {
            return await _connectionContext.Moeda
                .Include(m => m.Plataforma)
                .ToListAsync();
        }

        public async Task<Moeda> GetMoedaById(int id)
        {
            return await _connectionContext.Moeda
                .Include(m => m.Plataforma)
                .FirstOrDefaultAsync(m => m.id_moeda == id);
        }

        public async Task<List<Moeda>> GetMoedasByPlataforma(int id_plataforma)
        {
            return await _connectionContext.Moeda
                .Include(m => m.Plataforma)
                .Where(m => m.plataforma_id == id_plataforma)
                .ToListAsync();
        }

        public async Task<Moeda> UpdateValueMoeda(Moeda moeda)
        {
            _connectionContext.Moeda.Update(moeda);
            await _connectionContext.SaveChangesAsync();
            return moeda;
        }
    }
}
