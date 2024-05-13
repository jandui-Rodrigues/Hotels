#  Boas-vindas ao repositório do projeto Hotel

## Sobre o projeto

Sua empresa do coração começou a desenvolver um software de booking de várias redes de hotéis.
Este projeto é uma API responsável pelo controle de cidades, hotéis e quartos disponíveis para realizar reservas.

### Habilidades Trabalhadas

- Entender do funcionamento do ASP.NET e como ele se integra ao C#.
- Entender do funcionamento do banco de dados.
- Criar operações de manipulação de banco de dados em uma API.

<details>
  <summary><strong>Clonando o Repositorio </strong></summary><br />

  1. Clone o repositório

  - Use o comando: `git clone git@github.com:tryber/csharp-001-projeto-trybe-hotel.git`.
  - Entre na pasta do repositório que você acabou de clonar:
    - `cd csharp-001-projeto-trybe-hotel`

  2. Instale as dependências
  
  - Entre na pasta `src/`.
  - Execute o comando: `dotnet restore`.
  
</details>

<details>
   <summary><strong>Sobre o Projeto</strong></summary><br />

Esta é uma API que será utilizada em uma aplicação de booking de várias redes de hotéis.
Exitem rotas das entidades acerca das cidades, hotéis e quartos que servirão para, no futuro, realizar o booking de pessoas clientes.

Esta disponibilizado o diagrama de entidade-relacionamento, alem de um container na qual você poderá utilizar um banco de dados.

O sistema está dividido em diretórios específicos para auxiliar na organização e desenvolvimento do projeto.

- `Controllers/`: Este diretório armazena os arquivos com as lógicas dos controllers da aplicação. Os métodos a serem desenvolvidos estão prontos mas sem implementação alguma, o que você desenvolverá ao longo do projeto.
<br />

- `Models/`: Este diretório armazena os arquivos com as models do banco de dados. As models `City`, `Hotel` e `Room` serão as instruções para as tabelas `Cities`, `Hotels` e `Rooms`. Lembre-se, o nome da tabela não é dado pelo nome da model mas sim pelo nome do `DBSet<model>` presente no contexto.
<br />

- `DTO/`: Este diretório armazena as classes de DTO. Algumas rotas esperam as `responses` baseadas nestes DTOs. Você pode conferir isso pelo requisito do projeto e pelo retorno dos métodos dos `repositories`.
<br />

- `Repository/`: Este diretório armazena as lógicas que farão a interação com o banco de dados. Os métodos de cada requisito já estão criados e você deverá incluir a implementação de cada um desses métodos respeitando o retorno do DTO. Além disso, você terá o arquivo `TrybeHotelContext` com o contexto para a conexão com o banco de dados. Todos os `repository` e o `context` possuem interfaces que estão nesse diretório e fornecem o contrato para essas classes.
<br />

</details>
<details id='der'>
  <summary><strong>🎲 Banco de Dados</strong></summary>
  <br/>

  Para o desenvolvimento, o time de produto disponibilizou um *Diagrama de Entidade-Relacionamento (DER)* para construir a modelagem do banco de dados. Com essa imagem você já consegue saber:
  - Como nomear suas tabelas e colunas;
  - Quais são os tipos de suas colunas;
  - Relações entre tabelas.

    ![banco de dados](img/der.png)

  O diagrama infere 05 tabelas:
  - ***Cities***: tabela que armazenará um conjunto de cidades nas quais os hotéis estão localizados (já desenvolvida).
  - ***Hotels***: tabela que armazenará os hotéis da nossa aplicação. Note que informamos o `CityId`, atributo que armazenará o id da cidade (já desenvolvida).
  - ***Rooms***: tabela que armazenará os quartos de cada hotel da nossa aplicação. Note que informamos o `HotelId`, atributo que armazenará o id do hotel (já desenvolvida).
  - ***Users***: tabela que armazenará as pessoas usuárias do sistema.
  - ***Bookings***: tabela que armazenará as reservas de quartos de hotéis. Note que informamos os atributos `UserId`, que armazenará o id da pessoa usuária e `RoomId`, que armazenará o id do quarto reservado.

  Acerca dos relacionamentos, pelo diagrama de entidade-relacionamento temos:
  - Uma cidade pode ter vários hotéis.
  - Um hotel pode ter vários quartos.
  - Uma pessoa usuária pode ter várias reservas.
  - Um quarto pode ter várias reservas.

  ⚠️ **Você poderá criar migrations para visualizar o banco de dados**

</details>

<details>
<summary><strong>🐳 Docker</strong></summary><br />

Para auxiliar no desenvolvimento, este projeto possui um arquivo do docker compose para subir um serviço do banco de dados `Azure Data Studio`. Este banco de dados possui a mesma arquitetura do `SQL Server`.

Para subir o serviço, utilize o comando:

```shell
docker-compose up -d --build
```

Para conectar ao seu sistema de gerenciamento de banco de dados, utilize as seguintes credenciais:

- `Server`: localhost
- `User`: sa
- `Password`: TrybeHotel12!
- `Database`: TrybeHotel
- `Trust server certificate`: true

Para criar o contexto do banco de dados na sua aplicação, utilize como connection string:

```csharp
var connectionString = "Server=localhost;Database=TrybeHotel;User=SA;Password=TrybeHotel12!;TrustServerCertificate=True";
```

</details>
