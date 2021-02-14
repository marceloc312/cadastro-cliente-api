
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

kslogs-up:
	#Cria infra Logs
	kubectl apply -f ./k8s/infra/logs/elasticsearch-ss.yaml

kslogs-down:
	#Delete Logs
	kubectl delete statefulsets elasticsearch-logging
	kubectl delete services elasticsearch-logging

ksdb-up:
	#Cria infra banco de dados
	kubectl apply -f ./k8s/infra/mysql/pv.yaml
	kubectl apply -f ./k8s/infra/mysql/deployment.yaml
	kubectl apply -f ./k8s/infra/mysql/loadbalance.yaml

ksdb-down:
	kubectl delete deployments mysql
	kubectl delete services mysql
	kubectl delete PersistentVolumeClaim mysql-pv-claim

ksapi-up:
	#Cria Api
	kubectl apply -f ./k8s/app/deployment.yaml
	kubectl apply -f ./k8s/app/loadbalance.yaml

ksapi-down:
	kubectl delete deployments cadcliente-api-deployment
	kubectl delete services cadcliente-api-service

gcks-up:
	#gcloud container clusters create cadclienteapi
	make kslogs-up
	make ksdb-up
	make ksapi-up

gcup:
	#prepara a imagem para o container
	make api
	docker tag cadastro_cliente_api gcr.io/devops-learn-304501/cadcliente-api:v1
	docker push gcr.io/devops-learn-304501/cadcliente-api:v1

	#kubernetes
	make gcks

k8s-down:
	#kubernetes
	make ksdb-down
	make kslogs-down
	make ksapi-down


	gcloud container clusters delete cadclienteapi

	#exclui a imagem do Gogle Artifact Registry
	gcloud container images delete gcr.io/devops-learn-304501/cadcliente-api:v1  --force-delete-tags --quiet