# Moving Company API [![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Lines of Code](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=ncloc)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=vaernion_MovingCompanyAPI&metric=reliability_rating)](https://sonarcloud.io/summary/new_code?id=vaernion_MovingCompanyAPI)

## Description

Example REST API for managing orders for a Moving Company. It consists of a ASP.NET Core web app running on an Azure app service instance.

Terraform Cloud is used as the remote state backend. GitHub workflows are included for CI/CD.

SonarCloud is used for code quality checks.

## Prerequisites

This project uses .NET 6.0 and Terraform. Infrastructure is provisioned in Azure.

The [setup](#setup) guide assumes that the code is stored in GitHub, and that Terraform Cloud and SonarCloud accounts have been created.

## Setup

To use SonarCloud, a SonarCloud workspace has to be connected to the GitHub repo.

Add the following variables to GitHub repo -> settings -> secrets.

```sh
TF_API_TOKEN # Terraform Cloud API token
SONAR_TOKEN # SonarCloud user token
```

To authenticate Terraform Cloud to your Azure subscription, create a Service Principal:

```ps1
az ad sp create-for-rbac --role="Contributor" --scopes="/subscriptions/SUBSCRIPTION_ID"
```

In Terraform Cloud, create workspaces for dev/staging/prod, with a CLI-driven workflow.

```sh
moving-api-dev
moving-api-staging
moving-api-prod
```

In each workspace, go to Settings -> General and set Terraform Working Directory:

```sh
# workspace: moving-api-dev
infrastructure/environments/dev
# save settings
# repeat for staging and prod
```

In your Terraform Cloud organization, create a variable set for these workspaces, and add environment variables from the newly created Service Principal:

```sh
# variable set
# make sure to create environment variables and not Terraform variables
ARM_TENANT_ID
ARM_SUBSCRIPTION_ID
ARM_CLIENT_SECRET
ARM_CLIENT_ID # appId
```

Finally configure organization name in the cloud block in `main.tf` in each of `./infrastructure/environments/[dev/prod/staging]`.

```sh
terraform {
# ...
  cloud {
    organization = "your-org"
    # ...
  }
}
```

## Creating infrastructure in dev environment

```sh
cd infrastructure/environments/dev
terraform init
terraform apply
```

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

## CI/CD flow

Pull Requests updates the staging environment if tests pass, and the PR is commented with the `terraform plan` results. Once merged with main, the production environment is updated as long as the tests pass for prod.

## API v1

| Method | Endpoint    | Description           | Request body | Response  body  |
|--------|-------------|-----------------------|--------------|-----------------|
| GET    | /orders     | Get all orders.       | None         | Array of orders |
| GET    | /orders/:id | Get a specific order. | None         | Order           |
| POST   | /orders     | Add a new order.      | Order        | Order           |
| PUT    | /orders/:id | Edit an order.        | Order        | None            |
| DELETE | /orders/:id | Delete an order.      | None         | None            |
