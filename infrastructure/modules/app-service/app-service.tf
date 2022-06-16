locals {
  # don't use prefix in prod for cleaner URL
  app_env_prefix = var.environment == "prod" ? "" : "${var.environment}-"
}

resource "azurerm_app_service_plan" "asp" {
  name                = "${var.environment}-${var.namespace}-asp-${var.suffix}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name

  sku {
    tier = var.app_service_sku_tier
    size = var.app_service_sku_size
  }
}

resource "azurerm_app_service" "app" {
  name                = "${local.app_env_prefix}${var.namespace}-webapp-${var.suffix}"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  app_service_plan_id = azurerm_app_service_plan.asp.id

  source_control {
    repo_url           = var.app_repo_url
    branch             = var.app_repo_branch
    manual_integration = true
    use_mercurial      = false
  }
}
