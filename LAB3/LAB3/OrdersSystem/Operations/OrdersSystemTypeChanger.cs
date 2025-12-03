namespace LAB3.OrdersSystem.Operations;
using Order.Core;
using Patterns.Strategy;

public class OrdersSystemTypeChanger(List<Order> items)
{
    private readonly List<Order> _items = items;

    public void ChangeType(Order order, string newType)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        ArgumentException.ThrowIfNullOrWhiteSpace(newType, nameof(newType));

        order.OrderType = newType;
        
        // Update strategy based on type
        if (newType == "Fast")
        {
            order.SetPriceStrategy(new FastDeliveryPriceStrategy());
        }
        else
        {
            order.SetPriceStrategy(new StandardPriceStrategy());
        }
    }

    public void ChangeTypeForMultiple(IEnumerable<Order> orders, string newType)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newType, nameof(newType));

        foreach (var order in orders)
        {
            ChangeType(order, newType);
        }
    }
}
