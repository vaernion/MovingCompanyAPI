# Moving Company API [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI)

## Introduction

Simple API for managing orders for the Moving Company. It consists of a ASP.NET Core web app running on an Azure app service instance.

## Prerequisites

This project uses .NET 6.0 and Terraform. Terraform Cloud is used by default as the remote state backend. GitHub workflows are included for CI/CD.

SonarCloud is used for code quality checks. A workspace has to be connected to the GitHub repo.

## Setup

Add the following variables to GitHub repo settings -> secrets.

```sh
SONAR_TOKEN # SonarCloud user token
TF_API_TOKEN # Terraform Cloud API token
```

Configure cloud block arguments in `MovingCompanyAPI-Infrastructure/main.tf`.

## Build and Test

```sh
dotnet restore
dotnet build --configuration Release --no-restore
dotnet test --no-restore
```

```sh
cd MovingCompanyAPI-Infrastructure
terraform init
terraform plan
terraform apply
```

## API v1

| Method | Endpoint    | Description           | Request body | Response  body  |
|--------|-------------|-----------------------|--------------|-----------------|
| GET    | /orders     | Get all orders.       | None         | Array of orders |
| GET    | /orders/:id | Get a specific order. | None         | Order           |
| POST   | /orders     | Add a new order.      | Order        | Order           |
| PUT    | /orders/:id | Edit an order.        | Order        | None            |
| DELETE | /orders/:id | Delete an order.      | None         | None            |
