# alb.tf | Load Balancer Configuration

# resource "aws_alb" "application_load_balancer" {
#   name               = "${var.app_name}-${var.app_environment}-alb"
#   load_balancer_type = "application"
#   subnets = [ # Referencing the default subnets
#     "${aws_default_subnet.default_subnet_a.id}",
#     "${aws_default_subnet.default_subnet_b.id}",
#     "${aws_default_subnet.default_subnet_c.id}"
#   ]
#   # Referencing the security group
#   security_groups = ["${aws_security_group.load_balancer_security_group.id}"]
# }

resource "aws_alb" "application_load_balancer" {
  name               = "${var.app_name}-${var.app_environment}-alb"
  internal           = false
  load_balancer_type = "application"
  subnets            = aws_subnet.public.*.id
  security_groups    = [aws_security_group.load_balancer_security_group.id]

  tags = {
    Name        = "${var.app_name}-alb"
    Environment = var.app_environment
  }
}

# # Creating a security group for the load balancer:
# resource "aws_security_group" "load_balancer_security_group" {
#   ingress {
#     from_port   = 80 # Allowing traffic in from port 80
#     to_port     = 80
#     protocol    = "tcp"
#     cidr_blocks = ["0.0.0.0/0"] # Allowing traffic in from all sources
#   }

#   egress {
#     from_port   = 0 # Allowing any incoming port
#     to_port     = 0 # Allowing any outgoing port
#     protocol    = "-1" # Allowing any outgoing protocol 
#     cidr_blocks = ["0.0.0.0/0"] # Allowing traffic out to all IP addresses
#   }
# }

resource "aws_security_group" "load_balancer_security_group" {
  vpc_id = aws_vpc.aws-vpc.id

  ingress {
    from_port        = 80
    to_port          = 80
    protocol         = "tcp"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }

  egress {
    from_port        = 0
    to_port          = 0
    protocol         = "-1"
    cidr_blocks      = ["0.0.0.0/0"]
    ipv6_cidr_blocks = ["::/0"]
  }
  tags = {
    Name        = "${var.app_name}-sg"
    Environment = var.app_environment
  }
}

# resource "aws_lb_target_group" "target_group" {
#   name        = "${var.app_name}-${var.app_environment}-tg"
#   port        = 80
#   protocol    = "HTTP"
#   target_type = "ip"
#   vpc_id      = "${aws_default_vpc.default_vpc.id}" # Referencing the default VPC
#   health_check {
#     matcher = "200,301,302"
#     path = "/health"
#   }
# }

resource "aws_lb_target_group" "target_group" {
  name        = "${var.app_name}-${var.app_environment}-tg"
  port        = 80
  protocol    = "HTTP"
  target_type = "ip"
  vpc_id      = aws_vpc.aws-vpc.id

  health_check {
    healthy_threshold   = "3"
    interval            = "300"
    protocol            = "HTTP"
    matcher             = "200"
    timeout             = "3"
    path                = "/health"
    unhealthy_threshold = "2"
  }

  tags = {
    Name        = "${var.app_name}-lb-tg"
    Environment = var.app_environment
  }
}

# resource "aws_lb_listener" "listener" {
#   load_balancer_arn = "${aws_alb.application_load_balancer.arn}" # Referencing our load balancer
#   port              = "80"
#   protocol          = "HTTP"
#   default_action {
#     type             = "forward"
#     target_group_arn = "${aws_lb_target_group.target_group.arn}" # Referencing our tagrte group
#   }
# }

resource "aws_lb_listener" "listener" {
  load_balancer_arn = aws_alb.application_load_balancer.id
  port              = "80"
  protocol          = "HTTP"

  default_action {
    type             = "forward"
    target_group_arn = aws_lb_target_group.target_group.id
  }
}
