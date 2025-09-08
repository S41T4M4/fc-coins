using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Models;
using EAFCCoinsManager.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ConnectionContext _context;

        public AuthController(ConnectionContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Auth([FromBody] LoginRequest request)
        {
            // Verifica se email e senha foram preenchidos
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest("Email e senha são obrigatórios.");
            }

            // Busca o usuário pelo email
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.email == request.Email);

            // Verifica se o usuário existe
            if (usuario == null)
            {
                return Unauthorized("Usuário não encontrado.");
            }

            // Verifica se a senha está correta
            if (usuario.senha != request.Password)
            {
                return Unauthorized("Senha incorreta.");
            }

            // Debug: Log dos dados recebidos
            Console.WriteLine($"Login attempt - Email: {request.Email}, Password: {request.Password}");
            Console.WriteLine($"User found - Email: {usuario.email}, Password: {usuario.senha}");

            // Gera o token
            var token = TokenService.GenerateToken(usuario);

           
            return Ok(new
            {
                success = true,
                token,
                role = usuario.role,
                userId = usuario.id,
                email = usuario.email,
                nome = usuario.nome
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
                {
                    return BadRequest("Email e senha são obrigatórios.");
                }

                // Verificar se o usuário já existe
                var usuarioExistente = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.email == request.Email);

                if (usuarioExistente != null)
                {
                    return BadRequest("Usuário já existe com este email.");
                }

                // Criar novo usuário
                var novoUsuario = new Usuarios
                {
                    email = request.Email,
                    senha = request.Password, // Em produção, hash a senha
                    nome = request.Email.Split('@')[0], // Nome padrão baseado no email
                    role = "comprador" // Role padrão
                };

                _context.Usuarios.Add(novoUsuario);
                await _context.SaveChangesAsync();

                // Gerar token para o novo usuário
                var token = TokenService.GenerateToken(novoUsuario);

                return Ok(new
                {
                    token,
                    role = novoUsuario.role,
                    userId = novoUsuario.id,
                    email = novoUsuario.email,
                    nome = novoUsuario.nome
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpGet("validate")]
        public IActionResult ValidateToken()
        {
            // Se chegou até aqui, o token é válido
            return Ok(new { message = "Token válido" });
        }

        [HttpGet("debug/users")]
        public async Task<IActionResult> DebugUsers()
        {
            try
            {
                var usuarios = await _context.Usuarios.ToListAsync();
                return Ok(new
                {
                    success = true,
                    total = usuarios.Count,
                    usuarios = usuarios.Select(u => new
                    {
                        id = u.id,
                        nome = u.nome,
                        email = u.email,
                        role = u.role,
                        dataRegistro = u.data_registro
                    })
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}