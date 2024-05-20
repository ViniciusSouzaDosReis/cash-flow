## Sobre o projeto
Esta API, desenvolvida utilizando .NET 8, adota os princípios do **Domain-Driver-Design (DDD)** para oferecer uma solução estruturada e eficaz no gerenciamento de despesas pessoais. O principal objetivo é permitir o usuário registrarem suas despesas, detalhando informações como titulo, data e hora, descrição, valor e tipo de pagamento, com os dados armazenados de forma segura em um banco de dados MySQL.

A arquitetura da API baseia-se em REST, utilizando métodos HTTP padrão para uma comunicação eficiente e simples. Além disso, é completada por uma documentação Swagger, que proporciona uma interface gráfica interativa para que os desenvolvedores possam explorar e testar os endpoints de maneira fácil.

<!-- TODO: ADICIONAR IMAGEM -->

### Features

- **Domain-Driver-Design (DDD)**
- **Teste de Unidade**
- **Geração de relatórios**
- **RESTful API com documentação Swagger**

### Construído com
![badge-dot-net]
![badge-visual-studio]
![badge-my-sql]
![badge-swagger]

## Getting Started

Para obter uma cópia local funcionando, siga passos simples

### Requisitos

* Visual Studio versão 2022+ ou Visual Studio Code
* Windows 10+ ou Linux/MacOS com [.Net SDK](dot-net-sdk) instalado.
* MySql Server

### Instalação
1. Clone o repositório
```sh
git clone https://github.com/ViniciusSouzaDosReis/cash-flow.git
```
2. Preencha as informações no arquivo `appsettings.Development.json`
3. Execute a API.

<!-- Links -->
[dot-net-sdk]: https://dotnet.microsoft.com/en-us/download/dotnet/8.0

<!-- Badges -->
[badge-dot-net]: https://img.shields.io/badge/.NET-512BD4?logo=dotnet&logoColor=fff&style=for-the-badge
[badge-visual-studio]: https://img.shields.io/badge/Visual%20Studio-5C2D91?logo=visualstudio&logoColor=fff&style=for-the-badge
[badge-my-sql]: https://img.shields.io/badge/MySQL-4479A1?logo=mysql&logoColor=fff&style=for-the-badge
[badge-swagger]: https://img.shields.io/badge/Swagger-85EA2D?logo=swagger&logoColor=000&style=for-the-badge
[badge-git-hub-actions]: https://img.shields.io/badge/GitHub%20Actions-2088FF?logo=githubactions&logoColor=fff&style=for-the-badge
[badge-docker]: https://img.shields.io/badge/Docker-2496ED?logo=docker&logoColor=fff&style=for-the-badge

