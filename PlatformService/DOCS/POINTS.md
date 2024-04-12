
## What we'll Cover 
### INTRO & THEORY
1. Tooling (VS Code) .Net5 Docker Destop, Docker Hub, Postman
2. Solution Architecture
3. Service Architecture

### STARTING THE PLATFORM SERVICE
1. Scaffolding 
2. Data Layer
3. Controller & Actions

### DOCKER & KUBERNETES
1. Review of Docker
2. Containerize Platform Service
3. Pushing to Docker Hub
4. Intro to Kubernetes
5. Kubernetes Architecture
6. External Network Access

### STARTING THE COMMANDS SERVICE
1. Scaffolding 
2. Controlelr & Action Synchronous & Ascynchronous Messaging
3. Adding a HTTP Client
4. Deploy Service to Kubernetes
5. Internal Networking
6. API Gateway

### SQL SERVER
1. Persistent Volume Claims
2. Kubernetes Secrets
3. Deploy SQL Server to Kubernetes
4. Revisit Platform Service

### MULTI-RESOURCE API
1. Review of Endponits for Commands Service
2. Data Layer
3. Controller & Actions

### MESSAGE BUS / RABBITMQ
1. Solution Archetecture
2. RabbitMQ Overview
3. Deploy RabbitMQ to Kubernetes
4. Test

### ASYNCHRONOUS MESSAGING
1. Adding a Message Bus Publisher to Platform Service
2. Event Processing
3. Adding an Event Listener to the Commands Service

### GRPC
1. Overview of GRPC
2. Final Kubernetes
3. Adding gRPC Server to Platform Service
4. Creating a "proto" file 
5. Adding a gRPC Client to the Commands Service
6. Deploy & Test

### SOLUTION ARCH / EVENT ARCH
- Event Arch is used in Microservices
- Solution Arch for Microservice is Event Driven Arch
### DATA DRIVEN ARCH / EVENT DRIVEN ARCH
- A critical difference between the two is that in a data-driven architecture, the application state is determined by the data, and events are just a side effect of changes in data. In contrast, an event-driven architecture is more reactive, with the state being determined by the events.

### APPLICATION VIRTUALIZATION
- Application virtualization allows apps to comply with strict governance and privacy regulations such as the Health Insurance Portability and Accountability Act (HIPAA) and Payment Card Industry Data Security Standards (PCI DSS). This helps keep private information safe from any malware or attempts at compromise.

### PCI DSS
1. Install and maintain a firewall configuration to protect cardholder data
2. Do not use vendor-supplied defaults for system passwords and other security parameters
3. Protect stored cardholder data
4. Encrypt transmission of cardholder data across open, public networks
5. Use and regularly update anti-virus software or programs
6. Develop and maintain secure systems and applications
7. Restrict access to cardholder data by business need to know
8. Assign a unique ID to each person with computer access
9. Restrict physical access to cardholder data
Track and monitor all access to network resources and cardholder data
10. Regularly test security systems and processes
11. Maintain a policy that addresses information security for all personnel