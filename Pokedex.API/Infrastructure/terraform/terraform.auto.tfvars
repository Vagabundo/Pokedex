aws_region     = "eu-west-1"
aws_access_key = "aws_access_key"
aws_secret_key = "aws_secret_key"

# these are zones and subnets
availability_zones = ["eu-west-1a", "eu-west-1b"]
public_subnets     = ["10.10.100.0/24", "10.10.101.0/24"]
private_subnets    = ["10.10.0.0/24", "10.10.1.0/24"]

# these are used for tags
app_name        = "pokedex-api"
app_environment = "playground"
