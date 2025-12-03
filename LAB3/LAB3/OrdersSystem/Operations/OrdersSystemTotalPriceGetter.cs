namespace LAB3.OrdersSystem.Operations;
using Order.Core;
using LAB3.Menu.Core;

public class OrdersSystemTotalPriceGetter(List<Order> items)
{
    private readonly List<Order> _items = items;

    public int GetTotalPrice(Order order, Menu menu)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        return order.GetTotalPrice(menu);
    }

    public int GetTotalPriceForAll(Menu menu)
    {
        int totalPrice = 0;
        
        foreach (var order in _items)
        {
            totalPrice += order.GetTotalPrice(menu);
        }

        return totalPrice;
    }

    public Dictionary<Order, int> GetAllOrdersPrices(Menu menu)
    {
        var prices = new Dictionary<Order, int>();

        foreach (var order in _items)
        {
            prices[order] = order.GetTotalPrice(menu);
        }

        return prices;
    }
}
