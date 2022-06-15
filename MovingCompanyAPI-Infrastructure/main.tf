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
}

provider "azuread" {
  tenant_id = data.azurerm_client_config.current.tenant_id
}
