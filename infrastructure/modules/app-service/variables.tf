variable "environment" {
  type        = string
  description = "Name of the environment"
}

variable "namespace" {
  type        = string
  description = "Namespace used for naming various resources. Should be complex enough to be globally unique."
}

variable "suffix" {
  type        = string
  description = "Resource name suffix to uniquely identify this module instance."
}

variable "location" {
  type        = string
  description = "Azure region resources are created in. Resource groups use this value, and resources inherit hierarchically."
  default     = "West Europe"
}

variable "app_service_sku_tier" {
  type        = string
  description = "SKU tier for the app service plan"
  default     = "Free"
}

variable "app_service_sku_size" {
  type        = string
  description = "SKU size for the app service plan"
  default     = "F1"
}

variable "app_repo_url" {
  type        = string
  description = "URL to the source code repo"
}

variable "app_repo_branch" {
  type        = string
  description = "Git branch to use"
  default     = "main"
}
