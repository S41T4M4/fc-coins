using EAFCCoinsManager.Models;

namespace EAFCCoinsManager.Infraestrutura.Interfaces
{
    public interface IUsuariosRepository
    {
        void AddNewUser(Usuarios user);
        List<Usuarios> GetAll();
        Usuarios GetUsuarioById(int id);
        Task<Usuarios?> GetUserByIdAsync(int id);
        void UpdateUsuario(Usuarios usuarios);
        void DeleteUsuarioById(int id); 
    }
}
