namespace LAB3.OrdersSystem.Operations;
using Order.Core;

public class OrdersSystemAdder(List<Order> items)
{
    private readonly List<Order> _items = items;

    public void Add(Order order)
    {
        ArgumentNullException.ThrowIfNull(order);
        _items.Add(order);
    }

    public void AddRange(IEnumerable<Order> orders)
    {
        ArgumentNullException.ThrowIfNull(orders);
        
        foreach (var order in orders)
        {
            ArgumentNullException.ThrowIfNull(order);
            _items.Add(order);
        }
    }
}
