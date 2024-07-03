# food-tracker-website

## Description

tracks food places

temporarily hosted on

[testing.illuminat3-projects.com](https://testing.illuminat3-projects.com/)

## Setup

You may need to setup a database if you havent already  
To do this use the following commands

```bat
dotnet tool install --global dotnet-ef
```

If there are no migrations you will need to run

``` bat
dotnet ef migrations add InitialCreate
```

and

``` bat
dotnet ef database update
```

## Docker Instructions

Build

``` bat 
docker build -t justilluminate/food-tracker-app:latest .
```

Push 

``` bat 
docker push justilluminate/food-tracker-app:latest
```

This will also happen when a pr is merged
