# CatalogoDeProdutos

Este é um projeto de catálogo de produtos, desenvolvido em .NET 8 com Blazor e MudBlazor. O objetivo do projeto é fornecer uma interface simples e intuitiva para gerenciar produtos, incluindo funcionalidades de criação, atualização, exclusão e listagem.

## Tecnologias Utilizadas

- **Backend**: .NET 8, ASP.NET Core
- **Frontend**: Blazor WebAssembly
- **Banco de Dados**: SQLite (ou InMemory para testes)
- **UI Framework**: MudBlazor

## Funcionalidades

- Criar, atualizar e excluir produtos.
- Listar produtos com paginação.
- Notificações de sucesso e erro ao realizar operações.
- Diálogo de confirmação para exclusão de produtos.

## Estrutura do Projeto

O projeto é dividido em duas principais camadas:

1. **CatalogoDeProdutos.Api**: Responsável pela API e manipulação de dados.
2. **CatalogoDeProdutos.Blazor**: Responsável pela interface do usuário e interação com a API.

## Como Executar o Projeto
1. Navegue até o diretório da API 
   cd CatalogoDeProdutos.Api

2. Restaure as dependências e execute o backend
   dotnet restore
   dotnet run
   
3. Navegue até o diretório do frontend
   cd ../CatalogoDeProdutos.Blazor

4. Restaure as dependências e execute o frontend
   dotnet restore
   dotnet run
   
### Pré-requisitos

- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)

### Passos para Execução

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu_usuario/CatalogoDeProdutos.git
   cd CatalogoDeProdutos
