
api:
	docker-compose -f ./docker/docker-compose-api.yaml stop
	docker-compose -f ./docker/docker-compose-api.yaml build
	docker-compose -f ./docker/docker-compose-api.yaml up

infra:
	docker-compose -f ./docker/docker-compose-infra-db.yaml stop
	docker-compose -f ./docker/docker-compose-infra-db.yaml build
	docker-compose -f ./docker/docker-compose-infra-db.yaml up

init:
	make infra
	#make api
start:
	docker start db-mysql
	docker start cadastro_cliente_api
