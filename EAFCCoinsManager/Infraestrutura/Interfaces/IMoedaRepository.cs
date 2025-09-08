using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface IMoedaRepository
    {
        Task<List<Moeda>> GetAllMoedas();
        Task<Moeda> GetMoedaById(int id);
        Task<Moeda> AddNewValues(Moeda moeda);
        Task<Moeda> UpdateValueMoeda(Moeda moeda);
        Task<List<Moeda>> GetMoedasByPlataforma(int id_plataforma);

    }
}
