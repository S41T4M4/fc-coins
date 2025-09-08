using EAFCCoinsManager.Infraestrutura.Interfaces;
using EAFCCoinsManager.Models;
using EAFCCoinsManager.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckoutController : ControllerBase
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly PagamentoRepository _pagamentoRepository;

        public CheckoutController(
            ICarrinhoRepository carrinhoRepository,
            IPedidoRepository pedidoRepository,
            PagamentoRepository pagamentoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _pedidoRepository = pedidoRepository;
            _pagamentoRepository = pagamentoRepository;
        }

        [HttpPost("finalizar-compra")]
        public async Task<IActionResult> FinalizarCompra([FromBody] CompraRequest compraRequest)
        {
            try
            {
                if (compraRequest.IdCarrinho <= 0)
                    return BadRequest("ID do carrinho é obrigatório");

                if (string.IsNullOrEmpty(compraRequest.Email))
                    return BadRequest("Email é obrigatório");

                if (string.IsNullOrEmpty(compraRequest.MetodoPagamento))
                    return BadRequest("Método de pagamento é obrigatório");

                // Buscar carrinho
                var carrinho = await _carrinhoRepository.GetById(compraRequest.IdCarrinho);
                if (carrinho == null)
                    return NotFound("Carrinho não encontrado");

                if (carrinho.Itens == null || !carrinho.Itens.Any())
                    return BadRequest("Carrinho está vazio");

                // Calcular total
                decimal total = 0;
                foreach (var item in carrinho.Itens)
                {
                    total += item.quantidade * item.Moeda.valor;
                }

                // Criar pedido
                var pedido = new Pedido
                {
                    id_user = carrinho.id_user,
                    data_pedido = DateTime.UtcNow,
                    total = total,
                    status = "Pendente"
                };

                var novoPedido = await _pedidoRepository.AddPedidoAsync(pedido);

                // Adicionar itens do carrinho ao pedido
                foreach (var itemCarrinho in carrinho.Itens)
                {
                    var itemPedido = new ItemPedido
                    {
                        id_pedido = novoPedido.id_pedido,
                        id_moeda = itemCarrinho.id_moeda,
                        quantidade = itemCarrinho.quantidade,
                        Preco_unitario = itemCarrinho.Moeda.valor
                    };

                    await _pedidoRepository.AddNewItemPedido(itemPedido);
                }

                // Criar pagamento
                var pagamento = new Pagamento
                {
                    id_pedido = novoPedido.id_pedido,
                    data_pag = DateTime.UtcNow,
                    valor_pago = total,
                    metodo = compraRequest.MetodoPagamento,
                    status = "Pendente",
                    transaction_id = compraRequest.TransactionId ?? 0
                };

                await _pagamentoRepository.AddPagamento(pagamento);

                // Limpar carrinho
                await _carrinhoRepository.RemoveItemCarrinho(carrinho.id_carrinho);

                return Ok(new
                {
                    pedidoId = novoPedido.id_pedido,
                    total = total,
                    status = "Processando",
                    pagamentoId = pagamento.id_pagamento,
                    email = compraRequest.Email
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPost("criar-usuario-temporario")]
        public IActionResult CriarUsuarioTemporario([FromBody] string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return BadRequest("Email é obrigatório");

                // Aqui você pode implementar a lógica para criar um usuário temporário
                // ou apenas retornar um ID temporário para o checkout
                
                return Ok(new
                {
                    userId = 0, // ID temporário
                    email = email,
                    isTemporary = true
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
