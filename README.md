# TaskManager
Simples Gerenciador de Tarefas feito com ASP.NET Core Web API e Blazor.

## Passos para configurar o projeto localmente
- Instalar o SQL Server Express e o Visual Studio com workload para desenvolvimento web.
- Apagar todos os arquivos da pasta Migrations dentro de TaskManager.Api, se houver algum, e o banco de dados TaskManager local, se houver.
- Rodar o migration do EntityFramework utilizando os seguintes comandos:

Criar arquivos de migration
```
dotnet ef migrations add InitialCreate
```
Aplicar migration no banco de dados
```
dotnet ef database update
```
Para rodar esses comandos é necessário que o working directory seja o do projeto da api, TaskManager.Api.

Com isso, um novo banco de dados será criado com o nome TaskManager, contendo uma tabela chamada Tarefas.

## Comandos para rodar o projeto
Restaurar as dependências
```
dotnet restore
```
Compilar
```
dotnet build
```
Rodar os testes
```
dotnet test
```
Para rodar, abrir a solução no Visual Studio e configurar para multiplos projetos inicializáveis. Inicializar o TaskManager.Api e TaskManager.App, em IIS Express.

## Banco de dados utilizado
- SQL Server Express 16.0.1000
