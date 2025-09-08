# EAFCCoinsManager API Documentation

## Visão Geral
API para gerenciamento de compra de moedas do EA FC (FIFA). Permite que usuários escolham plataformas, adicionem moedas ao carrinho e finalizem compras.

## Autenticação
A API utiliza JWT Bearer Token. Inclua o token no header `Authorization: Bearer <token>` para endpoints protegidos.

## Endpoints

### Autenticação (`/api/auth`)

#### POST `/api/auth/login`
Faz login do usuário.
```json
{
  "email": "usuario@email.com",
  "password": "senha123"
}
```

#### POST `/api/auth/register`
Registra novo usuário.
```json
{
  "email": "usuario@email.com",
  "password": "senha123"
}
```

#### GET `/api/auth/validate`
Valida token JWT (requer autenticação).

### Plataformas (`/api/plataforma`)

#### GET `/api/plataforma`
Lista todas as plataformas disponíveis.

#### GET `/api/plataforma/{id}`
Busca plataforma por ID.

#### POST `/api/plataforma`
Cria nova plataforma (admin).
```json
{
  "descricao_plataforma": "PlayStation 5"
}
```

### Moedas (`/api/moeda`)

#### GET `/api/moeda`
Lista todas as moedas disponíveis.

#### GET `/api/moeda/{id}`
Busca moeda por ID.

#### GET `/api/moeda/plataforma/{plataformaId}`
Lista moedas por plataforma.

#### POST `/api/moeda`
Cria nova moeda (admin).
```json
{
  "plataforma_id": 1,
  "quantidade": 100000,
  "valor": 50.00
}
```

#### PUT `/api/moeda/{id}`
Atualiza moeda (admin).

### Carrinho (`/api/carrinho`) - Requer Autenticação

#### POST `/api/carrinho/criar`
Cria novo carrinho.
```json
{
  "idUser": 1
}
```

#### GET `/api/carrinho/{id}`
Busca carrinho por ID.

#### GET `/api/carrinho/usuario/{idUser}`
Busca carrinho por usuário.

#### POST `/api/carrinho/adicionar-item`
Adiciona item ao carrinho.
```json
{
  "idCarrinho": 1,
  "idMoeda": 1,
  "quantidade": 2
}
```

#### DELETE `/api/carrinho/remover-item/{idItem}`
Remove item do carrinho.

### Pedidos (`/api/pedido`) - Requer Autenticação

#### GET `/api/pedido/{id}`
Busca pedido por ID.

#### GET `/api/pedido/usuario/{userId}`
Lista pedidos do usuário.

#### POST `/api/pedido`
Cria novo pedido.
```json
{
  "id_user": 1,
  "total": 100.00
}
```

#### POST `/api/pedido/{pedidoId}/itens`
Adiciona item ao pedido.
```json
{
  "id_moeda": 1,
  "quantidade": 2,
  "Preco_unitario": 50.00
}
```

#### POST `/api/pedido/{pedidoId}/checkout`
Finaliza checkout do pedido.
```json
{
  "metodoPagamento": "PIX",
  "transactionId": 12345
}
```

#### PUT `/api/pedido/{pedidoId}/status`
Atualiza status do pedido.

### Checkout (`/api/checkout`)

#### POST `/api/checkout/finalizar-compra`
Finaliza compra completa (do carrinho ao pedido).
```json
{
  "idCarrinho": 1,
  "email": "usuario@email.com",
  "metodoPagamento": "PIX",
  "transactionId": 12345
}
```

#### POST `/api/checkout/criar-usuario-temporario`
Cria usuário temporário para checkout.
```json
"usuario@email.com"
```

### Pagamentos (`/api/pagamento`)

#### GET `/api/pagamento/pedido/{pedidoId}`
Busca pagamento por pedido.

#### PUT `/api/pagamento/{id}/status`
Atualiza status do pagamento.
```json
"Aprovado"
```

#### POST `/api/pagamento/webhook`
Endpoint para webhooks de pagamento.

### Usuários (`/api/v1/usuarios/`)

#### POST `/api/v1/usuarios/novo_usuario`
Cria novo usuário.

#### GET `/api/v1/usuarios/get_all_usuarios`
Lista todos os usuários.

#### GET `/api/v1/usuarios/get_user_by_id`
Busca usuário por ID.

#### PUT `/api/v1/usuarios/update_user`
Atualiza usuário.

#### DELETE `/api/v1/usuarios/remove_user`
Remove usuário.

## Fluxo de Compra Recomendado

1. **Buscar Plataformas**: `GET /api/plataforma`
2. **Buscar Moedas**: `GET /api/moeda/plataforma/{plataformaId}`
3. **Criar Carrinho**: `POST /api/carrinho/criar`
4. **Adicionar Itens**: `POST /api/carrinho/adicionar-item`
5. **Finalizar Compra**: `POST /api/checkout/finalizar-compra`

## Status de Pedidos
- `Pendente`: Aguardando pagamento
- `Processando`: Pagamento confirmado, processando
- `Concluído`: Entrega realizada
- `Cancelado`: Pedido cancelado

## Status de Pagamentos
- `Pendente`: Aguardando confirmação
- `Aprovado`: Pagamento confirmado
- `Rejeitado`: Pagamento rejeitado
- `Estornado`: Pagamento estornado
