### MESSAGING
#### Sync Messaging
- Request / Response Cycle
- Requester will "wait" for response
- Externally facing services usually sync (e.g http requests)
- Services usually need to "know" about each other
- 2 forms of Request
- - Http
- - Grpc


#### Note Sync Http C#
- From a messaging perspective this method is still sync
- The client still has to wait for response
- Async in this context (the C# lang) means that the **action will not wait** for long running operations
- It will hand back it's thread to the thread pool, where it can be reused
- When the operation finishes it will re-acquire a thread and complete, (and respond back to the requester)
- So Async here is about thread exhaustion - the requestor still has to wait (the call is sync)

```c#
[HttpPost]
public async ActionResult TestInboundConnection() 
{
  Console.WriteLine("--> Inbound POST # Command Service");

  return Ok("Inbound test of from Platforms Controller");
}
```
#### Sync Messing b/w Services
- Can and does occur - we will implement, however...
- It does tend to pair services, (couple them), creating a dependency
- Could lead to long dependency chains
- srv1 -> srv2 -> src3 -> srvx

#### Async Messaging
- No Request / Response Cycle
- Requester does not wait
- Event Model, e.g. publish - subscribe
- Typically used between services
- Event bus is often used (we'll be using RabbitMQ)
- services don't need to know about each other, just the bus\
- Introduces its own range of complexities - not a magic bullet

#### MESSAGE BUS
- Event bus a Monolisth to some extent Yes
- Internal comms would cease if the message bus goes down
- Services will still operate and work externally
- Should be treated as a first class citizen, similar to: 
- - Network, Physical Storage, Power etc

- Message bus should be clustered, with message persistence etc.
- Services should implement some kind of retry policy
- Aim for Smart Services, stupid pipes.

