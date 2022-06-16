terraform {
  required_version = ">= 1.1.0"

  cloud {
    organization = "vaernion"
    hostname     = "app.terraform.io"
  }
}

module "moving" {
  source          = "./modules/app-service"
  environment     = split("-", terraform.workspace)[2] # extract env from "moving-api-env"
  namespace       = var.namespace
  suffix          = "moving"
  location        = var.location
  app_repo_url    = "https://github.com/vaernion/MovingCompanyAPI"
  app_repo_branch = "main"
}
