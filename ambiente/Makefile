
api:
	docker-compose -f ./docker/docker-compose-api.yaml stop
	docker-compose -f ./docker/docker-compose-api.yaml build
	docker-compose -f ./docker/docker-compose-api.yaml up -d

logs:
	docker-compose -f ./docker/docker-compose-logs.yaml stop
	docker-compose -f ./docker/docker-compose-logs.yaml build
	docker-compose -f ./docker/docker-compose-logs.yaml up -d
	
db:
	docker-compose -f ./docker/docker-compose-db.yaml stop
	docker-compose -f ./docker/docker-compose-db.yaml build
	docker-compose -f ./docker/docker-compose-db.yaml up -d

infra:
	make db
	make logs
	
init:
	make infra
	make api
	
start:
	docker start db-mysql
	docker start cadastro_cliente_api
	docker start elasticsearch
	docker start kibana
	
stop:
	docker stop cadastro_cliente_api
	docker stop kibana
	docker stop elasticsearch
	docker stop db-mysql
	
reset:
	make stop
	docker rm cadastro_cliente_api
	docker rm kibana
	docker rm elasticsearch
	docker rm db-mysql
