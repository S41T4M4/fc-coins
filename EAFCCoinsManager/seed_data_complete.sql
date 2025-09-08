-- Script SQL melhorado para popular o banco EAFCCoinsManager
-- Execute este script após criar as tabelas

-- Limpar dados existentes (opcional)
-- DELETE FROM moeda;
-- DELETE FROM plataforma;
-- DELETE FROM usuarios;

-- Inserir plataformas
INSERT INTO plataforma (descricao_plataforma) VALUES 
('PlayStation 5'),
('PlayStation 4'),
('Xbox Series X'),
('Xbox One'),
('PC (Origin)'),
('PC (Steam)'),
('Nintendo Switch')
ON CONFLICT DO NOTHING;

-- Inserir usuários de exemplo
INSERT INTO usuarios (nome, email, senha, role, data_registro) VALUES 
('Admin', 'admin@eafccoins.com', 'admin123', 'admin', NOW()),
('João Silva', 'joao@email.com', 'senha123', 'comprador', NOW()),
('Maria Santos', 'maria@email.com', 'senha456', 'comprador', NOW()),
('Pedro Costa', 'pedro@email.com', 'senha789', 'comprador', NOW()),
('Ana Lima', 'ana@email.com', 'senha101', 'comprador', NOW())
ON CONFLICT (email) DO NOTHING;

-- Inserir moedas para cada plataforma
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
(7, 1000000, 380.00);

-- Inserir alguns carrinhos de exemplo
INSERT INTO carrinho (id_user, create_time) VALUES 
(2, NOW()),
(3, NOW()),
(4, NOW());

-- Inserir alguns itens de carrinho de exemplo
INSERT INTO item_carrinho (id_carrinho, id_moeda, quantidade) VALUES 
(1, 1, 2),  -- João: 2x 100k PS5
(1, 2, 1),  -- João: 1x 250k PS5
(2, 5, 1),  -- Maria: 1x 100k PS4
(3, 9, 3);  -- Pedro: 3x 100k Xbox Series X

-- Inserir alguns pedidos de exemplo
INSERT INTO pedido (id_user, data_pedido, total, status) VALUES 
(2, NOW() - INTERVAL '2 days', 190.00, 'Concluído'),
(3, NOW() - INTERVAL '1 day', 40.00, 'Processando'),
(4, NOW(), 135.00, 'Pendente');

-- Inserir itens dos pedidos
INSERT INTO item_pedido (id_pedido, id_moeda, quantidade, preco_unitario) VALUES 
(1, 1, 2, 45.00),  -- Pedido 1: 2x 100k PS5
(1, 2, 1, 100.00), -- Pedido 1: 1x 250k PS5
(2, 5, 1, 40.00),  -- Pedido 2: 1x 100k PS4
(3, 9, 3, 45.00);  -- Pedido 3: 3x 100k Xbox Series X

-- Inserir pagamentos de exemplo
INSERT INTO pagamento (id_pedido, data_pag, valor_pago, metodo, status, transaction_id) VALUES 
(1, NOW() - INTERVAL '2 days', 190.00, 'PIX', 'Aprovado', 1001),
(2, NOW() - INTERVAL '1 day', 40.00, 'Cartão', 'Aprovado', 1002),
(3, NOW(), 135.00, 'PIX', 'Pendente', 1003);

-- Verificar dados inseridos
SELECT '=== RESUMO DOS DADOS INSERIDOS ===' as info;

SELECT 'Plataformas:' as info;
SELECT id_plataforma, descricao_plataforma FROM plataforma ORDER BY id_plataforma;

SELECT 'Usuários:' as info;
SELECT id, nome, email, role FROM usuarios ORDER BY id;

SELECT 'Moedas por Plataforma:' as info;
SELECT 
    p.descricao_plataforma,
    COUNT(m.id_moeda) as total_moedas,
    MIN(m.valor) as menor_preco,
    MAX(m.valor) as maior_preco
FROM plataforma p 
LEFT JOIN moeda m ON p.id_plataforma = m.plataforma_id 
GROUP BY p.id_plataforma, p.descricao_plataforma
ORDER BY p.id_plataforma;

SELECT 'Carrinhos Ativos:' as info;
SELECT 
    c.id_carrinho,
    u.nome as usuario,
    COUNT(ic.id_item) as total_itens,
    SUM(ic.quantidade * m.valor) as valor_total
FROM carrinho c
JOIN usuarios u ON c.id_user = u.id
LEFT JOIN item_carrinho ic ON c.id_carrinho = ic.id_carrinho
LEFT JOIN moeda m ON ic.id_moeda = m.id_moeda
GROUP BY c.id_carrinho, u.nome
ORDER BY c.id_carrinho;

SELECT 'Pedidos:' as info;
SELECT 
    p.id_pedido,
    u.nome as usuario,
    p.total,
    p.status,
    pg.metodo as metodo_pagamento,
    pg.status as status_pagamento
FROM pedido p
JOIN usuarios u ON p.id_user = u.id
LEFT JOIN pagamento pg ON p.id_pedido = pg.id_pedido
ORDER BY p.id_pedido;

SELECT '=== BANCO POPULADO COM SUCESSO! ===' as info;
