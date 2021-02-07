CREATE DATABASE  IF NOT EXISTS `cad` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `cad`;

DROP TABLE IF EXISTS `cliente`;
CREATE TABLE `cliente` (  
  id int,
  nome varchar(200) DEFAULT NULL,
  cpf varchar(11) UNIQUE NOT NULL,
  inclusao datetime NOT NULL
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

/*OBS.: A CHAVE PRIMARIA DA TABELA CLIENTE SÓ INSERIDA APÓS 
A EXECUÇÃO DO SCRIPT QUE POPULA OS PRIMEIROS DADOS DE TESTES*/

DROP TABLE IF EXISTS `cliente_endereco`;

CREATE TABLE `cliente_endereco` (
  id int,   
  cliente_id int NOT NULL,
  logradouro varchar(150) NOT NULL,
  numero varchar(20) not null,
  complemento varchar(20),
  cidade varchar(150) NOT NULL,  
  estado varchar(2) NOT NULL,
  pais varchar(100) NOT NULL,
  cep  varchar(8) NOT NULL,
  inclusao datetime NOT NULL,
      FOREIGN KEY (cliente_id) REFERENCES cliente(id)
) ENGINE=MyISAM DEFAULT CHARSET=latin1;

ALTER TABLE cliente_endereco MODIFY Id INT NOT NULL PRIMARY KEY AUTO_INCREMENT;