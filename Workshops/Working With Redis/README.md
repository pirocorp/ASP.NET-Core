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

## Common Redis commands

The commands are atomic in a multithread environment.

Working with primitive values:

```redis
SET my:key value      // set a text value by key
GET my:key            // get the value by key
EXISTS my:key         // check whether a key exists
DEL my:key            // delete a key
SET cats 10           // set an integer value by key
INCR cats             // atomically increment a value by 1
INCRBY cats 10        // atomically increment a value by 10
EXPIRE cats 120       // set an expiration time in seconds
TTL cats              // check the expiration time
PERSIST cats          // remove an expiration time
```

## Complex data structures Commands

List – contains series of values

```
RPUSH my:cats Sharo      // add values at the end of the list
LPUSH my:cats Lady       // add values at the beginning of the list
LRANGE my:cats 0 -1      // return subset of the list
LPOP my:cats             // remove a value from the beginning 
RPOP my:cats             // remove a value from the end
LLEN my:cats             // get the length of the list
```

Set – contains unique elements without any order

```
SADD my:cars BMW           // add values to the set
SADD my:cars Mercedes      // add more values to the set
SREM my:cars Audi          // remove a value from the set
SISMEMBER my:cars Opel     // check if a value is in the set
SMEMBERS my:cars           // return all values in the set
SUNION my:cars your:cars   // combines two sets
```

Sorted Set – contains unique elements with sorted scores

```
ZADD expensive:cars 100000 BMW       // add a value with a score
ZADD expensive:cars 80000 Audi       // add a value with a score
ZADD expensive:cars 120000 Mercedes  // add a value with a score
ZRANGE expensive:cars 0 1            // return sorted values
```

Hashes – maps between field, useful for objects

```
HSET user:1234 name Ivaylo      // set object name
HSET user:1234 age 5            // set object age
HGET user:1234 name             // get object name
HGETALL user:1234               // get all object values
HINCRBY user:1234 age 1         // increment an integer
HDEL user:1234 age              // delete an object property
```

## Designing a database

You need to design what kind of keys you are going to use

For example, storing users:

```redis
INCR next_user_id => 1000                  
HMSET user:1000 username antirez password somepasshash
HSET users antirez 1000
```

[Redis database example](https://redis.io/topics/twitter-clone)

## REDIS Configuration

[Redis v6.0 configuration file example](https://raw.githubusercontent.com/redis/redis/6.0/redis.conf)
Redis configuration for WSL (Ubuntu 20.04) is lacated at ```/etc/redis/redis.conf```

Password can be set in config file at ```# masterauth <master-password>```
Connecting with password to redis-server ```redis-cli -h localhost -a YOURPASSWORD```

