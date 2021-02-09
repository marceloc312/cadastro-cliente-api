
api:
	docker-compose -f ./docker/docker-compose-api.yaml stop
	docker-compose -f ./docker/docker-compose-api.yaml build
	docker-compose -f ./docker/docker-compose-api.yaml up

infra:
	docker-compose -f ./docker/docker-compose-db.yaml stop
	docker-compose -f ./docker/docker-compose-db.yaml build
	docker-compose -f ./docker/docker-compose-db.yaml up -d

init:
	make infra
	make api
start:
	docker start db-mysql
	docker start cadastro_cliente_api
	sysctl -w vm.max_map_count=262144
	docker start elasticsearch
	docker start kibana
