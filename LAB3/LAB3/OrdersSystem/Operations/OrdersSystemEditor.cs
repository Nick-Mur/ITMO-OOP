namespace LAB3.OrdersSystem.Operations;
using Order.Core;
using LAB3.Menu.Core;

public class OrdersSystemEditor(List<Order> items)
{
    private readonly List<Order> _items = items;

    public void AddDishToOrder(Order order, string dishName, Menu menu, int quantity = 1)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        order.AddByName(dishName, menu, quantity);
    }

    public void RemoveDishFromOrder(Order order, string dishName, int quantity = 1)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        order.RemoveByName(dishName, quantity);
    }

    public void RemoveDishCompletelyFromOrder(Order order, string dishName)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        order.RemoveCompletelyByName(dishName);
    }

    public void ClearOrder(Order order)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        order.Clear();
    }
}
