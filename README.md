# Repository Abstracts

This repository demonstrates different ways of providing an abstraction to data storage in C#.

## RepositoryAbstracts.Common

Contains all the common code to run the samples

## AbstractBaseSample

Uses an abstract base to provide base repository implementation without "knowing" how it's implemented at the business layer. Can be used when it is unlikely the underlying storage technology will change. Keeps separation of concern in tact but is not dependency injection friendly. To switch, you'd have to inherit from the different class. Also forces you to pass configuration through the inheritance chain.

## GatewayRepository

Uses a Gateway pattern to gain access to the business logic/layer. In this case we are using an `IRepository` abstract to provide a swappable backend with minimal buy-in. That is, we can change the "injected" database access based on the specifics of the request (in our case a tenant partitioning scheme) without altering the business layer itself.