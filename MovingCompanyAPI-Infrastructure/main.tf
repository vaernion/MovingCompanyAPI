terraform {
  required_version = ">= 1.1.0"

  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "= 3.10.0"
    }
    azuread = {
      source  = "hashicorp/azuread"
      version = "= 2.23.0"
    }
  }

  cloud {
    organization = "vaernion"
    hostname     = "app.terraform.io"

    workspaces {
      name = "moving-company"
    }
  }
}

provider "azurerm" {
  features {
  }
}

provider "azuread" {
  tenant_id = data.azurerm_client_config.current.tenant_id
}

resource "azurerm_resource_group" "moving" {
  name     = "${var.environment}-${var.namespace}-rg-moving"
  location = var.location

  tags = {
    environment = var.environment
  }
}
