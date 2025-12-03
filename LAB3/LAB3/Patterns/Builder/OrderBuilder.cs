namespace LAB3.Patterns.Builder;

using Order.Core;
using Menu.Core;
using Strategy;

public class OrderBuilder(Menu menu)
{
    private readonly Order _order = new Order();
    private readonly Menu _menu = menu;

    public OrderBuilder SetType(string type)
    {
        _order.OrderType = type;
        if (type == "Fast")
            _order.SetPriceStrategy(new FastDeliveryPriceStrategy());
        else
            _order.SetPriceStrategy(new StandardPriceStrategy());
        
        return this;
    }

    public OrderBuilder AddDish(string dishName, int quantity = 1)
    {
        _order.AddByName(dishName, _menu, quantity);
        return this;
    }

    public Order Build()
    {
        return _order;
    }
}
