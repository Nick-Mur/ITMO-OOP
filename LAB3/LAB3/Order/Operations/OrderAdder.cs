namespace LAB3.Order.Operations;
using Objects;
using LAB3.Menu.Core;

public class OrderAdder(Dictionary<string, int> items)
{
    private readonly Dictionary<string, int> _items = items;
    
    public void AddByName(string dishName, Menu menu, int quantity = 1)
    {
        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть больше нуля.", nameof(quantity));

        Dish? dish = menu.GetDishByName(dishName);
        if (dish == null)
            throw new InvalidOperationException($"Блюдо '{dishName}' не найдено в меню.");

        if (_items.ContainsKey(dishName))
            _items[dishName] += quantity;
        else
            _items[dishName] = quantity;
    }

    public void AddRangeByName(IEnumerable<string> dishNames, Menu menu, int quantity = 1)
    {
        foreach (var dishName in dishNames)
            AddByName(dishName, menu, quantity);
    }
}