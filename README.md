# Golden Raspberry Awards API

Esta é uma API RESTful para ler a lista de indicados e vencedores da categoria Pior Filme do Golden Raspberry Awards.

## Requisitos do Sistema

- Ler o arquivo CSV dos filmes e inserir os dados em uma base de dados ao iniciar a aplicação.

## Requisitos da API

- Obter o produtor com maior intervalo entre dois prêmios consecutivos, e o que obteve dois prêmios mais rápido, seguindo a especificação de formato definida abaixo.

## Requisitos Não Funcionais

1. O web service RESTful deve ser implementado com base no nível 2 de maturidade de Richardson;
2. Devem ser implementados somente testes de integração. Eles devem garantir que os dados obtidos estão de acordo com os dados fornecidos na proposta;
3. O banco de dados deve estar em memória utilizando um SGBD embarcado (por exemplo, H2 ). Nenhuma instalação externa deve ser necessária;
4. A aplicação deve conter um readme com instruções para rodar o projeto e os testes de integração.

## Executando a aplicação

Para executar a aplicação, siga os seguintes passos:

1. Certifique-se de ter o .NET SDK 9.0 instalado em sua máquina.
2. Faça o clone deste repositório em sua máquina. https://github.com/jricardo-morais/GoldenRaspberryAwards.git
3. Abra o terminal ou prompt de comando na pasta raiz do projeto.
4. Executar o seguinte comando: `dotnet run --project .\GoldenRaspberryAwards.API\`.
5. A aplicação será iniciada.
6. Para utilizar a aplicação, acesse o endereço https://localhost:7019/swagger ou http://localhost:5019/swagger.

## Executando os testes

Para executar os testes automatizados do projeto, siga os seguintes passos:

1. Abra o terminal ou prompt de comando na pasta raiz do projeto.
2. Execute o seguinte comando: `dotnet test`.
3. Os testes serão executados e o resultado será exibido no console.

## Tecnologias utilizadas

- **Linguagem**: C#  
- **Framework**: .NET 9  
- **Banco de Dados**: SQL Server InMemory
- **ORM**: EntityFramework Core 9.0 InMemory
- **Testes**: xUnit  
- **Upload do Arquivo**: CSVHelper
