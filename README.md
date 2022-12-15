# EagleRock

EagleRock is a simple dockerized .NET Core 6 application utilizing:
 - Distributed Redis Cache - to store all saved records (records available in cache for 24 hours). Cache record expiration time is a subject to change if required.
 - EF 7 database storage. Once the record is saved to the cache, it also getting saved to the DB. All processes are commited asynchronously 
 - xunit testing framework implements the integration testing. It extends the WebApplicationFactory to setup a fake data source
 
![image](https://user-images.githubusercontent.com/50306019/207755123-79d044fe-7345-4b8a-b689-42dfed96de4b.png)

### The API consists of two points:

 - GetAll API point retrieves all cached records from active EagleBots. The call can be extended to get data from the cache + database 
 - Post - revieves, validates and saves the traffic records from EagleBots

![image](https://user-images.githubusercontent.com/50306019/207754103-8d50af80-0517-4cea-bbf2-fa93b41e193a.png)


### Proposed improvements:
 - Global Error handler Example: https://code-maze.com/global-error-handling-aspnetcore/
 - Use Serilog structured logging system https://serilog.net/
 - Use Protobuf serializer/deserializer and use gRPC for data streaming Example: https://github.com/protobuf-net/protobuf-net https://en.wikipedia.org/wiki/GRPC
 - Submit all received traffic into RabbitMQ message broker. Example: https://www.rabbitmq.com/tutorials/tutorial-one-dotnet.html
