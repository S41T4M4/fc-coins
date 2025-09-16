using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using EAFCCoinsManager.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/v1/usuarios/")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepository _usuariosRepository;

        public UsuariosController(IUsuariosRepository usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }
        [HttpPost("novo_usuario")]

        public IActionResult AddNewUsuario(UsuariosViewModel usuariosView)
        {
            var newUser = new Usuarios()
            {           
                nome = usuariosView.Nome,
                email = usuariosView.Email,
                senha = usuariosView.Senha,
                role = usuariosView.Role,                
            };
            if(newUser.role != "admin" && newUser.role != "comprador")
            {
                throw new Exception("O usuario tem que ser ou admin ou comprador");
            }
            _usuariosRepository.AddNewUser(newUser);
            return Ok();
        }
        [HttpGet("get_all_usuarios")]
        public IActionResult GetAllUsers()
        {
            var allUsers = _usuariosRepository.GetAll();
            return Ok(allUsers);
        }
        [HttpGet("get_user_by_id")]
        public IActionResult GetUserById(int id)
        {
            var userExisting = _usuariosRepository.GetUsuarioById(id);
            return Ok(userExisting);
        }
        [HttpPut("update_user")]
        public IActionResult UpdateUser (UsuariosViewModel usuariosViewModel, int id )
        {
            var userExisting = _usuariosRepository.GetUsuarioById(id);
            if (userExisting != null)
            {
                userExisting.nome = usuariosViewModel.Nome;
                userExisting.email = usuariosViewModel.Email;
                userExisting.senha = usuariosViewModel.Senha;
                userExisting.role = usuariosViewModel.Role;

                _usuariosRepository.UpdateUsuario(userExisting);
                return Ok();
            }
            else
            {
                throw new Exception("O usuario não foi encontrado");
            }
            
        }
        [HttpDelete("remove_user")]
        public IActionResult RemoveUser(int id)
        {
            _usuariosRepository.DeleteUsuarioById(id);
            return Ok();
        }

    }
}
