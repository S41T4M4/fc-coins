namespace EAFCCoinsManager.Models
{
    public interface IUsuariosRepository
    {
        void AddNewUser(Usuarios user);
        List<Usuarios> GetAll();
        Usuarios GetUsuarioById(int id);    
        void UpdateUsuario(Usuarios usuarios);
        void DeleteUsuarioById(int id); 
    }
}
