# Taurus API

Uma Web API de gerenciamento de tarefas desenvolvida com **ASP.NET Core**.

O projeto foi criado como estudo dos fundamentos de desenvolvimento backend, explorando criação de endpoints REST, manipulação de requisições HTTP, persistência de dados e organização básica de uma aplicação web.

Atualmente, os dados são armazenados em um arquivo JSON, simulando uma camada simples de persistência.

---

# Tecnologias utilizadas

- C#
- ASP.NET Core
- JSON

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

### PATCH `/tasks/{id}/change-title`

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
│
├── Controller.cs
├── Service.cs
├── Tarefa.cs
├── Requests.cs
├── Program.cs
│
├── data/
│   └── tasks.json
│
├── tests/
│
├── appsettings.json
├── appsettings.Development.json
└── taurus.csproj
```

---

# Organização

O projeto utiliza uma separação simples de responsabilidades.

## Controller.cs

Responsável pelos endpoints HTTP.

Funções:

- Receber requisições.
- Validar dados recebidos.
- Retornar respostas HTTP.

---

## Service.cs

Responsável pela lógica da aplicação.

Funções:

- Ler tarefas do arquivo JSON.
- Escrever alterações no arquivo.
- Criar, atualizar e remover tarefas.

---

## Tarefa.cs

Representa a entidade principal da aplicação.

---

## Requests.cs

Contém os modelos utilizados para receber dados das requisições.

---

# Persistência de dados

A aplicação utiliza um arquivo JSON como armazenamento:

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

# Recursos implementados

- API REST
- Endpoints HTTP
- Operações CRUD
- Async/Await
- Manipulação de JSON
- Identificadores utilizando Guid
- DTOs para requisições
- Validação de dados
- Separação entre Controller e Service

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