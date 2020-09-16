# Rubens (ruːbənz)
## 📮 Basic pub-sub functionality on top of SignalR  
#### Other communication methods possibly coming in the future (gRPC, AMQP)
![](https://i.imgur.com/ZRPMZau.png)
### 🧙🏽‍♂️ With Microsoft's DI
```
app.UseRubens(x =>
{
    x.Subscribe<Event>(@event =>
    {
        Console.WriteLine($">{@event}");
        Console.WriteLine($"<{@event}");
    });
    //With IEventHandler
    x.Subscribe<Event,EventHandler>();
});
```
### ⚙️ With Manual Wiring
```
var cfg = new RubensConfiguration{ConnectionString = "https://localhost:5001"};
var ctl = new ControlPlane(cfg);
var bus = new Bus(ctl);
await bus.Subscribe<Event>(@event => Console.WriteLine(@event.Body));
```
### 🥳 Publishing events
```
await _bus.Publish(new Event());
```
 where Event : IEvent
