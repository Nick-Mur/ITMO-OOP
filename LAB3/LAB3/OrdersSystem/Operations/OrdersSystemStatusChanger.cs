namespace LAB3.OrdersSystem.Operations;
using Order.Core;

public class OrdersSystemStatusChanger(List<Order> items)
{
    private readonly List<Order> _items = items;

    public void ChangeStatus(Order order, string newStatus)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        ArgumentException.ThrowIfNullOrWhiteSpace(newStatus, nameof(newStatus));

        order.OrderStatus = newStatus;
    }

    public void ChangeStatusForMultiple(IEnumerable<Order> orders, string newStatus)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newStatus, nameof(newStatus));

        foreach (var order in orders)
        {
            if (!_items.Contains(order))
                throw new InvalidOperationException("Один из заказов не найден в системе.");

            order.OrderStatus = newStatus;
        }
    }
}
