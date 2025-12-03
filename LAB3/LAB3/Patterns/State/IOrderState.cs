namespace LAB3.Patterns.State;

using Order.Core;

public interface IOrderState
{
    void Next(Order order);
    void Cancel(Order order);
    string GetStatusName();
}
