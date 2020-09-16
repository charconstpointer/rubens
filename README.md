# Rubens (ruːbənz)
## 📮 Basic pub-sub functionality on top of SignalR  
#### Other communication methods possibly coming in the future (gRPC, AMQP)
![](https://i.imgur.com/tUiHROH.png)
### 🐕‍🦺 Rubens's server https://hub.docker.com/r/controllerbase/rubens
To run it 
```
docker run -p 4444:4444 controllerbase/rubens 
```
### 🧙🏽‍♂️ With Microsoft's DI
#### 🥴 Register
```
services.AddRubens(options =>
{
    options.ConnectionString = "http://localhost:4444";
});
```
#### 🏌🏽‍♀️ Run
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
