namespace LAB3.OrdersSystem.Operations;
using Order.Core;

public class OrdersSystemRemover(List<Order> items)
{
    private readonly List<Order> _items = items;

    public void Remove(Order order)
    {
        _items.Remove(order);
    }

    public void RemoveRange(IEnumerable<Order> orders)
    {
        foreach (var order in orders)
            _items.Remove(order);
    }

    public void Clear()
    {
        _items.Clear();
    }
}
