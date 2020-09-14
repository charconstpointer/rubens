# Rubens (ruːbənz)
🌎 powered by SignalR
![](https://i.imgur.com/ZRPMZau.png)
```
app.UseRubens(x =>
{
    x.Subscribe<Event>(@event =>
    {
        Console.WriteLine($">{@event}");
        Console.WriteLine($"<{@event}");
    });
});
```
Publishing events
 
```
await _bus.Publish(new Event());
```
 where Event : IEvent


Without DI
```
var cfg = new RubensConfiguration{ConnectionString = "https://localhost:5001"};
var ctl = new ControlPlane(cfg);
var bus = new Bus(ctl);
await bus.Subscribe<Event>(@event => Console.WriteLine(@event.Body));
```
