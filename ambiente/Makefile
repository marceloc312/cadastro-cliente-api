
api:
	docker-compose -f docker-compose-api.yaml stop
	docker-compose -f docker-compose-api.yaml build
	docker-compose -f docker-compose-api.yaml up

infra:
	docker-compose -f docker-compose-infra.yaml stop
	docker-compose -f docker-compose-infra.yaml build
	docker-compose -f docker-compose-infra.yaml up

init:
	make infra
	make api