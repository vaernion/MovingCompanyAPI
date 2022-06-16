terraform {
  required_version = ">= 1.1.0"

  cloud {
    organization = "vaernion"
    hostname     = "app.terraform.io"

    workspaces {
      name = "moving-api-prod"
    }
  }
}

module "moving" {
  source          = "../../modules/app-service"
  environment     = "prod"
  namespace       = var.namespace
  suffix          = "moving"
  location        = var.location
  app_repo_url    = "https://github.com/vaernion/MovingCompanyAPI"
  app_repo_branch = "main"
}
