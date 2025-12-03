namespace LAB3.Order.Core;
using Operations;
using LAB3.Menu.Core;
using Patterns.Strategy;
using Patterns.State;
using Patterns.Observer;

public class Order : OrderStorage
{
    private readonly OrderAdder _adder;
    private readonly OrderRemover _remover;
    
    private IPriceCalculationStrategy _priceStrategy;
    private IOrderState _state;
    private readonly List<IOrderObserver> _observers = new();

    public string OrderType { get; set; }

    public string OrderStatus => _state.GetStatusName();

    public IOrderState State => _state;

    public Order(string orderType = "basic")
    {
        OrderType = orderType;

        if (orderType == "Fast")
            _priceStrategy = new FastDeliveryPriceStrategy();
        else
            _priceStrategy = new StandardPriceStrategy();
            
        _state = new NewState();

        _adder = new OrderAdder(_items);
        _remover = new OrderRemover(_items);
    }
    
    // Strategy Methods
    public void SetPriceStrategy(IPriceCalculationStrategy strategy)
    {
        _priceStrategy = strategy;
    }

    // State Methods
    public void SetState(IOrderState state)
    {
        _state = state;
        NotifyObservers();
    }

    public void NextState()
    {
        _state.Next(this);
    }

    public void Cancel()
    {
        _state.Cancel(this);
    }

    // Observer Methods
    public void Attach(IOrderObserver observer)
    {
        _observers.Add(observer);
    }

    public void Detach(IOrderObserver observer)
    {
        _observers.Remove(observer);
    }

    private void NotifyObservers()
    {
        foreach (var observer in _observers)
        {
            observer.Update(this);
        }
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

    // Получение общей цены заказа через стратегию
    public int GetTotalPrice(Menu menu) => _priceStrategy.CalculatePrice(this, menu);
}
