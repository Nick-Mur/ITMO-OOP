namespace LAB3.OrdersSystem.Core;
using Order.Core;

public abstract class OrdersSystemStorage
{
    protected readonly List<Order> _items = new();
    public IReadOnlyList<Order> Items => _items;
}