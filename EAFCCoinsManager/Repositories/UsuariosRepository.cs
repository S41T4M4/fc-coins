using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        private readonly ConnectionContext _connectionContext;

        public UsuariosRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public void AddNewUser(Usuarios user)
        {
            _connectionContext.Usuarios.Add(user);
            _connectionContext.SaveChanges();
        }

        public void DeleteUsuarioById(int id)
        {
            var existingUsuario = _connectionContext.Usuarios.Find(id);
            if (existingUsuario != null)
            {
                _connectionContext.Usuarios.Remove(existingUsuario);
                _connectionContext.SaveChanges();
            }
            
        }

        public List<Usuarios> GetAll()
        {
           return  _connectionContext.Usuarios.ToList();
        }

        public Usuarios GetUsuarioById(int id)
        {
            return _connectionContext.Usuarios.Find(id);
        }

        public async Task<Usuarios?> GetUserByIdAsync(int id)
        {
            return await _connectionContext.Usuarios.FindAsync(id);
        }

        public void UpdateUsuario(Usuarios usuarios)
        {
            var existingUsuario = _connectionContext.Usuarios.Find(usuarios.id);
            if(existingUsuario != null)
            {
                _connectionContext.Usuarios.Update(existingUsuario);
                _connectionContext.SaveChanges();
            }
        }
    }
}
