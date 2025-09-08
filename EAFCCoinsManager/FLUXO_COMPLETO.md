# FLUXO COMPLETO DA APLICAÇÃO EAFCCoinsManager

## 🎯 VISÃO GERAL
Sistema SAAS para compra de moedas do EA FC (FIFA) com fluxo completo de e-commerce.

## 📊 DIAGRAMA DO FLUXO

```
┌─────────────────────────────────────────────────────────────────────────────┐
│                           EAFCCoinsManager - Fluxo Completo                  │
└─────────────────────────────────────────────────────────────────────────────┘

1. INÍCIO (Frontend)
   │
   ▼
2. SELEÇÃO DE PLATAFORMA
   │ GET /api/plataforma
   │ → Lista: PS5, PS4, Xbox Series X, Xbox One, PC Origin, PC Steam, Switch
   │
   ▼
3. ESCOLHA DE MOEDAS
   │ GET /api/moeda/plataforma/{id}
   │ → Lista moedas disponíveis com preços
   │
   ▼
4. AUTENTICAÇÃO (Opcional)
   │ POST /api/auth/login (usuário existente)
   │ POST /api/auth/register (novo usuário)
   │ → Retorna JWT Token
   │
   ▼
5. GERENCIAMENTO DO CARRINHO
   │ POST /api/carrinho/criar
   │ POST /api/carrinho/adicionar-item
   │ GET /api/carrinho/usuario/{id}
   │ DELETE /api/carrinho/remover-item/{id}
   │
   ▼
6. CHECKOUT E FINALIZAÇÃO
   │ POST /api/checkout/finalizar-compra
   │ → Cria Pedido + Pagamento
   │ → Limpa Carrinho
   │ → Retorna dados da compra
   │
   ▼
7. PROCESSAMENTO DO PEDIDO
   │ GET /api/pedido/usuario/{id}
   │ PUT /api/pedido/{id}/status
   │ → Acompanhamento do status
   │
   ▼
8. PAGAMENTO
   │ GET /api/pagamento/pedido/{id}
   │ PUT /api/pagamento/{id}/status
   │ POST /api/pagamento/webhook
   │ → Processamento e confirmação
   │
   ▼
9. CONCLUSÃO
   │ → Entrega das moedas
   │ → Notificação por email
```

## 🔄 FLUXOS DETALHADOS

### FLUXO 1: COMPRA SIMPLES (Usuário Logado)
```
1. Usuário acessa o site
2. Escolhe plataforma → GET /api/plataforma
3. Escolhe moedas → GET /api/moeda/plataforma/{id}
4. Adiciona ao carrinho → POST /api/carrinho/adicionar-item
5. Finaliza compra → POST /api/checkout/finalizar-compra
6. Recebe confirmação com pedidoId e pagamentoId
```

### FLUXO 2: COMPRA SEM CADASTRO
```
1. Usuário acessa o site
2. Escolhe plataforma → GET /api/plataforma
3. Escolhe moedas → GET /api/moeda/plataforma/{id}
4. Adiciona ao carrinho → POST /api/carrinho/adicionar-item
5. Informa email → POST /api/checkout/criar-usuario-temporario
6. Finaliza compra → POST /api/checkout/finalizar-compra
7. Recebe confirmação por email
```

### FLUXO 3: GERENCIAMENTO ADMINISTRATIVO
```
1. Admin faz login → POST /api/auth/login
2. Gerencia plataformas → POST /api/plataforma
3. Gerencia moedas → POST /api/moeda
4. Acompanha pedidos → GET /api/pedido/usuario/{id}
5. Atualiza status → PUT /api/pedido/{id}/status
6. Processa pagamentos → PUT /api/pagamento/{id}/status
```

## 📋 ENDPOINTS POR FUNCIONALIDADE

### 🔐 AUTENTICAÇÃO
- `POST /api/auth/login` - Login do usuário
- `POST /api/auth/register` - Registro de novo usuário
- `GET /api/auth/validate` - Validar token JWT

