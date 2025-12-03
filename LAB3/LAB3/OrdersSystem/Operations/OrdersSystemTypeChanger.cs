namespace LAB3.OrdersSystem.Operations;
using Order.Core;

public class OrdersSystemTypeChanger(List<Order> items)
{
    private readonly List<Order> _items = items;

    public void ChangeType(Order order, string newType)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        ArgumentException.ThrowIfNullOrWhiteSpace(newType, nameof(newType));

        order.OrderType = newType;
    }

    public void ChangeTypeForMultiple(IEnumerable<Order> orders, string newType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newType, nameof(newType));

        foreach (var order in orders)
        {
            if (!_items.Contains(order))
                throw new InvalidOperationException("Один из заказов не найден в системе.");

            order.OrderType = newType;
        }
    }
}
