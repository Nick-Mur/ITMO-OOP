namespace LAB3.Patterns.Observer;

using Order.Core;

public interface IOrderObserver
{
    void Update(Order order);
}
