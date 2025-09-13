using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EAFCCoinsManager.Models;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using EAFCCoinsManager.ViewModel;
using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Infraestrutura.Interfaces;

namespace EAFCCoinsManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoRepository _carrinhoRepository;

        public CarrinhoController(ICarrinhoRepository carrinhoRepository)
        {
            _carrinhoRepository = carrinhoRepository;
        }

        [HttpPost("criar")]
        public async Task<IActionResult> Criar([FromBody] CarrinhoViewModel model)
        {
            var carrinho = new Carrinho
            {
                id_user = model.IdUser,
                create_time = DateTime.UtcNow
            };

            await _carrinhoRepository.CreateCarrinho(carrinho);
            return Ok(carrinho);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            try
            {
                var carrinho = await _carrinhoRepository.GetById(id);
                if (carrinho == null) 
                    return NotFound(new { success = false, message = "Carrinho não encontrado" });

                return Ok(new
                {
                    success = true,
                    idCarrinho = carrinho.id_carrinho,
                    idUser = carrinho.id_user,
                    createTime = carrinho.create_time,
                    usuario = carrinho.Usuario != null ? new
                    {
                        id = carrinho.Usuario.id,
                        nome = carrinho.Usuario.nome,
                        email = carrinho.Usuario.email,
                        role = carrinho.Usuario.role,
                        dataRegistro = carrinho.Usuario.data_registro
                    } : null,
                    itens = carrinho.Itens?.Select(i => new
                    {
                        idItem = i.id_item,
                        idCarrinho = i.id_carrinho,
                        idMoeda = i.id_moeda,
                        quantidade = i.quantidade,
                        moeda = i.Moeda != null ? new
                        {
                            idMoeda = i.Moeda.id_moeda,
                            quantidade = i.Moeda.quantidade,
                            valor = i.Moeda.valor,
                            plataforma = i.Moeda.Plataforma?.descricao_plataforma ?? "N/A"
                        } : null
                    }).ToList(),
                    message = "Carrinho encontrado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpGet("usuario/{idUser}")]
        public async Task<IActionResult> ObterPorUsuario(int idUser)
        {
            try
            {
                var carrinho = await _carrinhoRepository.GetByUserId(idUser);
                if (carrinho == null) 
                    return NotFound(new { success = false, message = "Carrinho não encontrado para este usuário" });

                return Ok(new
                {
                    success = true,
                    idCarrinho = carrinho.id_carrinho,
                    idUser = carrinho.id_user,
                    createTime = carrinho.create_time,
                    usuario = carrinho.Usuario != null ? new
                    {
                        id = carrinho.Usuario.id,
                        nome = carrinho.Usuario.nome,
                        email = carrinho.Usuario.email,
                        role = carrinho.Usuario.role,
                        dataRegistro = carrinho.Usuario.data_registro
                    } : null,
                    itens = carrinho.Itens?.Select(i => new
                    {
                        idItem = i.id_item,
                        idCarrinho = i.id_carrinho,
                        idMoeda = i.id_moeda,
                        quantidade = i.quantidade,
                        moeda = i.Moeda != null ? new
                        {
                            idMoeda = i.Moeda.id_moeda,
                            quantidade = i.Moeda.quantidade,
                            valor = i.Moeda.valor,
                            plataforma = i.Moeda.Plataforma?.descricao_plataforma ?? "N/A"
                        } : null
                    }).ToList(),
                    message = "Carrinho encontrado com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }


        [HttpPost("adicionar-item")]
        public async Task<IActionResult> AdicionarItem([FromBody] ItemCarrinhoViewModel model)
        {
            try
            {
                var item = new ItemCarrinho
                {
                    id_carrinho = model.IdCarrinho,
                    id_moeda = model.IdMoeda,
                    quantidade = model.Quantidade
                };

                await _carrinhoRepository.AddItemCarrinho(item);
                
                return Ok(new
                {
                    success = true,
                    idItem = item.id_item,
                    idCarrinho = item.id_carrinho,
                    idMoeda = item.id_moeda,
                    quantidade = item.quantidade,
                    message = "Item adicionado ao carrinho com sucesso!"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }



        [HttpDelete("remover-item/{idItem}")]
        public async Task<IActionResult> RemoverItem(int idItem)
        {
            try
            {
                // Primeiro, verificar se o item existe e obter informações do carrinho
                var item = await _carrinhoRepository.GetItemById(idItem);
                if (item == null)
                    return NotFound(new { success = false, message = "Item não encontrado" });

                // Remover o item
                await _carrinhoRepository.RemoveItemCarrinho(idItem);
                
                return Ok(new
                {
                    success = true,
                    message = $"Item {idItem} removido do carrinho {item.id_carrinho} com sucesso!",
                    itemRemovido = new
                    {
                        idItem = item.id_item,
                        idCarrinho = item.id_carrinho,
                        idMoeda = item.id_moeda,
                        quantidade = item.quantidade
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpPut("atualizar-quantidade/{idItem}")]
        public async Task<IActionResult> AtualizarQuantidade(int idItem, [FromBody] UpdateQuantityRequest request)
        {
            try
            {
                if (request.Quantidade <= 0)
                    return BadRequest(new { success = false, message = "Quantidade deve ser maior que zero" });

                // Verificar se o item existe
                var item = await _carrinhoRepository.GetItemById(idItem);
                if (item == null)
                    return NotFound(new { success = false, message = "Item não encontrado" });

                // Atualizar a quantidade
                await _carrinhoRepository.UpdateItemQuantity(idItem, request.Quantidade);
                
                return Ok(new
                {
                    success = true,
                    message = $"Quantidade do item {idItem} atualizada para {request.Quantidade} com sucesso!",
                    itemAtualizado = new
                    {
                        idItem = item.id_item,
                        idCarrinho = item.id_carrinho,
                        idMoeda = item.id_moeda,
                        quantidade = request.Quantidade
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

    }

}
