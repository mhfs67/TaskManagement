# Task Management API

Este é um sistema de gerenciamento de tarefas desenvolvido em .NET Core 8, utilizando C# e MySQL como banco de dados. O sistema permite que usuários organizem e monitorem suas tarefas diárias, bem como colaborem com colegas de equipe.

## Pré-requisitos

- Docker Desktop
- .NET SDK 8.0 (para desenvolvimento local)
- Visual Studio 2022 ou VS Code (para desenvolvimento local)

## Tecnologias Utilizadas

- .NET Core 8
- Entity Framework Core
- MySQL
- Docker
- Swagger/OpenAPI

## Como Executar

1. Clone o repositório:
```bash
git clone https://seu-repositorio/TaskManagement.git
cd TaskManagement
```

2. Execute o projeto usando Docker Compose:
```bash
docker-compose up --build
```

3. Acesse a API:
- Swagger UI: http://localhost:8080/swagger
- API Base URL: http://localhost:8080/api

## Estrutura do Projeto

O projeto está organizado seguindo os princípios de Clean Architecture:

- `Controllers/`: Endpoints da API REST
- `Models/`: Entidades do domínio
- `Data/`: Contexto do EF Core e repositórios
- `Services/`: Lógica de negócios
- `DTOs/`: Objetos de transferência de dados
- `Middlewares/`: Middleware personalizado
- `Extensions/`: Extensões de métodos

## Funcionalidades Principais

1. Gerenciamento de Projetos
   - Listagem de projetos
   - Criação de novos projetos
   - Remoção de projetos (com validações)

2. Gerenciamento de Tarefas
   - Visualização de tarefas por projeto
   - Criação de novas tarefas
   - Atualização de status e detalhes
   - Remoção de tarefas

3. Recursos Adicionais
   - Priorização de tarefas
   - Histórico de alterações
   - Comentários nas tarefas
   - Relatórios de desempenho

## Regras de Negócio

1. Prioridades de Tarefas
   - Níveis: baixa, média, alta
   - Imutável após criação

2. Restrições de Remoção
   - Projetos com tarefas pendentes não podem ser removidos

3. Limite de Tarefas
   - Máximo de 20 tarefas por projeto

4. Histórico de Alterações
   - Registro de todas as modificações
   - Rastreamento de usuário e data

## Desenvolvimento Local

1. Restaure os pacotes NuGet:
```bash
dotnet restore
```

2. Atualize o banco de dados:
```bash
dotnet ef database update
```

3. Execute o projeto:
```bash
dotnet run
```

## Endpoints da API

### Projetos
- GET /api/projects - Lista todos os projetos
- POST /api/projects - Cria novo projeto
- DELETE /api/projects/{id} - Remove um projeto

### Tarefas
- GET /api/projects/{projectId}/tasks - Lista tarefas do projeto
- POST /api/projects/{projectId}/tasks - Cria nova tarefa
- PUT /api/tasks/{id} - Atualiza uma tarefa
- DELETE /api/tasks/{id} - Remove uma tarefa

### Relatórios
- GET /api/reports/performance - Relatório de desempenho (requer perfil gerente)

## Contribuindo

1. Fork o projeto
2. Crie uma branch para sua feature (`git checkout -b feature/AmazingFeature`)
3. Commit suas mudanças (`git commit -m 'Add some AmazingFeature'`)
4. Push para a branch (`git push origin feature/AmazingFeature`)
5. Abra um Pull Request

## Licença

Este projeto está licenciado sob a licença MIT - veja o arquivo LICENSE.md para detalhes.
