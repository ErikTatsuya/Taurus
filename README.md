# Taurus API

Uma Web API de gerenciamento de tarefas desenvolvida com **ASP.NET Core**.

O projeto foi criado como estudo dos fundamentos de desenvolvimento backend, explorando criação de endpoints REST, manipulação de requisições HTTP, persistência de dados e organização básica de uma aplicação web.

Atualmente, os dados são armazenados em um arquivo JSON, simulando uma camada simples de persistência. A arquitetura foi preparada para futura migração para PostgreSQL.

---

# Tecnologias utilizadas

- C#
- ASP.NET Core
- JSON
- Entity Framework Core (preparado para PostgreSQL)

---

# Funcionalidades

## Gerenciamento de tarefas

A API permite:

- Criar tarefas.
- Listar todas as tarefas.
- Buscar uma tarefa específica.
- Alterar o título de uma tarefa.
- Marcar tarefas como concluídas.
- Remover tarefas.

Cada tarefa possui:

| Campo | Tipo | Descrição |
|---|---|---|
| Id | Guid | Identificador único |
| Title | string | Título da tarefa |
| Completed | bool | Indica se a tarefa foi concluída |
| CreatedAt | DateTime | Data de criação da tarefa |

---

# Endpoints

## Listar tarefas

### GET `/tasks`

Retorna todas as tarefas cadastradas.

---

## Buscar tarefa por ID

### GET `/tasks/{id}`

Retorna uma tarefa específica utilizando seu identificador.

Resposta caso não encontrada:

```
404 Not Found
```

---

## Criar tarefa

### POST `/tasks`

Cria uma nova tarefa.

### Corpo da requisição:

```json
{
  "title": "Estudar ASP.NET"
}
```

---

## Completar tarefa

### PATCH `/tasks/{id}/complete`

Marca uma tarefa como concluída.

---

## Alterar título

### PATCH `/tasks/{id}/title`

Altera o título de uma tarefa existente.

### Corpo da requisição:

```json
{
  "title": "Novo título"
}
```

---

## Remover tarefa

### DELETE `/tasks/{id}`

Remove uma tarefa pelo identificador.

---

# Estrutura do projeto

```
Taurus/
├── Program.cs
├── taurus.csproj
├── appsettings.json
├── appsettings.Development.json
├── README.md
├── .gitignore
├── Properties/
│   └── launchSettings.json
├── src/
│   ├── Models/
│   │   └── Tarefa.cs
│   ├── DTOs/
│   │   ├── CreateTarefaRequest.cs
│   │   └── ChangeTarefaTitleRequest.cs
│   ├── Controllers/
│   │   └── TarefaController.cs
│   ├── Services/
│   │   ├── ITarefaService.cs
│   │   └── TarefaService.cs
│   ├── Data/
│   │   ├── AppDbContext.cs
│   │   └── Repositories/
│   │       ├── ITarefaRepository.cs
│   │       ├── JsonTarefaRepository.cs
│   │       └── PgTarefaRepository.cs
│   └── Extensions/
│       └── ServiceCollectionExtensions.cs
├── Migrations/
│   ├── AppDbContextModelSnapshot.cs
│   └── 20260805034533_InitialCreate.cs
│   └── 20260805034533_InitialCreate.Designer.cs
└── tests/
    ├── taurus.http
    └── script_py/
```

---

# Organização

O projeto utiliza uma separação clara de responsabilidades baseada em camadas.

## src/Models/Tarefa.cs

Representa a entidade principal da aplicação.

## src/DTOs/

Contém os modelos utilizados para receber dados das requisições.

- `CreateTarefaRequest.cs` - Modelo para criação de tarefas
- `ChangeTarefaTitleRequest.cs` - Modelo para alteração de título

## src/Controllers/TarefaController.cs

Responsável pelos endpoints HTTP.

Funções:

- Receber requisições.
- Validar dados recebidos.
- Retornar respostas HTTP.

## src/Services/

Responsável pela lógica da aplicação.

- `ITarefaService.cs` - Interface do serviço
- `TarefaService.cs` - Implementação com regras de negócio

## src/Data/Repositories/

Implementa o padrão Repository para abstrair a persistência de dados.

- `ITarefaRepository.cs` - Interface do repositório
- `JsonTarefaRepository.cs` - Implementação atual usando arquivo JSON
- `PgTarefaRepository.cs` - Implementação futura usando PostgreSQL via EF Core

## src/Extensions/ServiceCollectionExtensions.cs

Configuração centralizada de injeção de dependência.

## src/Data/AppDbContext.cs

Contexto do Entity Framework Core para uso futuro com PostgreSQL.

---

# Persistência de dados

A aplicação atualmente utiliza um arquivo JSON como armazenamento:

```
data/tasks.json
```

As operações de alteração funcionam da seguinte forma:

```
Ler arquivo JSON
        ↓
Alterar dados em memória
        ↓
Salvar arquivo novamente
```

Essa abordagem foi utilizada para fins de aprendizado.

Em aplicações maiores, esse tipo de armazenamento seria substituído por um banco de dados.

---

# Migração para PostgreSQL

A arquitetura foi preparada para facilitar a migração de JSON para PostgreSQL:

1. O `ITarefaRepository` define o contrato para operações de dados
2. `JsonTarefaRepository` implementa a persistência atual em JSON
3. `PgTarefaRepository` está pronto para uso com PostgreSQL via Entity Framework Core
4. A troca entre implementações é feita apenas na configuração de DI em `ServiceCollectionExtensions.cs`

Para migrar:

1. Configure a string de conexão no `appsettings.json`
2. Altere a implementação registrada em `ServiceCollectionExtensions.cs`:
   ```csharp
   services.AddScoped<ITarefaRepository, PgTarefaRepository>();
   ```
3. Execute as migrations: `dotnet ef database update`

---

# Recursos implementados

- API REST
- Endpoints HTTP
- Operações CRUD
- Async/Await
- Manipulação de JSON
- Identificadores utilizando Guid
- DTOs para requisições
- Validação de dados
- Injeção de dependência
- Padrão Repository
- Separação entre Controller, Service e Repository
- Preparado para migração para PostgreSQL

---

# Possíveis melhorias futuras

- [ ] Adicionar Swagger/OpenAPI
- [ ] Implementar Rate Limiting
- [ ] Adicionar tratamento global de erros
- [ ] Adicionar logging
- [ ] Migrar persistência para PostgreSQL
- [ ] Criar testes automatizados
- [ ] Implementar autenticação

---

# Objetivo

Taurus foi desenvolvido como um projeto de estudo para entender os fundamentos de criação de APIs utilizando ASP.NET Core antes da utilização de bancos de dados e arquiteturas mais complexas.
