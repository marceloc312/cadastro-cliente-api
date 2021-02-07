
api:
	docker-compose -f docker-compose-api.yaml stop
	docker-compose -f docker-compose-api.yaml build
	docker-compose -f docker-compose-api.yaml up

infra-create:
	docker-compose -f ./docker/docker-compose-infra.yaml stop
	docker-compose -f ./docker/docker-compose-infra.yaml build
	docker-compose -f ./docker/docker-compose-infra.yaml up

	docker-compose -f ./docker/docker-compose-logs.yaml stop 
	docker-compose -f ./docker/docker-compose-logs.yaml build
	docker-compose -f ./docker/docker-compose-logs.yaml up

infra-start:
	docker start db-mysql
	sysctl -w vm.max_map_count=262144
	docker start elasticsearch
	docker start kibana