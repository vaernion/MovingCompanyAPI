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
}

provider "azurerm" {
  features {
  }
}

provider "azuread" {
  tenant_id = data.azurerm_client_config.current.tenant_id
}

resource "azurerm_resource_group" "rg" {
  name     = "${var.environment}-${var.namespace}-rg-appservice-${var.suffix}"
  location = var.location

  tags = {
    environment = var.environment
  }
}
