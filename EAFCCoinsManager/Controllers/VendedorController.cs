using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using EAFCCoinsManager.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requer autenticação
    public class VendedorController : ControllerBase
    {
        private readonly IVendedorOfertaRepository _vendedorOfertaRepository;
        private readonly IPlataformaRepository _plataformaRepository;
        private readonly IUsuariosRepository _usuariosRepository;

        public VendedorController(
            IVendedorOfertaRepository vendedorOfertaRepository,
            IPlataformaRepository plataformaRepository,
            IUsuariosRepository usuariosRepository)
        {
            _vendedorOfertaRepository = vendedorOfertaRepository;
            _plataformaRepository = plataformaRepository;
            _usuariosRepository = usuariosRepository;
        }

        // GET: api/vendedor/ofertas
        [HttpGet("ofertas")]
        public async Task<IActionResult> GetOfertasByVendedor()
        {
            try
            {
                // Obter ID do usuário autenticado (implementar conforme seu sistema de auth)
                var userId = GetCurrentUserId();
                if (userId == null)
                    return Unauthorized("Usuário não autenticado");

                var ofertas = await _vendedorOfertaRepository.GetOfertasByVendedorAsync(userId.Value);
                
                var ofertasResponse = ofertas.Select(o => new VendedorOfertaResponse
                {
                    Success = true,
                    IdOferta = o.id_oferta,
                    IdVendedor = o.id_vendedor,
                    NomeVendedor = o.Vendedor?.nome ?? "N/A",
                    PlataformaId = o.plataforma_id,
                    PlataformaNome = o.Plataforma?.descricao_plataforma ?? "N/A",
                    QuantidadeCoins = o.quantidade_coins,
                    PrecoPor100k = o.preco_por_100k,
                    TaxaPlataforma = o.taxa_plataforma,
                    PrecoFinal = o.preco_final,
                    Status = o.status,
                    DataCriacao = o.data_criacao,
                    DataAtualizacao = o.data_atualizacao,
                    DataVenda = o.data_venda,
                    Message = "Oferta encontrada"
                }).ToList();

                return Ok(new VendedorOfertaListResponse
                {
                    Success = true,
                    Ofertas = ofertasResponse,
                    Message = $"Encontradas {ofertasResponse.Count} ofertas"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/vendedor/ofertas
        [HttpPost("ofertas")]
        public async Task<IActionResult> CreateOferta([FromBody] VendedorOfertaRequest request)
        {
            try
            {
                var userId = GetCurrentUserId();
                if (userId == null)
                    return Unauthorized("Usuário não autenticado");

                // Verificar se o usuário é vendedor
                var usuario = await _usuariosRepository.GetUserByIdAsync(userId.Value);
                if (usuario == null || (usuario.role != "seller" && usuario.role != "admin"))
                    return Forbid("Apenas vendedores podem criar ofertas");

                // Verificar se a plataforma existe e está ativa
                var plataforma = await _plataformaRepository.GetPlataformaByIdAsync(request.plataforma_id);
                if (plataforma == null || !plataforma.ativa)
                    return BadRequest("Plataforma não encontrada ou inativa");

                var oferta = new VendedorOferta
                {
                    id_vendedor = userId.Value,
                    plataforma_id = request.plataforma_id,
                    quantidade_coins = request.quantidade_coins,
                    preco_por_100k = request.preco_por_100k
                };

                var ofertaCriada = await _vendedorOfertaRepository.CreateOfertaAsync(oferta);

                var response = new VendedorOfertaResponse
                {
                    Success = true,
                    IdOferta = ofertaCriada.id_oferta,
                    IdVendedor = ofertaCriada.id_vendedor,
                    NomeVendedor = usuario.nome,
                    PlataformaId = ofertaCriada.plataforma_id,
                    PlataformaNome = plataforma.descricao_plataforma,
                    QuantidadeCoins = ofertaCriada.quantidade_coins,
                    PrecoPor100k = ofertaCriada.preco_por_100k,
                    TaxaPlataforma = ofertaCriada.taxa_plataforma,
                    PrecoFinal = ofertaCriada.preco_final,
                    Status = ofertaCriada.status,
                    DataCriacao = ofertaCriada.data_criacao,
                    Message = "Oferta criada com sucesso"
                };

                return CreatedAtAction(nameof(GetOfertaById), new { id = ofertaCriada.id_oferta }, response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // GET: api/vendedor/ofertas/{id}
        [HttpGet("ofertas/{id}")]
        public async Task<IActionResult> GetOfertaById(int id)
        {
            try
            {
                var oferta = await _vendedorOfertaRepository.GetOfertaByIdAsync(id);
                if (oferta == null)
                    return NotFound("Oferta não encontrada");

                var userId = GetCurrentUserId();
                if (userId == null || oferta.id_vendedor != userId.Value)
                    return Forbid("Acesso negado a esta oferta");

                var response = new VendedorOfertaResponse
                {
                    Success = true,
                    IdOferta = oferta.id_oferta,
                    IdVendedor = oferta.id_vendedor,
                    NomeVendedor = oferta.Vendedor?.nome ?? "N/A",
                    PlataformaId = oferta.plataforma_id,
                    PlataformaNome = oferta.Plataforma?.descricao_plataforma ?? "N/A",
                    QuantidadeCoins = oferta.quantidade_coins,
                    PrecoPor100k = oferta.preco_por_100k,
                    TaxaPlataforma = oferta.taxa_plataforma,
                    PrecoFinal = oferta.preco_final,
                    Status = oferta.status,
                    DataCriacao = oferta.data_criacao,
                    DataAtualizacao = oferta.data_atualizacao,
                    DataVenda = oferta.data_venda,
                    Message = "Oferta encontrada"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // PUT: api/vendedor/ofertas/{id}
        [HttpPut("ofertas/{id}")]
        public async Task<IActionResult> UpdateOferta(int id, [FromBody] VendedorOfertaRequest request)
        {
            try
            {
                var oferta = await _vendedorOfertaRepository.GetOfertaByIdAsync(id);
                if (oferta == null)
                    return NotFound("Oferta não encontrada");

                var userId = GetCurrentUserId();
                if (userId == null || oferta.id_vendedor != userId.Value)
                    return Forbid("Acesso negado a esta oferta");

                oferta.plataforma_id = request.plataforma_id;
                oferta.quantidade_coins = request.quantidade_coins;
                oferta.preco_por_100k = request.preco_por_100k;

                var ofertaAtualizada = await _vendedorOfertaRepository.UpdateOfertaAsync(oferta);

                var response = new VendedorOfertaResponse
                {
                    Success = true,
                    IdOferta = ofertaAtualizada.id_oferta,
                    IdVendedor = ofertaAtualizada.id_vendedor,
                    NomeVendedor = ofertaAtualizada.Vendedor?.nome ?? "N/A",
                    PlataformaId = ofertaAtualizada.plataforma_id,
                    PlataformaNome = ofertaAtualizada.Plataforma?.descricao_plataforma ?? "N/A",
                    QuantidadeCoins = ofertaAtualizada.quantidade_coins,
                    PrecoPor100k = ofertaAtualizada.preco_por_100k,
                    TaxaPlataforma = ofertaAtualizada.taxa_plataforma,
                    PrecoFinal = ofertaAtualizada.preco_final,
                    Status = ofertaAtualizada.status,
                    DataCriacao = ofertaAtualizada.data_criacao,
                    DataAtualizacao = ofertaAtualizada.data_atualizacao,
                    DataVenda = ofertaAtualizada.data_venda,
                    Message = "Oferta atualizada com sucesso"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // DELETE: api/vendedor/ofertas/{id}
        [HttpDelete("ofertas/{id}")]
        public async Task<IActionResult> DeleteOferta(int id)
        {
            try
            {
                var oferta = await _vendedorOfertaRepository.GetOfertaByIdAsync(id);
                if (oferta == null)
                    return NotFound("Oferta não encontrada");

                var userId = GetCurrentUserId();
                if (userId == null || oferta.id_vendedor != userId.Value)
                    return Forbid("Acesso negado a esta oferta");

                var sucesso = await _vendedorOfertaRepository.DeleteOfertaAsync(id);
                if (!sucesso)
                    return StatusCode(500, "Erro ao deletar oferta");

                return Ok(new { Success = true, Message = "Oferta deletada com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // PUT: api/vendedor/ofertas/{id}/status
        [HttpPut("ofertas/{id}/status")]
        public async Task<IActionResult> UpdateStatusOferta(int id, [FromBody] UpdateStatusRequest request)
        {
            try
            {
                var oferta = await _vendedorOfertaRepository.GetOfertaByIdAsync(id);
                if (oferta == null)
                    return NotFound("Oferta não encontrada");

                var userId = GetCurrentUserId();
                if (userId == null || oferta.id_vendedor != userId.Value)
                    return Forbid("Acesso negado a esta oferta");

                var sucesso = await _vendedorOfertaRepository.UpdateStatusOfertaAsync(id, request.Status);
                if (!sucesso)
                    return StatusCode(500, "Erro ao atualizar status da oferta");

                return Ok(new { Success = true, Message = "Status da oferta atualizado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // POST: api/vendedor/calcular-preco
        [HttpPost("calcular-preco")]
        public async Task<IActionResult> CalcularPreco([FromBody] CalculoPrecoRequest request)
        {
            try
            {
                var plataforma = await _plataformaRepository.GetPlataformaByIdAsync(request.PlataformaId);
                if (plataforma == null)
                    return NotFound("Plataforma não encontrada");

                var taxaPlataforma = plataforma.taxa_por_100k;
                var precoFinal = await _vendedorOfertaRepository.CalcularPrecoFinalAsync(
                    request.QuantidadeCoins, 
                    request.PrecoPor100k, 
                    taxaPlataforma
                );

                var totalTaxa = (request.QuantidadeCoins * taxaPlataforma) / 100000;
                var lucroVendedor = precoFinal - totalTaxa;

                var response = new CalculoPrecoResponse
                {
                    Success = true,
                    QuantidadeCoins = request.QuantidadeCoins,
                    PrecoPor100k = request.PrecoPor100k,
                    TaxaPlataforma = taxaPlataforma,
                    TotalTaxa = totalTaxa,
                    PrecoFinal = precoFinal,
                    LucroVendedor = lucroVendedor,
                    Message = "Preço calculado com sucesso"
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // GET: api/vendedor/plataformas
        [HttpGet("plataformas")]
        public async Task<IActionResult> GetPlataformasAtivas()
        {
            try
            {
                var plataformas = await _plataformaRepository.GetAllPlataformasAsync();
                var plataformasAtivas = plataformas.Where(p => p.ativa).ToList();

                var response = plataformasAtivas.Select(p => new
                {
                    Id = p.id_plataforma,
                    Nome = p.descricao_plataforma,
                    TaxaPor100k = p.taxa_por_100k,
                    Ativa = p.ativa
                }).ToList();

                return Ok(new { Success = true, Plataformas = response, Message = "Plataformas encontradas" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        // Método auxiliar para obter ID do usuário autenticado
        private int? GetCurrentUserId()
        {
            // Implementar conforme seu sistema de autenticação
            // Por exemplo, extrair do JWT token ou claims
            var userIdClaim = User.FindFirst("usuariosId")?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            return null;
        }
    }

    // Classes auxiliares para requests
    public class UpdateStatusRequest
    {
        public string Status { get; set; } = string.Empty;
    }

    public class CalculoPrecoRequest
    {
        public int PlataformaId { get; set; }
        public decimal QuantidadeCoins { get; set; }
        public decimal PrecoPor100k { get; set; }
    }
}
