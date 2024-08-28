# Projeto de Estacionamento

## Solução do Desafio

Este projeto é um sistema de gerenciamento de estacionamento. Abaixo estão os detalhes da implementação:

### Tecnologias Utilizadas

- **Banco de Dados**: SQLite
- **Back-End**: ASP.NET Core, C#
- **Front-End**: Vue.js

### Visão Geral

O sistema permite aos usuários cadastrar, atualizar, deletar e finalizar a saída de veículos do estacionamento. O fluxo de trabalho inclui:

1. **Cadastro e Busca de Registros**: Os dados são armazenados e recuperados do banco de dados SQLite através de migrações e APIs.
2. **API**: O backend oferece APIs que permitem criar, ler, atualizar e excluir dados dos veículos e calcular o valor da tarifa de estacionamento.
3. **Front-End**: Implementado em Vue.js, permitindo interação do usuário com o sistema, incluindo o cálculo de tarifas no front-end.

As requisições da API foram testadas com Postman e os mesmos foram positivos.

### Arquitetura

#### Back-End

O back-end foi desenvolvido com ASP.NET Core, utilizando Entity Framework Core para interagir com o banco de dados SQLite.

- **Entity Framework Core**: Utilizado para gerenciar o contexto do banco de dados e as migrações.
- **Migrations**: Gerencia as mudanças no esquema do banco de dados.

#### Front-End

O front-end foi desenvolvido com Vue.js e comunica-se com o back-end através de chamadas API. As funcionalidades incluem:

- Cadastro de veículos
- Atualização de registros
- Exclusão de veículos
- Finalização da saída e cálculo de tarifas

### Considerações Finais

Para executar o projeto, siga as instruções abaixo:

#### Back-End

1. **Configuração**: Utilize o Visual Studio 2022 (versão 8) para abrir o arquivo `Estacionamento.sln`.
2. **Instalação de Dependências**:
   ```bash
   dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
   dotnet add package Microsoft.EntityFrameworkCore
   dotnet add package Microsoft.EntityFrameworkCore.Design
   dotnet add package Microsoft.EntityFrameworkCore.Sqlite
   dotnet run

#### Front-End
    npm install
    npm run dev

