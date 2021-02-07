USE `cad`;

INSERT INTO cliente(id,nome,cpf,inclusao) VALUES(1,"Marcelo Costa","93939714046",CURRENT_TIME);
INSERT INTO cliente(id,nome,cpf,inclusao) VALUES(2,"Tereza Da Silva","58939041097",CURRENT_TIME);
INSERT INTO cliente(id,nome,cpf,inclusao) VALUES(3,"Renata Cristina Alves","89781767049",CURRENT_TIME);
INSERT INTO cliente(id,nome,cpf,inclusao) VALUES(4,"Olegario Martins Souzas","08991157050",CURRENT_TIME);

ALTER TABLE cliente MODIFY id INT NOT NULL PRIMARY KEY AUTO_INCREMENT;