using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using EAFCCoinsManager.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly PagamentoRepository _pagamentoRepository;

        public PedidoController(IPedidoRepository pedidoRepository, PagamentoRepository pagamentoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _pagamentoRepository = pagamentoRepository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPedidoById(int id)
        {
            try
            {
                var pedido = await _pedidoRepository.GetByIdAsync(id);
                if (pedido == null)
                    return NotFound("Pedido não encontrado");

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("usuario/{userId}")]
        public async Task<IActionResult> GetPedidosByUser(int userId)
        {
            try
            {
                var pedidos = await _pedidoRepository.GetPedidoByUser(userId);
                return Ok(pedidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePedido([FromBody] Pedido pedido)
        {
            try
            {
                if (pedido.id_user <= 0)
                    return BadRequest("ID do usuário é obrigatório");

                if (pedido.total <= 0)
                    return BadRequest("Total deve ser maior que zero");

                pedido.data_pedido = DateTime.UtcNow;
                pedido.status = "Pendente";

                var novoPedido = await _pedidoRepository.AddPedidoAsync(pedido);
                return CreatedAtAction(nameof(GetPedidoById), new { id = novoPedido.id_pedido }, novoPedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("{pedidoId}/itens")]
        public async Task<IActionResult> AddItemToPedido(int pedidoId, [FromBody] ItemPedido itemPedido)
        {
            try
            {
                itemPedido.id_pedido = pedidoId;

                if (itemPedido.id_moeda <= 0)
                    return BadRequest("ID da moeda é obrigatório");

                if (itemPedido.quantidade <= 0)
                    return BadRequest("Quantidade deve ser maior que zero");

                var pedidoAtualizado = await _pedidoRepository.AddNewItemPedido(itemPedido);
                return Ok(pedidoAtualizado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("{pedidoId}/checkout")]
        public async Task<IActionResult> CheckoutPedido(int pedidoId, [FromBody] CompraRequest compraRequest)
        {
            try
            {
                var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
                if (pedido == null)
                    return NotFound("Pedido não encontrado");

                if (pedido.status != "Pendente")
                    return BadRequest("Pedido já foi processado");

                // Criar pagamento
                var pagamento = new Pagamento
                {
                    id_pedido = pedidoId,
                    data_pag = DateTime.UtcNow,
                    valor_pago = pedido.total,
                    metodo = compraRequest.MetodoPagamento,
                    status = "Pendente",
                    transaction_id = compraRequest.TransactionId ?? 0
                };

                await _pagamentoRepository.AddPagamento(pagamento);

                // Atualizar status do pedido
                pedido.status = "Processando";
                await _pedidoRepository.AddPedidoAsync(pedido);

                return Ok(new { 
                    pedidoId = pedido.id_pedido, 
                    status = pedido.status,
                    pagamentoId = pagamento.id_pagamento 
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("{pedidoId}/status")]
        public async Task<IActionResult> UpdatePedidoStatus(int pedidoId, [FromBody] string status)
        {
            try
            {
                var pedido = await _pedidoRepository.GetByIdAsync(pedidoId);
                if (pedido == null)
                    return NotFound("Pedido não encontrado");

                pedido.status = status;
                await _pedidoRepository.AddPedidoAsync(pedido);

                return Ok(pedido);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
