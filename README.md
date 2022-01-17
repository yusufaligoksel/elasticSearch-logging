# elasticSearch-logging

Projedeki logları bir nosql database de tutmak için CRUD methodlarını araştırıp denemiş olduğum bir poc projesidir.

elasticSearch, kibanayı ayağa kaldırmak için bkz. => https://youtu.be/X1_PFmgP8SU

docker-compose.yml file

version: "3.0"
services:
  elasticsearch:
    container_name: elastic-container
    image: docker.elastic.co/elasticsearch/elasticsearch:7.13.4
    environment:
      - xpack.security.enabled=false
      - "discovery.type=single-node"
    networks:
      - ek-net
    ports:
      - 9200:9200
  kibana:
    container_name: kibana-container
    image: docker.elastic.co/kibana/kibana:7.13.4
    environment:
      - ELASTICSEARCH_HOSTS=http://elastic-container:9200
    networks:
      - ek-net
    depends_on:
      - elasticsearch
    ports:
      - 5601:5601
networks:
  ek-net:
    driver: bridge

<img width="1528" alt="Ekran Resmi 2022-01-18 00 26 09" src="https://user-images.githubusercontent.com/37908301/149838821-24fd9f45-6b99-46bf-9780-f36d01ed4257.png">

<img width="1786" alt="Ekran Resmi 2022-01-18 00 26 41" src="https://user-images.githubusercontent.com/37908301/149838852-5e8ffc60-7b98-48a3-80df-ff6a0fb236de.png">

