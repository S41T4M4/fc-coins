using Npgsql;

namespace DatabaseSeeder
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("🚀 EAFCCoinsManager - Database Seeder");
            Console.WriteLine("=====================================");

            var connectionString = "Host=localhost;Port=5432;Database=eacoins_db;Username=postgres;Password=Staff4912;";

            try
            {
                using var connection = new NpgsqlConnection(connectionString);
                await connection.OpenAsync();
                Console.WriteLine("✅ Conexão estabelecida!");

                // Verificar se já existem dados
                using var checkCmd = new NpgsqlCommand("SELECT COUNT(*) FROM plataforma", connection);
                var count = await checkCmd.ExecuteScalarAsync();
                
                if (Convert.ToInt32(count) > 0)
                {
                    Console.WriteLine("⚠️  Banco já possui dados. Pulando população.");
                    return;
                }

                Console.WriteLine("🌱 Iniciando população do banco...");

                // 1. Inserir Plataformas
                Console.WriteLine("📱 Inserindo plataformas...");
                var plataformasSql = @"
                    INSERT INTO plataforma (descricao_plataforma) VALUES 
                    ('PlayStation 5'),
                    ('PlayStation 4'),
                    ('Xbox Series X'),
                    ('Xbox One'),
                    ('PC (Origin)'),
                    ('PC (Steam)'),
                    ('Nintendo Switch');";

                using var plataformasCmd = new NpgsqlCommand(plataformasSql, connection);
                await plataformasCmd.ExecuteNonQueryAsync();

                // 2. Inserir Usuários
                Console.WriteLine("👥 Inserindo usuários...");
                var usuariosSql = @"
                    INSERT INTO usuarios (nome, email, senha, role, data_registro) VALUES 
                    ('Admin', 'admin@eafccoins.com', 'admin123', 'admin', NOW()),
                    ('João Silva', 'joao@email.com', 'senha123', 'comprador', NOW()),
                    ('Maria Santos', 'maria@email.com', 'senha456', 'comprador', NOW()),
                    ('Pedro Costa', 'pedro@email.com', 'senha789', 'comprador', NOW()),
                    ('Ana Lima', 'ana@email.com', 'senha101', 'comprador', NOW());";

                using var usuariosCmd = new NpgsqlCommand(usuariosSql, connection);
                await usuariosCmd.ExecuteNonQueryAsync();

                // 3. Inserir Moedas
                Console.WriteLine("💰 Inserindo moedas...");
                var moedasSql = @"
                    -- PlayStation 5
                    INSERT INTO moeda (plataforma_id, quantidade, valor) VALUES 
                    (1, 100000, 45.00),
                    (1, 250000, 100.00),
                    (1, 500000, 180.00),
                    (1, 1000000, 350.00);

                    -- PlayStation 4
                    INSERT INTO moeda (plataforma_id, quantidade, valor) VALUES 
                    (2, 100000, 40.00),
                    (2, 250000, 90.00),
                    (2, 500000, 160.00),
                    (2, 1000000, 300.00);

                    -- Xbox Series X
                    INSERT INTO moeda (plataforma_id, quantidade, valor) VALUES 
                    (3, 100000, 45.00),
                    (3, 250000, 100.00),
                    (3, 500000, 180.00),
                    (3, 1000000, 350.00);

                    -- Xbox One
                    INSERT INTO moeda (plataforma_id, quantidade, valor) VALUES 
                    (4, 100000, 40.00),
                    (4, 250000, 90.00),
                    (4, 500000, 160.00),
                    (4, 1000000, 300.00);

                    -- PC (Origin)
                    INSERT INTO moeda (plataforma_id, quantidade, valor) VALUES 
                    (5, 100000, 35.00),
                    (5, 250000, 80.00),
                    (5, 500000, 150.00),
                    (5, 1000000, 280.00);

                    -- PC (Steam)
                    INSERT INTO moeda (plataforma_id, quantidade, valor) VALUES 
                    (6, 100000, 35.00),
                    (6, 250000, 80.00),
                    (6, 500000, 150.00),
                    (6, 1000000, 280.00);

                    -- Nintendo Switch
                    INSERT INTO moeda (plataforma_id, quantidade, valor) VALUES 
                    (7, 100000, 50.00),
                    (7, 250000, 110.00),
                    (7, 500000, 200.00),
                    (7, 1000000, 380.00);";

                using var moedasCmd = new NpgsqlCommand(moedasSql, connection);
                await moedasCmd.ExecuteNonQueryAsync();

                // 4. Inserir Carrinhos
                Console.WriteLine("🛒 Inserindo carrinhos...");
                var carrinhosSql = @"
                    INSERT INTO carrinho (id_user, create_time) VALUES 
                    (2, NOW()),
                    (3, NOW()),
                    (4, NOW());";

                using var carrinhosCmd = new NpgsqlCommand(carrinhosSql, connection);
                await carrinhosCmd.ExecuteNonQueryAsync();

                // 5. Inserir Itens de Carrinho
                Console.WriteLine("🛍️ Inserindo itens de carrinho...");
                var itensCarrinhoSql = @"
                    INSERT INTO item_carrinho (id_carrinho, id_moeda, quantidade) VALUES 
                    (1, 1, 2),  -- João: 2x 100k PS5
                    (1, 2, 1),  -- João: 1x 250k PS5
                    (2, 5, 1),  -- Maria: 1x 100k PS4
                    (3, 9, 3);  -- Pedro: 3x 100k Xbox Series X";

                using var itensCarrinhoCmd = new NpgsqlCommand(itensCarrinhoSql, connection);
                await itensCarrinhoCmd.ExecuteNonQueryAsync();

                // 6. Inserir Pedidos
                Console.WriteLine("📦 Inserindo pedidos...");
                var pedidosSql = @"
                    INSERT INTO pedido (id_user, data_pedido, total, status) VALUES 
                    (2, NOW() - INTERVAL '2 days', 190.00, 'Concluído'),
                    (3, NOW() - INTERVAL '1 day', 40.00, 'Processando'),
                    (4, NOW(), 135.00, 'Pendente');";

                using var pedidosCmd = new NpgsqlCommand(pedidosSql, connection);
                await pedidosCmd.ExecuteNonQueryAsync();

                // 7. Inserir Itens de Pedido
                Console.WriteLine("📋 Inserindo itens de pedido...");
                var itensPedidoSql = @"
                    INSERT INTO item_pedido (id_pedido, id_moeda, quantidade, preco_unitario) VALUES 
                    (1, 1, 2, 45.00),  -- Pedido 1: 2x 100k PS5
                    (1, 2, 1, 100.00), -- Pedido 1: 1x 250k PS5
                    (2, 5, 1, 40.00),  -- Pedido 2: 1x 100k PS4
                    (3, 9, 3, 45.00);  -- Pedido 3: 3x 100k Xbox Series X";

                using var itensPedidoCmd = new NpgsqlCommand(itensPedidoSql, connection);
                await itensPedidoCmd.ExecuteNonQueryAsync();

                // 8. Inserir Pagamentos
                Console.WriteLine("💳 Inserindo pagamentos...");
                var pagamentosSql = @"
                    INSERT INTO pagamento (id_pedido, data_pag, valor_pago, metodo, status, transaction_id) VALUES 
                    (1, NOW() - INTERVAL '2 days', 190.00, 'PIX', 'Aprovado', 1001),
                    (2, NOW() - INTERVAL '1 day', 40.00, 'Cartão', 'Aprovado', 1002),
                    (3, NOW(), 135.00, 'PIX', 'Pendente', 1003);";

                using var pagamentosCmd = new NpgsqlCommand(pagamentosSql, connection);
                await pagamentosCmd.ExecuteNonQueryAsync();

                // Verificar dados inseridos
                Console.WriteLine("🔍 Verificando dados inseridos...");
                var verificacaoSql = @"
                    SELECT 'Plataformas:' as info, COUNT(*) as total FROM plataforma
                    UNION ALL
                    SELECT 'Usuários:' as info, COUNT(*) as total FROM usuarios
                    UNION ALL
                    SELECT 'Moedas:' as info, COUNT(*) as total FROM moeda
                    UNION ALL
                    SELECT 'Carrinhos:' as info, COUNT(*) as total FROM carrinho
                    UNION ALL
                    SELECT 'Pedidos:' as info, COUNT(*) as total FROM pedido
                    UNION ALL
                    SELECT 'Pagamentos:' as info, COUNT(*) as total FROM pagamento;";

                using var verificacaoCmd = new NpgsqlCommand(verificacaoSql, connection);
                using var reader = await verificacaoCmd.ExecuteReaderAsync();

                Console.WriteLine("\n📊 Resumo dos dados inseridos:");
                while (await reader.ReadAsync())
                {
                    Console.WriteLine($"   - {reader.GetString(0)} {reader.GetInt32(1)}");
                }

                Console.WriteLine("\n🎉 Banco populado com sucesso!");
                Console.WriteLine("🚀 Seu sistema EAFCCoinsManager está pronto para uso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Erro: {ex.Message}");
                Console.WriteLine("💡 Certifique-se de que:");
                Console.WriteLine("   - PostgreSQL está rodando");
                Console.WriteLine("   - Banco 'eacoins_db' existe");
                Console.WriteLine("   - Credenciais estão corretas");
            }

            Console.WriteLine("\nPressione qualquer tecla para sair...");
            Console.ReadKey();
        }
    }
}
