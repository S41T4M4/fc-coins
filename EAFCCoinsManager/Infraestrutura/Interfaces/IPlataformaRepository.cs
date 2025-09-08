using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface IPlataformaRepository
    {
        Task<Plataforma> AddNewPlataforma(Plataforma plataforma);
        Task<List<Plataforma>> GetAllPlataforma();
        Task<Plataforma> GetPlataformaById(int id_plataforma);

    }
}
