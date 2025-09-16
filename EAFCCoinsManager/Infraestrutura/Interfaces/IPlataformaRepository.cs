using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface IPlataformaRepository
    {
        Task<Plataforma> AddNewPlataforma(Plataforma plataforma);
        Task<List<Plataforma>> GetAllPlataforma();
        Task<List<Plataforma>> GetAllPlataformasAsync();
        Task<Plataforma> GetPlataformaById(int id_plataforma);
        Task<Plataforma?> GetPlataformaByIdAsync(int id_plataforma);
    }
}