### 🎮 PLATAFORMAS
- `GET /api/plataforma` - Listar todas as plataformas
- `GET /api/plataforma/{id}` - Buscar plataforma por ID
- `POST /api/plataforma` - Criar nova plataforma (Admin)

### 💰 MOEDAS
- `GET /api/moeda` - Listar todas as moedas
- `GET /api/moeda/{id}` - Buscar moeda por ID
- `GET /api/moeda/plataforma/{id}` - Moedas por plataforma
- `POST /api/moeda` - Criar nova moeda (Admin)
- `PUT /api/moeda/{id}` - Atualizar moeda (Admin)

### 🛒 CARRINHO (Requer Autenticação)
- `POST /api/carrinho/criar` - Criar carrinho
- `GET /api/carrinho/{id}` - Buscar carrinho por ID
- `GET /api/carrinho/usuario/{id}` - Carrinho do usuário
- `POST /api/carrinho/adicionar-item` - Adicionar item
- `DELETE /api/carrinho/remover-item/{id}` - Remover item

### 📦 PEDIDOS (Requer Autenticação)
- `GET /api/pedido/{id}` - Buscar pedido por ID
- `GET /api/pedido/usuario/{id}` - Pedidos do usuário
- `POST /api/pedido` - Criar pedido
- `POST /api/pedido/{id}/itens` - Adicionar item ao pedido
- `POST /api/pedido/{id}/checkout` - Finalizar checkout
- `PUT /api/pedido/{id}/status` - Atualizar status

### 💳 PAGAMENTOS
- `GET /api/pagamento/pedido/{id}` - Pagamento por pedido
- `PUT /api/pagamento/{id}/status` - Atualizar status
- `POST /api/pagamento/webhook` - Webhook de pagamento

### 🛍️ CHECKOUT
- `POST /api/checkout/finalizar-compra` - Finalizar compra completa
- `POST /api/checkout/criar-usuario-temporario` - Usuário temporário

### 👥 USUÁRIOS
- `POST /api/v1/usuarios/novo_usuario` - Criar usuário
- `GET /api/v1/usuarios/get_all_usuarios` - Listar usuários
- `GET /api/v1/usuarios/get_user_by_id` - Buscar usuário
- `PUT /api/v1/usuarios/update_user` - Atualizar usuário
- `DELETE /api/v1/usuarios/remove_user` - Remover usuário

## 🔄 ESTADOS DO SISTEMA

### Status de Pedidos:
- `Pendente` - Aguardando pagamento
- `Processando` - Pagamento confirmado, processando
- `Concluído` - Entrega realizada
- `Cancelado` - Pedido cancelado

### Status de Pagamentos:
- `Pendente` - Aguardando confirmação
- `Aprovado` - Pagamento confirmado
- `Rejeitado` - Pagamento rejeitado
- `Estornado` - Pagamento estornado

## 🎯 CASOS DE USO PRINCIPAIS

### 1. Compra Rápida
Usuário logado faz compra direta sem precisar criar carrinho.

### 2. Compra com Carrinho
Usuário adiciona múltiplos itens ao carrinho antes de finalizar.

### 3. Compra sem Cadastro
Usuário pode comprar informando apenas o email.

### 4. Gestão Administrativa
Admin gerencia plataformas, moedas e acompanha pedidos.

### 5. Acompanhamento de Pedidos
Usuário pode acompanhar status dos seus pedidos.

## 🔧 INTEGRAÇÕES NECESSÁRIAS

### Frontend:
- Integração com endpoints de autenticação
- Gerenciamento de estado do carrinho
- Interface de checkout
- Sistema de notificações

### Pagamentos:
- Gateway de pagamento (Stripe, PayPal, PagSeguro)
- Webhooks para confirmação automática
- Sistema de estorno

### Notificações:
- Envio de emails de confirmação
- Notificações de status do pedido
- Confirmação de entrega
