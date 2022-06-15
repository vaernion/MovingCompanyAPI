# resource "azurerm_service_plan" "moving" {
#   name                = "${var.environment}-${var.namespace}-asp-moving"
#   resource_group_name = azurerm_resource_group.moving.name
#   location            = azurerm_resource_group.moving.location
#   os_type             = "Windows"
#   sku_name            = "F1"
# }

# resource "azurerm_windows_web_app" "moving" {
#   name                = "${var.environment}-${var.namespace}-webapp-moving"
#   resource_group_name = azurerm_resource_group.moving.name
#   location            = azurerm_service_plan.moving.location
#   service_plan_id     = azurerm_service_plan.moving.id

#   site_config {
#     application_stack {
#       current_stack  = "dotnet"
#       dotnet_version = "6.0"
#     }

#   }
# }

# Create the Linux App Service Plan
resource "azurerm_app_service_plan" "moving" {
  name                = "${var.environment}-${var.namespace}-asp-moving"
  location            = azurerm_resource_group.moving.location
  resource_group_name = azurerm_resource_group.moving.name
  sku {
    tier = "Free"
    size = "F1"
  }
}
# Create the web app, pass in the App Service Plan ID, and deploy code from a public GitHub repo
resource "azurerm_app_service" "moving" {
  name                = "${var.environment}-${var.namespace}-webapp-moving"
  location            = azurerm_resource_group.moving.location
  resource_group_name = azurerm_resource_group.moving.name
  app_service_plan_id = azurerm_app_service_plan.moving.id
  source_control {
    repo_url           = "https://github.com/vaernion/MovingCompanyAPI"
    branch             = "main"
    manual_integration = true
    use_mercurial      = false
  }
}
