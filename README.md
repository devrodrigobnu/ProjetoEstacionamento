# Projeto de Estacionamento

Solução do desafio:

Para elaboração do desafio, foram utilizados:

    -Banco de dados: sqlite3
    -BackEnd: Aps/Net, C#
    -FrontEnd: Vue.js

Visão geral: Os registros são cadastrados e buscados no banco de dados SQLITE3, feito através de migrations via api.
Api é responsável por buscar e cadastras as informações no banco de dados.
No front-end o usuário pode cadastrar, atualizar, deletar e finalizar a saída do veiculo do estacionamento, fiz o cálculo da tarifa pelo front.

As requisições da API foram testadas via Postman, verificando o retorno das mesmas.

Considerações finais:
Para executar a aplicação devem-se utilizar os seguintes programas/comandos.

# Backend: Visual Studio 2022 (versão 8) - Rodar o arquivo Estacionamento.sln e se tiver dificuldades pra instalar as dependências, tem opção de instalar direto no NuGet dentro do visual studio.
    Executar:
      -dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson
      -dotnet add package Microsoft.EntityFrameworkCore
      -dotnet add package Microsoft.EntityFrameworkCore.Design
      -dotnet add package Microsoft.EntityFrameworkCore.Sqlite
      -dotnet run (Obrigatóriamente precisa estar na pasta do projeto)

# Frontend: Visual studio code - Rodar a IDE dentro do projeto
      Executar:
       -npm install (instalar as dependências)
       -npm run dev (rodar o programa)

Navegação: via Chrome ou outro navegador, caminho: http://127.0.0.1:5173/ (préviamente configurado no json)
