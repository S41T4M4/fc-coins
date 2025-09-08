using EAFCCoinsManager.Infraestrutura;
using EAFCCoinsManager.Models;
using Microsoft.EntityFrameworkCore;

namespace EAFCCoinsManager.Repositories
{
    public class PagamentoRepository
    {
        private readonly ConnectionContext _connectionContext;

        public PagamentoRepository(ConnectionContext connectionContext)
        {
            _connectionContext = connectionContext;
        }

        public async Task<Pagamento> AddPagamento(Pagamento pagamento)
        {
            _connectionContext.Pagamento.Add(pagamento);
            await _connectionContext.SaveChangesAsync();
            return pagamento;
        }

        public async Task<Pagamento> GetPagamentoByPedido(int idPedido)
        {
            return await _connectionContext.Pagamento
                .Include(p => p.Pedido)
                .FirstOrDefaultAsync(p => p.id_pedido == idPedido);
        }

        public async Task<Pagamento> UpdateStatusPagamento(int idPagamento, string status)
        {
            var pagamento = await _connectionContext.Pagamento.FindAsync(idPagamento);
            if (pagamento != null)
            {
                pagamento.status = status;
                await _connectionContext.SaveChangesAsync();
            }
            return pagamento;
        }
    }
}
