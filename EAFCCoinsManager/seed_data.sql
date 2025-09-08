-- Script SQL para popular o banco com dados de exemplo
-- Execute este script após criar as tabelas

-- Inserir plataformas
INSERT INTO plataforma (descricao_plataforma) VALUES 
('PlayStation 5'),
('PlayStation 4'),
('Xbox Series X'),
('Xbox One'),
('PC (Origin)'),
('PC (Steam)'),
('Nintendo Switch');

-- Inserir usuários de exemplo
INSERT INTO usuarios (nome, email, senha, role) VALUES 
('Admin', 'admin@eafccoins.com', 'admin123', 'admin'),
('João Silva', 'joao@email.com', 'senha123', 'comprador'),
('Maria Santos', 'maria@email.com', 'senha456', 'comprador');

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

-- Verificar dados inseridos
SELECT 'Plataformas:' as info;
SELECT * FROM plataforma;

SELECT 'Moedas:' as info;
SELECT m.id_moeda, p.descricao_plataforma, m.quantidade, m.valor 
FROM moeda m 
JOIN plataforma p ON m.plataforma_id = p.id_plataforma 
ORDER BY p.descricao_plataforma, m.quantidade;

SELECT 'Usuários:' as info;
SELECT * FROM usuarios;
