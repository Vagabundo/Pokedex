# ecs.tf | Elastic Container Service

resource "aws_ecs_cluster" "aws-ecs-cluster" {
  name = "${var.app_name}-${var.app_environment}-cluster"
  tags = {
    Name        = "${var.app_name}-ecs"
    Environment = var.app_environment
  }
}

resource "aws_cloudwatch_log_group" "log-group" {
  name = "${var.app_name}-${var.app_environment}-logs"

  tags = {
    Application = var.app_name
    Environment = var.app_environment
  }
}

# resource "aws_ecs_task_definition" "aws-ecs-task" {
#   family = "${var.app_name}-task"

#   container_definitions = <<DEFINITION
#   [
#     {
#       "name": "${var.app_name}-${var.app_environment}-container",
#       "image": "${aws_ecr_repository.aws-ecr.repository_url}:latest",
#       "entryPoint": [],
#       "essential": true,
#       "portMappings": [
#         {
#           "containerPort": 80,
#           "hostPort": 80
#         }
#       ],
#       "memory": 512,
#       "cpu": 256
#     }
#   ]
#   DEFINITION
#   requires_compatibilities = ["FARGATE"] # Stating that we are using ECS Fargate
#   network_mode             = "awsvpc"    # Using awsvpc as our network mode as this is required for Fargate
#   memory                   = 512         # Specifying the memory our container requires
#   cpu                      = 256         # Specifying the CPU our container requires
#   execution_role_arn       = "${aws_iam_role.ecsTaskExecutionRole.arn}"
# }

resource "aws_ecs_task_definition" "aws-ecs-task" {
  family = "${var.app_name}-task"

  container_definitions = <<DEFINITION
  [
    {
      "name": "${var.app_name}-${var.app_environment}-container",
      "image": "${aws_ecr_repository.aws-ecr.repository_url}:latest",
      "entryPoint": [],
      "essential": true,
      "logConfiguration": {
        "logDriver": "awslogs",
        "options": {
          "awslogs-group": "${aws_cloudwatch_log_group.log-group.id}",
          "awslogs-region": "${var.aws_region}",
          "awslogs-stream-prefix": "${var.app_name}-${var.app_environment}"
        }
      },
      "portMappings": [
        {
          "containerPort": 80,
          "hostPort": 80
        }
      ],
      "cpu": 256,
      "memory": 512,
      "networkMode": "awsvpc"
    }
  ]
  DEFINITION

  requires_compatibilities = ["FARGATE"]
  network_mode             = "awsvpc"
  memory                   = "512"
  cpu                      = "256"
  execution_role_arn       = aws_iam_role.ecsTaskExecutionRole.arn
  task_role_arn            = aws_iam_role.ecsTaskExecutionRole.arn

  tags = {
    Name        = "${var.app_name}-ecs-td"
    Environment = var.app_environment
  }
}

data "aws_ecs_task_definition" "main" {
  task_definition = aws_ecs_task_definition.aws-ecs-task.family
}

# resource "aws_ecs_service" "aws-ecs-service" {
#   name            = "${var.app_name}-${var.app_environment}-ecs-service"
#   cluster         = "${aws_ecs_cluster.aws-ecs-cluster.id}"
#   task_definition = "${aws_ecs_task_definition.aws-ecs-task.arn}"
#   launch_type     = "FARGATE"
#   desired_count   = 1 # Setting the number of containers we want deployed to 1

#   network_configuration {
#     subnets          = ["${aws_default_subnet.default_subnet_a.id}", "${aws_default_subnet.default_subnet_b.id}", "${aws_default_subnet.default_subnet_c.id}"]
#     assign_public_ip = true # Providing our containers with public IPs
#     security_groups  = ["${aws_security_group.service_security_group.id}"] # Setting the security group
#   }

#   load_balancer {
#     target_group_arn = "${aws_lb_target_group.target_group.arn}" # Referencing our target group
#     container_name   = "${var.app_name}-${var.app_environment}-container"
#     container_port   = 80 # Specifying the container port
#   }
# }

resource "aws_ecs_service" "aws-ecs-service" {
  name                 = "${var.app_name}-${var.app_environment}-ecs-service"
  cluster              = aws_ecs_cluster.aws-ecs-cluster.id
  task_definition      = "${aws_ecs_task_definition.aws-ecs-task.family}:${max(aws_ecs_task_definition.aws-ecs-task.revision, data.aws_ecs_task_definition.main.revision)}"
  launch_type          = "FARGATE"
  scheduling_strategy  = "REPLICA"
  desired_count        = 2
  force_new_deployment = true

  network_configuration {
    subnets          = aws_subnet.private.*.id
 #   assign_public_ip = true
    security_groups = [
      aws_security_group.service_security_group.id
      #aws_security_group.load_balancer_security_group.id
    ]
  }

  load_balancer {
    target_group_arn = aws_lb_target_group.target_group.arn
    container_name   = "${var.app_name}-${var.app_environment}-container"
    container_port   = 80
  }

  depends_on = [aws_lb_listener.listener]
}

# resource "aws_security_group" "service_security_group" {
#   ingress {
#     from_port = 0
#     to_port   = 0
#     protocol  = "-1"
#     # Only allowing traffic in from the load balancer security group
#     security_groups = ["${aws_security_group.load_balancer_security_group.id}"]
#   }

#   egress {
#     from_port   = 0 # Allowing any incoming port
#     to_port     = 0 # Allowing any outgoing port
#     protocol    = "-1" # Allowing any outgoing protocol 
#     cidr_blocks = ["0.0.0.0/0"] # Allowing traffic out to all IP addresses
#   }
# }

resource "aws_security_group" "service_security_group" {
  vpc_id = aws_vpc.aws-vpc.id

  ingress {
    from_port       = 0
    to_port         = 0
    protocol        = "-1"
    security_groups = [aws_security_group.load_balancer_security_group.id]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  tags = {
    Name        = "${var.app_name}-service-sg"
    Environment = var.app_environment
  }
}
