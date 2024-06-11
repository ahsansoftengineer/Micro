### K8s

### Big Picture

#### Cluster
- A Kubernetes (K8s) cluster is a group of computing nodes, or worker machines, that run containerized applications. Containerization is a software deployment and runtime process that bundles an application's code with all the files and libraries it needs to run on any infrastructure.

#### Node in Cluster
- A Node is a worker machine in Kubernetes and may be either a virtual or a physical machine, depending on the cluster. Each Node is managed by the control plane. A Node can have multiple pods, and the Kubernetes control plane automatically handles scheduling the pods across the Nodes in the cluster.

#### Pods in Node
- A pod is the smallest execution unit in Kubernetes. A pod encapsulates one or more applications. Pods are ephemeral by nature, if a pod (or the node it executes on) fails, Kubernetes can automatically create a new replica of that pod to continue operations.

#### Containers in Pods
- Kubernetes containers resemble virtual machines (VMs), each with its own CPU share, filesystem, process space, memory, and more. However, Kubernetes containers are considered lightweight because: they can share the Operating System (OS) among applications due to their relaxed isolation properties.

#### App in Containers
- Node, Dotnet, Web all are the types of Application


Kubernetes is an open-source container orchestration platform that automates the deployment, scaling, and management of containerized applications. Here are its key points:

1. **Container Orchestration**: Kubernetes manages containers, allowing applications to be packaged into containers for easy deployment and scaling.

2. **Automation**: It automates many tasks involved in deploying, managing, and scaling applications, reducing manual intervention.

3. **Scalability**: Kubernetes enables applications to scale seamlessly by adding or removing containers based on demand.

4. **High Availability**: It ensures that applications are highly available by automatically restarting containers or redistributing workloads in case of failures.

5. **Resource Management**: Kubernetes efficiently allocates computing resources such as CPU and memory to applications, optimizing performance and utilization.

6. **Service Discovery and Load Balancing**: It provides mechanisms for service discovery and load balancing, ensuring that traffic is routed to the appropriate containers.

7. **Storage Orchestration**: Kubernetes manages storage volumes and ensures that data persists even if containers fail.

8. **Rolling Updates and Rollbacks**: It supports rolling updates, allowing new versions of applications to be deployed gradually while ensuring that the application remains available. Rollbacks are also supported in case of issues.

9. **Declarative Configuration**: Kubernetes allows users to define the desired state of their applications using declarative configuration files, which Kubernetes then works to achieve and maintain.

10. **Community Support**: It has a large and active community contributing to its development and providing support, making it a robust and evolving platform for container orchestration.

In summary, Kubernetes provides a comprehensive solution for deploying, managing, and scaling containerized applications, offering automation, scalability, high availability, and a rich set of features for modern application development and deployment.

### COMPONENTS

Kubernetes comprises several key components that work together to provide container orchestration capabilities. These components include:

1. **Master Components**:
   - **API Server**: Acts as the front-end for Kubernetes, accepting and processing RESTful API requests for cluster management.
   - **Controller Manager**: Enforces cluster state and manages various controllers for maintaining the desired state of resources.
   - **Scheduler**: Assigns nodes to newly created pods based on resource requirements and other constraints.

2. **Node Components**:
   - **Kubelet**: Acts as an agent on each node, responsible for managing pods, their containers, and reporting back to the control plane.
   - **Kube-proxy**: Manages network communication for pods within the cluster and performs load balancing across them.
   - **Container Runtime**: The software responsible for running containers, such as Docker, containerd, or CRI-O.

3. **etcd**: Consistent and highly available key-value store used as Kubernetes' backing store for all cluster data.

4. **Networking**:
   - **Pod Network**: A network overlay that enables communication between pods across nodes in the cluster.
   - **Service Network**: Facilitates communication between services (logical groups of pods) and ensures service discovery.

5. **Add-ons**:
   - **DNS**: Provides DNS-based service discovery within the cluster.
   - **Dashboard**: Web-based UI for managing and monitoring the cluster.
   - **Ingress Controller**: Manages external access to services within the cluster.
   - **Metrics Server**: Aggregates resource usage data from the cluster for monitoring and autoscaling purposes.
   - **Cluster Autoscaler**: Automatically adjusts the size of the cluster based on resource utilization.

These components work together to provide a robust platform for deploying, managing, and scaling containerized applications in Kubernetes. Each component plays a specific role in maintaining the desired state of the cluster and ensuring that applications run smoothly.