using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoedaController : ControllerBase
    {
        private readonly IMoedaRepository _moedaRepository;

        public MoedaController(IMoedaRepository moedaRepository)
        {
            _moedaRepository = moedaRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMoedas()
        {
            try
            {
                var moedas = await _moedaRepository.GetAllMoedas();
                var moedasResponse = moedas.Select(m => new MoedaResponse
                {
                    Success = true,
                    IdMoeda = m.id_moeda,
                    PlataformaId = m.plataforma_id,
                    Quantidade = m.quantidade,
                    Valor = m.valor,
                    PlataformaNome = m.Plataforma?.descricao_plataforma ?? "N/A",
                    Message = "Moeda encontrada"
                }).ToList();

                return Ok(new MoedaListResponse
                {
                    Success = true,
                    Moedas = moedasResponse,
                    Message = $"Encontradas {moedasResponse.Count} moedas"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMoedaById(int id)
        {
            try
            {
                var moeda = await _moedaRepository.GetMoedaById(id);
                if (moeda == null)
                    return NotFound("Moeda não encontrada");

                var moedaResponse = new MoedaResponse
                {
                    Success = true,
                    IdMoeda = moeda.id_moeda,
                    PlataformaId = moeda.plataforma_id,
                    Quantidade = moeda.quantidade,
                    Valor = moeda.valor,
                    PlataformaNome = moeda.Plataforma?.descricao_plataforma ?? "N/A",
                    Message = "Moeda encontrada"
                };

                return Ok(moedaResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("plataforma/{plataformaId}")]
        public async Task<IActionResult> GetMoedasByPlataforma(int plataformaId)
        {
            try
            {
                var moedas = await _moedaRepository.GetMoedasByPlataforma(plataformaId);
                var moedasResponse = moedas.Select(m => new MoedaResponse
                {
                    Success = true,
                    IdMoeda = m.id_moeda,
                    PlataformaId = m.plataforma_id,
                    Quantidade = m.quantidade,
                    Valor = m.valor,
                    PlataformaNome = m.Plataforma?.descricao_plataforma ?? "N/A",
                    Message = "Moeda encontrada"
                }).ToList();

                return Ok(new MoedaListResponse
                {
                    Success = true,
                    Moedas = moedasResponse,
                    Message = $"Encontradas {moedasResponse.Count} moedas para a plataforma {plataformaId}"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoeda([FromBody] MoedaRequest request)
        {
            try
            {
                if (request.PlataformaId <= 0)
                    return BadRequest("ID da plataforma é obrigatório");

                if (request.Quantidade <= 0)
                    return BadRequest("Quantidade deve ser maior que zero");

                if (request.Valor <= 0)
                    return BadRequest("Valor deve ser maior que zero");

                var moeda = new Moeda
                {
                    plataforma_id = request.PlataformaId,
                    quantidade = request.Quantidade,
                    valor = request.Valor
                };

                var novaMoeda = await _moedaRepository.AddNewValues(moeda);
                
                var moedaResponse = new MoedaResponse
                {
                    Success = true,
                    IdMoeda = novaMoeda.id_moeda,
                    PlataformaId = novaMoeda.plataforma_id,
                    Quantidade = novaMoeda.quantidade,
                    Valor = novaMoeda.valor,
                    PlataformaNome = novaMoeda.Plataforma?.descricao_plataforma ?? "N/A",
                    Message = "Moeda criada com sucesso!"
                };

                return Ok(moedaResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMoeda(int id, [FromBody] MoedaRequest request)
        {
            try
            {
                var moedaExistente = await _moedaRepository.GetMoedaById(id);
                if (moedaExistente == null)
                    return NotFound("Moeda não encontrada");

                if (request.PlataformaId <= 0)
                    return BadRequest("ID da plataforma é obrigatório");

                if (request.Quantidade <= 0)
                    return BadRequest("Quantidade deve ser maior que zero");

                if (request.Valor <= 0)
                    return BadRequest("Valor deve ser maior que zero");

                var moeda = new Moeda
                {
                    id_moeda = id,
                    plataforma_id = request.PlataformaId,
                    quantidade = request.Quantidade,
                    valor = request.Valor
                };

                var moedaAtualizada = await _moedaRepository.UpdateValueMoeda(moeda);
                
                var moedaResponse = new MoedaResponse
                {
                    Success = true,
                    IdMoeda = moedaAtualizada.id_moeda,
                    PlataformaId = moedaAtualizada.plataforma_id,
                    Quantidade = moedaAtualizada.quantidade,
                    Valor = moedaAtualizada.valor,
                    PlataformaNome = moedaAtualizada.Plataforma?.descricao_plataforma ?? "N/A",
                    Message = "Moeda atualizada com sucesso!"
                };

                return Ok(moedaResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
