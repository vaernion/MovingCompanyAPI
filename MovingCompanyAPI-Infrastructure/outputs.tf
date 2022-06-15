output "webapp_url" {
  value = data.azurerm_app_service.moving.default_site_hostname
}
