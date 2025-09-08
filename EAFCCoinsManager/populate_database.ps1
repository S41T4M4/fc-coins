# Script PowerShell para popular o banco EAFCCoinsManager
# Execute este script para aplicar migrations e popular com dados de exemplo

Write-Host "🚀 Iniciando população do banco EAFCCoinsManager..." -ForegroundColor Green

# 1. Aplicar migrations
Write-Host "📦 Aplicando migrations..." -ForegroundColor Yellow
try {
    dotnet ef database update
    Write-Host "✅ Migrations aplicadas com sucesso!" -ForegroundColor Green
} catch {
    Write-Host "❌ Erro ao aplicar migrations: $($_.Exception.Message)" -ForegroundColor Red
    exit 1
}

# 2. Verificar se o PostgreSQL está rodando
Write-Host "🔍 Verificando conexão com PostgreSQL..." -ForegroundColor Yellow
try {
    $connectionString = "Host=localhost;Port=5432;Database=eacoins_db;Username=postgres;Password=Staff4912;"
    $connection = New-Object Npgsql.NpgsqlConnection($connectionString)
    $connection.Open()
    $connection.Close()
    Write-Host "✅ Conexão com PostgreSQL OK!" -ForegroundColor Green
} catch {
    Write-Host "❌ Erro de conexão: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "💡 Certifique-se de que o PostgreSQL está rodando e a senha está correta" -ForegroundColor Yellow
    exit 1
}

# 3. Executar script SQL
Write-Host "📊 Populando banco com dados de exemplo..." -ForegroundColor Yellow
try {
    # Usar psql para executar o script
    $env:PGPASSWORD = "Staff4912"
    psql -h localhost -p 5432 -U postgres -d eacoins_db -f "seed_data.sql"
    Write-Host "✅ Banco populado com sucesso!" -ForegroundColor Green
} catch {
    Write-Host "❌ Erro ao popular banco: $($_.Exception.Message)" -ForegroundColor Red
    Write-Host "💡 Certifique-se de que o psql está instalado e no PATH" -ForegroundColor Yellow
}

# 4. Verificar dados inseridos
Write-Host "🔍 Verificando dados inseridos..." -ForegroundColor Yellow
try {
    $query = @"
    SELECT 'Plataformas:' as info;
    SELECT COUNT(*) as total_plataformas FROM plataforma;
    
    SELECT 'Moedas:' as info;
    SELECT COUNT(*) as total_moedas FROM moeda;
    
    SELECT 'Usuários:' as info;
    SELECT COUNT(*) as total_usuarios FROM usuarios;
"@
    
    $env:PGPASSWORD = "Staff4912"
    psql -h localhost -p 5432 -U postgres -d eacoins_db -c "$query"
    
    Write-Host "🎉 Banco populado com sucesso!" -ForegroundColor Green
    Write-Host "📊 Dados inseridos:" -ForegroundColor Cyan
    Write-Host "   - 7 Plataformas (PS5, PS4, Xbox Series X, Xbox One, PC Origin, PC Steam, Switch)" -ForegroundColor White
    Write-Host "   - 28 Moedas (4 pacotes por plataforma)" -ForegroundColor White
    Write-Host "   - 3 Usuários (1 admin + 2 compradores)" -ForegroundColor White
    
} catch {
    Write-Host "❌ Erro ao verificar dados: $($_.Exception.Message)" -ForegroundColor Red
}

Write-Host "🚀 Pronto! Seu banco está populado e pronto para uso!" -ForegroundColor Green
