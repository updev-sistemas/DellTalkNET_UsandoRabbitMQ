# DellTalkNET_UsandoRabbitMQ

## Docker para instalação do RabbitMQ

```YML
version: "3.2"
services:
  rabbitmq:
    image: rabbitmq:3-management-alpine
    container_name: 'rabbitmq'
    ports:
        - 5672:5672
        - 15672:15672
    volumes:
        - ~/.docker-conf/rabbitmq/data/:/var/lib/rabbitmq/
        - ~/.docker-conf/rabbitmq/log/:/var/log/rabbitmq
    networks:
        - rabbitmq_go_net

networks:
  rabbitmq_go_net:
    driver: bridge
```

## Instalação (Necessário ter o Docker instalado no Terminal)
Abra o CMD 
```bash
cd PASTA_CONTEM_COMPOSER_DOCKER_YML
docker-composer up
```

## Instalação via Terminal (Necessário ter o Terminal Windows)
Acesse esse link e proceda conforme instruções.
```url
https://community.chocolatey.org/packages/rabbitmq
```
