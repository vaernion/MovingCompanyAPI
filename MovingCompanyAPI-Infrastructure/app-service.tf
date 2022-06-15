resource "azurerm_app_service_plan" "moving" {
  name                = "${var.environment}-${var.namespace}-asp-moving"
  location            = azurerm_resource_group.moving.location
  resource_group_name = azurerm_resource_group.moving.name

  sku {
    tier = var.app_service_sku_tier
    size = var.app_service_sku_size
  }
}

resource "azurerm_app_service" "moving" {
  name                = "${var.environment}-${var.namespace}-webapp-moving"
  location            = azurerm_resource_group.moving.location
  resource_group_name = azurerm_resource_group.moving.name
  app_service_plan_id = azurerm_app_service_plan.moving.id

  source_control {
    repo_url           = var.app_repo_url
    branch             = var.app_repo_branch
    manual_integration = true
    use_mercurial      = false
  }
}
