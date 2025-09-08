# EAFCCoinsManager Backend

Backend completo para o SAAS de compra de moedas do EA FC (FIFA). Implementa todo o fluxo de compra desde a seleção de plataformas até a finalização do pagamento.

## 🚀 Funcionalidades Implementadas

### ✅ Controllers Completos
- **AuthController**: Login, registro e validação de tokens JWT
- **PlataformaController**: Gerenciamento de plataformas (PS5, Xbox, PC, etc.)
- **MoedaController**: Gerenciamento de moedas por plataforma
- **CarrinhoController**: Adição e remoção de itens do carrinho
- **PedidoController**: Criação e gerenciamento de pedidos
- **PagamentoController**: Processamento de pagamentos
- **CheckoutController**: Fluxo completo de checkout
- **UsuariosController**: Gerenciamento de usuários

### ✅ Repositórios Implementados
- **MoedasRepository**: CRUD completo para moedas
- **PlataformaRepository**: CRUD para plataformas
- **PedidoRepository**: Gerenciamento de pedidos com relacionamentos
- **ItemPedidoRepository**: Gerenciamento de itens de pedido
- **PagamentoRepository**: Processamento de pagamentos

### ✅ Fluxo de Compra Completo
1. **Seleção de Plataforma**: Usuário escolhe a plataforma (PS5, Xbox, PC, etc.)
2. **Escolha de Moedas**: Seleciona quantidade e valor das moedas
3. **Carrinho**: Adiciona itens ao carrinho
4. **Cadastro/Login**: Sistema de autenticação JWT
5. **Checkout**: Finalização da compra com pagamento
6. **Processamento**: Acompanhamento do status do pedido

## 🛠️ Tecnologias Utilizadas

- **.NET 8**: Framework principal
- **Entity Framework Core**: ORM para banco de dados
- **PostgreSQL**: Banco de dados
- **JWT Bearer**: Autenticação
- **Swagger**: Documentação da API
- **CORS**: Configurado para frontend

## 📋 Pré-requisitos

- .NET 8 SDK
- PostgreSQL
- Visual Studio ou VS Code

## 🚀 Como Executar

1. **Clone o repositório**
2. **Configure a conexão do banco** em `appsettings.json`
3. **Execute as migrations**:
   ```bash
   dotnet ef database update
   ```
4. **Popule com dados de exemplo**:
   ```bash
   psql -d eacoins_db -f seed_data.sql
   ```
5. **Execute o projeto**:
   ```bash
   dotnet run
   ```

## 📚 Documentação da API

Acesse `/swagger` após executar o projeto para ver a documentação interativa da API.

Consulte também o arquivo `API_DOCUMENTATION.md` para detalhes completos dos endpoints.

## 🔐 Autenticação

A API utiliza JWT Bearer Token. Para endpoints protegidos, inclua no header:
```
Authorization: Bearer <seu_token>
```

## 📊 Estrutura do Banco

### Principais Tabelas
- **usuarios**: Usuários do sistema
- **plataforma**: Plataformas disponíveis (PS5, Xbox, etc.)
- **moeda**: Moedas por plataforma com preços
- **carrinho**: Carrinhos de compra
- **item_carrinho**: Itens no carrinho
- **pedido**: Pedidos finalizados
- **item_pedido**: Itens dos pedidos
- **pagamento**: Informações de pagamento

## 🔄 Fluxo de Compra

### 1. Frontend → Backend
```javascript
// 1. Buscar plataformas
GET /api/plataforma

// 2. Buscar moedas da plataforma
GET /api/moeda/plataforma/1

// 3. Criar carrinho
POST /api/carrinho/criar

// 4. Adicionar itens
POST /api/carrinho/adicionar-item

// 5. Finalizar compra
POST /api/checkout/finalizar-compra
```

### 2. Resposta do Backend
```json
{
  "pedidoId": 123,
  "total": 150.00,
  "status": "Processando",
  "pagamentoId": 456,
  "email": "usuario@email.com"
}
```

## 🎯 Próximos Passos

### Para o Frontend
1. **Integrar com os endpoints** criados
2. **Implementar autenticação JWT** no frontend
3. **Criar fluxo de checkout** completo
4. **Adicionar tratamento de erros** da API

### Melhorias Futuras
1. **Hash de senhas** (BCrypt)
2. **Integração com gateways de pagamento** (Stripe, PayPal)
3. **Sistema de notificações** por email
4. **Dashboard administrativo**
5. **Relatórios de vendas**
6. **Sistema de cupons/descontos**

## 📞 Suporte

Para dúvidas sobre a implementação, consulte:
- `API_DOCUMENTATION.md` - Documentação completa
- `seed_data.sql` - Dados de exemplo
- Swagger UI - Documentação interativa

## 🔧 Configurações Importantes

### CORS
Configurado para aceitar qualquer origem em desenvolvimento. Em produção, configure adequadamente.

### JWT
Chave secreta configurada em `Key.cs`. Em produção, use uma chave mais segura.

### Banco de Dados
String de conexão configurada para PostgreSQL. Ajuste conforme necessário.
