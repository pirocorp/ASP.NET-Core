## [Install and Test Redis](https://docs.microsoft.com/en-us/windows/wsl/tutorials/wsl-database#install-redis)

To install Redis on WSL (Ubuntu 20.04):
1. Open your WSL terminal (ie. Ubuntu 20.04)
2. Update your Ubuntu packages: ```sudo apt update```
3. Once the packages have updated, install Redis with: ```sudo apt install redis-server```
4. Confirm installation and get the version number: ```redis-server --version```

To start running your Redis server: ```sudo service redis-server start```

Check to see if redis is working (redis-cli is the command line interface utility to talk with Redis): ```redis-cli ping``` this should return a reply of "PONG".

To stop running your Redis server: ```sudo service redis-server stop```

To restart your Redis server: ```sudo service redis-server restart```

For more information about working with a Redis database, see the [Redis docs](https://redis.io/topics/quickstart)
