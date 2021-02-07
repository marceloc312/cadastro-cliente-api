# Api responsável pela manutenção dos dados cadastrais de Endereços do Cliente
A Api de Cadastro de Cliente tem as responsabilidades listadas abaixo:

* Busca um Cliente espécifico pelo CPF.
* Lista todos os endereços de um clitente específico pelo ID do Cliente.
* Busca um endereço específico para um Cliente, usando como critério o Id do Cliente e Id do Endereço, para garantir que um endereço de outro cliente não seja obtido por outro cliente, indevidamente.
* Altera um endereço espécifico de um Cliente específico, usando como critério o Id do Cliente e Id do Endereço, para garantir que um endereço de outro cliente não seja alterado, indevidamente por outro cliente.
* Lista todos os Estados/UF do Brasil, utilizando a Api disponibilizada pelo IBGE.
* Lista todos os Municípios/Cidades de um estado específico, utilizando a Api disponibilizada pelo IBGE.
* Recupera um endereço válido pelo CEP, utilizando a Api Via CEP.

# Implementações adicionais
* Todas as ações são logadas e podem ser vistas no console
* Todos os erros são tratados e seguem o padrão de Design de Api, disponibilizado pela Microsoft
* Todas as funcionalidades da aplicação são cobertas por Testes Unitário e Integrados
* Foi implementado o padrão OpenApi 3.0, para disponibilizar a documentação online. O Swagger está disponível em: https://localhost:5001/swagger/index.html e http://localhost:5000/swagger/index.html para execução local, e para execução no Docker, no endereço http://localhost:8080/swagger/index.html

# Disponibilização de Teste e validação
Para facilitar o teste e falidação da aplicação, ela está disponível em containers, bem como a infraestrutura necessária. Todos os dados para testes são criados ao criar a infraestrutra no Docker.
Na raíz do repositório existe o arquivo de Postman, utilizado nos testes: teste-pan.postman_collection.json

# Tecnologias utilizadas
Para o desenvolvimento da aplicação foram utilizadas as seguintes tecnologias:
* .Net Core 3.1
* Banco de dados MySql
* xUnit para Testes
* Swagger
