
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
	#remove os containers
	docker rm cadastro_cliente_api
	docker rm kibana
	docker rm elasticsearch
	docker rm db-mysql
	#remove as imagens
	docker rmi cadastro_cliente_api
	docker rmi db_mysql



gc-up:
	#prepara a imagem para o container
	make api
	docker tag cadastro_cliente_api gcr.io/devops-learn-304501/cadcliente-api:v1
	docker push gcr.io/devops-learn-304501/cadcliente-api:v1

	#kubernetes
	gcloud container clusters create cadclienteapi
	kubectl apply -f deployment.yaml
	kubectl apply -f service.yaml

k8s-down:
	#kubernetes
	kubectl delete deployments cadcliente-api-deployment
	kubectl delete services cadcliente-api-service
	gcloud container clusters delete cadclienteapi

	#exclui a imagem do Gogle Artifact Registry
	gcloud container images delete gcr.io/devops-learn-304501/cadcliente-api:v1  --force-delete-tags --quiet