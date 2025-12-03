namespace LAB3.Patterns.Observer;

using Order.Core;

public class ConsoleLoggerObserver : IOrderObserver
{
    public void Update(Order order)
    {
        Console.WriteLine($"[Observer] Order status changed to: {order.State.GetStatusName()}");
    }
}
