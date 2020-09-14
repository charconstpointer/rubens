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
