namespace LAB3.Order.Core;
using Operations;
using LAB3.Menu.Core;

public class Order : OrderStorage
{
    private readonly OrderAdder _adder;
    private readonly OrderRemover _remover;
    private readonly OrderTotalPriceGetter _totalPriceGetter;

    public string OrderType { get; set; }
    public string OrderStatus {get; set;}

    public Order(string orderType)
    {
        OrderType = orderType;
        OrderStatus = "Готовится";

        _adder = new OrderAdder(_items);
        _remover = new OrderRemover(_items);
        _totalPriceGetter = new OrderTotalPriceGetter(_items);
    }
    
    public void AddByName(string dishName, Menu menu, int quantity = 1) 
        => _adder.AddByName(dishName, menu, quantity);
    
    public void AddRangeByName(IEnumerable<string> dishNames, Menu menu, int quantity = 1) 
        => _adder.AddRangeByName(dishNames, menu, quantity);

    // Удаление блюд по имени с указанием количества
    public void RemoveByName(string dishName, int quantity = 1) 
        => _remover.RemoveByName(dishName, quantity);
    
    public void RemoveCompletelyByName(string dishName) 
        => _remover.RemoveCompletelyByName(dishName);
    
    public void RemoveRangeByName(IEnumerable<string> dishNames, int quantity = 1) 
        => _remover.RemoveRangeByName(dishNames, quantity);

    public void Clear() => _remover.Clear();

    // Получение общей цены заказа
    public int GetTotalPrice(Menu menu) => _totalPriceGetter.GetTotalPrice(OrderType, menu);
}
