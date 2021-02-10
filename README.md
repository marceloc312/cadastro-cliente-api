# Api responsável pela manutenção dos dados cadastrais de Endereços do Cliente
A Api de Cadastro de Cliente tem as responsabilidades listadas abaixo:

* Busca um Cliente espécifico pelo CPF.
* Lista todos os endereços de um clitente específico pelo ID do Cliente.
* Busca um endereço específico para um Cliente, usando como critério o Id do Cliente e Id do Endereço, para garantir que um endereço de outro cliente não seja obtido indevidamente.
* Altera um endereço específico de um Cliente específico, usando como critério o Id do Cliente e Id do Endereço, para garantir que um endereço de outro cliente não seja alterado indevidamente por outro cliente.
* Lista todos os Estados/UF do Brasil, utilizando a Api disponibilizada pelo IBGE.
* Lista todos os Municípios/Cidades de um estado específico, utilizando a Api disponibilizada pelo IBGE.
* Recupera um endereço válido pelo CEP, utilizando a Api Via CEP.

# Implementações adicionais
* Todas as ações são logadas e podem ser vistas no console e no Elasticsearch, utilizando a interface do Kibana, que pode ser acessado em http://localhost:5601/
OBS.: PODE SER NECESSARIO AGUARDAR ALGUNS MINUTOS ATE QUEO ELASTICSEARCH ESTEJA ACESSIVEL.
* Todos os erros são tratados e seguem o padrão de Design de Api, disponibilizado pela Microsoft
* Todas as funcionalidades da aplicação são cobertas por Testes Unitários e Integrados
* Foi implementado o padrão OpenApi 3.0, para disponibilizar a documentação online. O Swagger está disponível em: https://localhost:5001/swagger/index.html e http://localhost:5000/swagger/index.html para execução local, e no endereço http://localhost:8080/swagger/index.html, para execução no Docker,

# Disponibilização de Teste e validação
Para facilitar o teste e validação da aplicação, ela está disponível em containers, bem como a infraestrutura necessária. Todos os dados para testes são criados ao criar a infraestrutra no Docker.
Na raíz do repositório existe o arquivo de Postman, utilizado nos testes: *teste-pan.postman_collection.json*

# Tecnologias utilizadas
Para o desenvolvimento da aplicação foram utilizadas as seguintes tecnologias:
* .Net Core 3.1
* Banco de dados MySql
* xUnit para Testes
* Swagger
* Docker
* Docker Compose 3.7
* Elasticsearch
* Kibana

# Subindo o Ambiente em Container
Toda a aplicação pode ser testada pelo usuário sem a necessidade de depuração de código, ou hospedagem, utilizando apenas containerização. No entanto, é necessário observar os pré-requisitos abaixo.

# Pré-requisitos
* Para os ambientes Windows e Mac OS, é necessário ter o Docker Desktop For Windows/Mac, e o GNU instalados. 
* Para a execução no Linux é necessário o Docker juntamente com o docker-compose versão 1.26.2+.

# Instruções de execução

Utilize a ferramente de linha de comando que preferir.
1. Clone o repositório em uma pasta vazia com o seguinte comando: git clone git@github.com:marceloc312/cadastro-cliente-api.git
2. Na ferramente de linha de comando, navegue até a pasta do repositório e vá até a pasta ambiente. Exemplo: cd .\cadastro-cliente-api\ambiente\
3. Digite o comando *make init*. Esse comando faz o build da imagem docker da Api, cria a infraestrutura de banco de dados, elasticsearch e kibana, popula as tabelas com os dados necessários para os testes e sobe os containers.

Prontinho! Basta testar a aplicação.


*OBSERVAÇÃO: 
O PASSO A PASSO DESCRITO AQUI TEM COMO OBJETIVO ILUSTRAR A DISPONIBILIZAÇÃO DE CÓDIGO FONTE E DEMO, PARA QUE QUALQUER PESSOA POSSA TESTA-LÁ, INDEPENDENTE DE GRANDE CONHECIMENTO TÉCNICO.
TAMBÉM FORAM FEITAS VÁRIAS IMPLEMENTAÇÕES E REFATORAÇÕES DE VÁRIOS NÍVEIS, COM A INTENÇÃO DE ILUSTRAR O PROCESSO DE Git Flow.*
