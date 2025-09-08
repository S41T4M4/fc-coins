# Etapa 1: Build do projeto
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia os arquivos do projeto
COPY . ./

# Restaura as dependências e compila o projeto
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Etapa 2: Criação da imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copia os arquivos publicados
COPY --from=build /out .

# Exposição da porta
EXPOSE 8080


# Comando para rodar o aplicativo
ENTRYPOINT ["dotnet", "EAFCCoinsManager.dll"]
