# 🎮 EA FC Coins Manager - Endpoints Essenciais

## 📋 **Endpoints por Funcionalidade**

### 🔐 **Autenticação** (`/api/Auth`)
- `POST /api/Auth/login` - Login do usuário
- `POST /api/Auth/register` - Registro de usuário
- `GET /api/Auth/validate` - Validar token

### 🛒 **Carrinho** (`/api/Carrinho`)
- `POST /api/Carrinho/criar` - Criar carrinho
- `GET /api/Carrinho/{id}` - Obter carrinho por ID
- `GET /api/Carrinho/usuario/{idUser}` - Obter carrinho por usuário
- `POST /api/Carrinho/adicionar-item` - Adicionar item ao carrinho
- `DELETE /api/Carrinho/remover-item/{idItem}` - Remover item do carrinho

### 🎯 **Plataformas** (`/api/Plataforma`)
- `GET /api/Plataforma` - Listar todas as plataformas
- `GET /api/Plataforma/{id}` - Obter plataforma por ID
- `POST /api/Plataforma` - Criar nova plataforma
- `PUT /api/Plataforma/{id}` - Atualizar plataforma
- `DELETE /api/Plataforma/{id}` - Deletar plataforma

### 💰 **Moedas** (`/api/Moeda`)
- `GET /api/Moeda` - Listar todas as moedas
- `GET /api/Moeda/{id}` - Obter moeda por ID
- `GET /api/Moeda/plataforma/{plataformaId}` - Moedas por plataforma
- `POST /api/Moeda` - Criar nova moeda
- `PUT /api/Moeda/{id}` - Atualizar moeda

### 📦 **Pedidos** (`/api/Pedido`)
- `GET /api/Pedido` - Listar pedidos
- `GET /api/Pedido/{id}` - Obter pedido por ID
- `POST /api/Pedido` - Criar pedido
- `PUT /api/Pedido/{id}` - Atualizar pedido

### 💳 **Pagamentos** (`/api/Pagamento`)
- `POST /api/Pagamento` - Criar pagamento
- `GET /api/Pagamento/{id}` - Obter pagamento por ID
- `POST /api/Pagamento/webhook` - Webhook de pagamento

### 🛍️ **Checkout** (`/api/Checkout`)
- `POST /api/Checkout/finalizar-compra` - Finalizar compra
- `POST /api/Checkout/criar-usuario-temporario` - Criar usuário temporário

### 👥 **Usuários** (`/api/Usuarios`)
- `GET /api/Usuarios` - Listar usuários
- `GET /api/Usuarios/{id}` - Obter usuário por ID
- `POST /api/Usuarios` - Criar usuário
- `PUT /api/Usuarios/{id}` - Atualizar usuário
- `DELETE /api/Usuarios/{id}` - Deletar usuário

## 🚀 **Fluxo Principal do Usuário**

### **1. Seleção de Plataforma**
```bash
GET /api/Plataforma
```

### **2. Ver Moedas Disponíveis**
```bash
GET /api/Moeda/plataforma/{plataformaId}
```

### **3. Criar Carrinho**
```json
POST /api/Carrinho/criar
{
  "idUser": 1
}
```

### **4. Adicionar Moedas ao Carrinho**
```json
POST /api/Carrinho/adicionar-item
{
  "idCarrinho": 1,
  "idMoeda": 1,
  "quantidade": 2
}
```

### **5. Verificar Carrinho**
```bash
GET /api/Carrinho/1
```

### **6. Finalizar Compra**
```json
POST /api/Checkout/finalizar-compra
{
  "idCarrinho": 1,
  "email": "usuario@exemplo.com",
  "metodoPagamento": "PIX",
  "transactionId": 12345
}
```

## 📝 **Modelos de Request/Response**

### **Criar Carrinho**
```json
// Request
{
  "idUser": 1
}

// Response
{
  "success": true,
  "idCarrinho": 1,
  "idUser": 1,
  "createTime": "2025-01-07T20:47:54.522662Z",
  "message": "Carrinho criado com sucesso!"
}
```

### **Adicionar Item**
```json
// Request
{
  "idCarrinho": 1,
  "idMoeda": 1,
  "quantidade": 2
}

// Response
{
  "success": true,
  "message": "Item adicionado ao carrinho com sucesso!",
  "item": {
    "idItem": 1,
    "idCarrinho": 1,
    "idMoeda": 1,
    "quantidade": 2,
    "moeda": {
      "idMoeda": 1,
      "quantidade": 100000,
      "valor": 45.00,
      "plataformaNome": "PlayStation 5"
    }
  }
}
```

### **Finalizar Compra**
```json
// Request
{
  "idCarrinho": 1,
  "email": "usuario@exemplo.com",
  "metodoPagamento": "PIX",
  "transactionId": 12345
}

// Response
{
  "success": true,
  "message": "Compra finalizada com sucesso!",
  "pedido": {
    "idPedido": 1,
    "total": 90.00,
    "status": "Pendente",
    "dataPedido": "2025-01-07T20:47:54.522662Z"
  }
}
```

## 🔧 **Configuração**

- **Base URL**: `http://localhost:5041`
- **Swagger**: `http://localhost:5041/swagger/index.html`
- **Autenticação**: Desabilitada (todos os endpoints são públicos)
- **Banco**: PostgreSQL
- **ORM**: Entity Framework Core

## 📊 **Status dos Endpoints**

✅ **Funcionais**: Todos os endpoints listados estão funcionando
✅ **Testados**: Endpoints principais testados e validados
✅ **Documentados**: Swagger atualizado com schemas limpos
✅ **Limpos**: Removidos endpoints de debug e desnecessários
