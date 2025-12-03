namespace LAB3.OrdersSystem.Core;
using Order.Core;
using Operations;
using LAB3.Menu.Core;

public class OrdersSystem : OrdersSystemStorage
{
    private readonly OrdersSystemAdder _adder;
    private readonly OrdersSystemRemover _remover;
    private readonly OrdersSystemEditor _editor;
    private readonly OrdersSystemStatusChanger _statusChanger;
    private readonly OrdersSystemTypeChanger _typeChanger;
    private readonly OrdersSystemTotalPriceGetter _totalPriceGetter;

    public OrdersSystem()
    {
        _adder = new OrdersSystemAdder(_items);
        _remover = new OrdersSystemRemover(_items);
        _editor = new OrdersSystemEditor(_items);
        _statusChanger = new OrdersSystemStatusChanger(_items);
        _typeChanger = new OrdersSystemTypeChanger(_items);
        _totalPriceGetter = new OrdersSystemTotalPriceGetter(_items);
    }

    // Управление заказами
    public void Add(Order order) => _adder.Add(order);
    public void AddRange(IEnumerable<Order> orders) => _adder.AddRange(orders);

    public void Remove(Order order) => _remover.Remove(order);
    public void RemoveRange(IEnumerable<Order> orders) => _remover.RemoveRange(orders);
    public void Clear() => _remover.Clear();

    // Редактирование заказа
    public void AddDishToOrder(Order order, string dishName, Menu menu, int quantity = 1)
        => _editor.AddDishToOrder(order, dishName, menu, quantity);

    public void RemoveDishFromOrder(Order order, string dishName, int quantity = 1)
        => _editor.RemoveDishFromOrder(order, dishName, quantity);

    public void RemoveDishCompletelyFromOrder(Order order, string dishName)
        => _editor.RemoveDishCompletelyFromOrder(order, dishName);

    public void ClearOrder(Order order) => _editor.ClearOrder(order);

    // Изменение статуса заказа
    public void ChangeStatus(Order order, string newStatus)
        => _statusChanger.ChangeStatus(order, newStatus);

    public void ChangeStatusForMultiple(IEnumerable<Order> orders, string newStatus)
        => _statusChanger.ChangeStatusForMultiple(orders, newStatus);

    // Изменение типа заказа
    public void ChangeType(Order order, string newType)
        => _typeChanger.ChangeType(order, newType);

    public void ChangeTypeForMultiple(IEnumerable<Order> orders, string newType)
        => _typeChanger.ChangeTypeForMultiple(orders, newType);

    // Получение итоговой стоимости
    public int GetTotalPrice(Order order, Menu menu)
        => _totalPriceGetter.GetTotalPrice(order, menu);

    public int GetTotalPriceForAll(Menu menu)
        => _totalPriceGetter.GetTotalPriceForAll(menu);

    public Dictionary<Order, int> GetAllOrdersPrices(Menu menu)
        => _totalPriceGetter.GetAllOrdersPrices(menu);
}