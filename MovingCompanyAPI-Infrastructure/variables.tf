variable "namespace" {
  type        = string
  description = "Namespace used for naming various resources. Should be complex enough to be globally unique."
  default     = "movingapi22"
}

variable "location" {
  type        = string
  description = "Azure region resources are created in. Resource groups use this value, and resources inherit hierarchically."
  default     = "West Europe"
}

variable "environment" {
  type        = string
  description = "Name of the environment"
  default     = "dev"
}
