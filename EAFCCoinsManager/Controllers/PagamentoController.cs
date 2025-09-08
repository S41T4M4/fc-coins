using EAFCCoinsManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagamentoController : ControllerBase
    {
        private readonly PagamentoRepository _pagamentoRepository;

        public PagamentoController(PagamentoRepository pagamentoRepository)
        {
            _pagamentoRepository = pagamentoRepository;
        }

        [HttpGet("pedido/{pedidoId}")]
        public async Task<IActionResult> GetPagamentoByPedido(int pedidoId)
        {
            try
            {
                var pagamento = await _pagamentoRepository.GetPagamentoByPedido(pedidoId);
                if (pagamento == null)
                    return NotFound("Pagamento não encontrado para este pedido");

                return Ok(pagamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdatePagamentoStatus(int id, [FromBody] string status)
        {
            try
            {
                if (string.IsNullOrEmpty(status))
                    return BadRequest("Status é obrigatório");

                var pagamento = await _pagamentoRepository.UpdateStatusPagamento(id, status);
                if (pagamento == null)
                    return NotFound("Pagamento não encontrado");

                return Ok(pagamento);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("webhook")]
        public IActionResult ProcessarWebhookPagamento([FromBody] dynamic webhookData)
        {
            try
            {
                // Aqui você implementaria a lógica para processar webhooks de pagamento
                // Por exemplo, PayPal, Stripe, PagSeguro, etc.
                
                // Exemplo básico de estrutura:
                // var transactionId = webhookData.transaction_id;
                // var status = webhookData.status;
                // var valor = webhookData.amount;
                
                // Buscar pagamento pelo transaction_id e atualizar status
                
                return Ok(new { message = "Webhook processado com sucesso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
