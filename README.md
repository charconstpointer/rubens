# Rubens (ruːbənz)
## 📮 Basic pub-sub functionality on top of SignalR  
#### Other communication methods possibly coming in the future (gRPC, AMQP)
![](https://i.imgur.com/5qo5aQ0.png)
##### PW 💗
### 🐕‍🦺 Rubens' server https://hub.docker.com/r/controllerbase/rubens
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
##### In Memory Bus
```
services.AddRubens(options =>
{
    options.UseInMemoryBus = true;
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
var logger = new Logger<Bus>(new LoggerFactory());
var bus = new Bus(ctl, logger:logger);
await bus.Subscribe<Event>(@event => Console.WriteLine(@event.Body));
```
### 🥳 Publishing events
```
await _bus.Publish(new Event());
```
 where Event : IEvent
