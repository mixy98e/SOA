SolarRadiationIOT

![alt text](https://github.com/mixy98e/SOA/main/SolarRadiationIOT/Architecture.jpg?raw=true)

Project:
.Net Core Microservices using Docker and Docker-Compose

---

build 

>docker-compose up --build
>command result: 
	-DataService
	-SensorService
	-MongoDB
	-AnalystService
	-CommandService
	-OceloteGateway
	-RabbitMQ

DataService API:
----------------
	- GET /SensorData/all (returns all entries from database)
	- GET /SensorData/last (return last entry from database)
	- POST /SensorData (adds entry in database)
		Body:
		{
  			"id": {},
 			"unixTime": 0,
  			"radiation": 0,
  			"temperature": 0,
  			"pressure": 0,
  			"humidity": 0,
  			"windDirection": 0,
  			"speed": 0,
  			"timeSunRise": "string",
  			"timeSunSet": "string"
		}

	Model: SensorData
	     {
		id	ObjectId{...}
		unixTime	integer($int32)
		radiation	number($float)
		temperature	number($float)
		pressure	number($float)
		humidity	number($float)
		windDirection	number($float)
		speed	number($float)
		timeSunRise	string
		nullable: true
		timeSunSet	string
		nullable: true
	     }

SensorService API:
------------------
	- GET /Sensor/start (starts sensor)
	- GET /Sensor/stop (stops sensor)
	- GET /Sensor/metadata (returns all parameters of sensor)
		Default sensor parameters
		{
 	 		"interval": 5000,
  			"threshold": 0.01,
  			"dataSource": "./DataSource/SolarPredictionTestShort.csv"
		}
	- PUT /Sensor/interval (changes sensor read interval)
	- PUT /Sensor/threshold (changes sensor threshold parameter)


Testing:

1. build docker compose using command >docker-compose up --build
2. send get request GET /Sensor/start to SensorService
3. read all data from DataService GET /SensorData/all

SensorService will send data every 5 seconds periodically to the DataService using POST /SensorData request.
