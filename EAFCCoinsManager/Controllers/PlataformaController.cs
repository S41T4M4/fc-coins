using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlataformaController : ControllerBase
    {
        private readonly IPlataformaRepository _plataformaRepository;

        public PlataformaController(IPlataformaRepository plataformaRepository)
        {
            _plataformaRepository = plataformaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlataformas()
        {
            try
            {
                var plataformas = await _plataformaRepository.GetAllPlataforma();
                var plataformasResponse = plataformas.Select(p => new PlataformaResponse
                {
                    Success = true,
                    IdPlataforma = p.id_plataforma,
                    DescricaoPlataforma = p.descricao_plataforma,
                    Message = "Plataforma encontrada"
                }).ToList();

                return Ok(new
                {
                    success = true,
                    plataformas = plataformasResponse,
                    message = $"Encontradas {plataformasResponse.Count()} plataformas"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlataformaById(int id)
        {
            try
            {
                var plataforma = await _plataformaRepository.GetPlataformaById(id);
                if (plataforma == null)
                    return NotFound("Plataforma não encontrada");

                var plataformaResponse = new PlataformaResponse
                {
                    Success = true,
                    IdPlataforma = plataforma.id_plataforma,
                    DescricaoPlataforma = plataforma.descricao_plataforma,
                    Message = "Plataforma encontrada"
                };

                return Ok(plataformaResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlataforma([FromBody] PlataformaRequest request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.DescricaoPlataforma))
                    return BadRequest("Descrição da plataforma é obrigatória");

                var plataforma = new Plataforma
                {
                    descricao_plataforma = request.DescricaoPlataforma
                };

                var novaPlataforma = await _plataformaRepository.AddNewPlataforma(plataforma);
                
                return Ok(new PlataformaResponse
                {
                    Success = true,
                    IdPlataforma = novaPlataforma.id_plataforma,
                    DescricaoPlataforma = novaPlataforma.descricao_plataforma,
                    Message = "Plataforma criada com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

    }
}
