namespace LAB3.Order.Core;
using Objects;
using Operations;
using LAB3.Menu.Core;


public class Order : OrderStorage
{
    private readonly OrderAdder _adder;
    private readonly DishRemover _remover;
    private readonly DishTotalPriceGetter _totalPriceGetter;

    public string OrderType { get; set; }

    public Order(string orderType)
    {
        OrderType = orderType;

        _adder = new OrderAdder(_items);
        _remover = new DishRemover(_items);
        _totalPriceGetter = new DishTotalPriceGetter(_items);
    }

    public void Add(Dish item) => _adder.Add(item);
    public void AddRange(IEnumerable<Dish> items) => _adder.AddRange(items);

    public void Remove(Dish item) => _remover.Remove(item);
    public void RemoveRange(IEnumerable<Dish> items) => _remover.RemoveRange(items);

    public int GetTotalPrice() => _totalPriceGetter.GetTotalPrice(OrderType);

    public void AddByName(string dishName, Menu.Core.Menu menu) => _adder.AddByName(dishName, menu);
    public void AddRangeByName(IEnumerable<string> dishNames, Menu.Core.Menu menu) => _adder.AddRangeByName(dishNames, menu);
}

