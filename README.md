# rubens
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
 where Event : IEvent
