# 
# Create network for containers
#

docker network create --driver=bridge recruit-me-network

#
# Setup MySql
#

docker pull mysql/mysql-server

docker run -p 3306:3306 --net=recruit-me-network --name=rm-mysql -d mysql/mysql-server

docker logs rm-mysql 2>&1 | grep GENERATED
#You should see something like: 
#[Entrypoint] GENERATED ROOT PASSWORD: 6IbLUwAw0B3RaKXEG[aBW0MvONz
# Use that password to log in using command:

docker exec -it rm-mysql mysql -uroot -p

# and execute sql commands:

ALTER USER 'root'@'localhost' IDENTIFIED BY 'Tester!123';
CREATE USER 'recruitme'@'%' IDENTIFIED BY 'Tester!123';
GRANT ALL PRIVILEGES ON *.* TO 'recruitme'@'%';
quit

#
# setup API & WebApp
#

docker build -t rm-api-image . 
docker run -d --net=recruit-me-network --name rm-api -p 80:80 -p 443:443 --expose=80 --expose=443 rm-api-image


#
# Other Important commands:
#

#To stop container running in the background:
docker container stop ContainerName/ContainerID

#To remove all image or containers currently not in use:
docker container/image prune

#To look up container logs
docker container logs ContainerName/ContainerID