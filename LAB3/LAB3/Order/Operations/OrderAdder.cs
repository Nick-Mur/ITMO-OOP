namespace LAB3.Order.Operations;
using Objects;
using LAB3.Menu.Core;

public class OrderAdder(List<string> items)
{
    private readonly List<string> _items = items;
    
    public void AddByName(string dishName, Menu menu)
    {
        Dish? dish = menu.GetDishByName(dishName);
        if (dish == null)
            throw new InvalidOperationException($"Блюдо '{dishName}' не найдено в меню.");

        _items.Add(dishName);
    }

    public void AddRangeByName(IEnumerable<string> dishNames, Menu menu)
    {
        foreach (var dishName in dishNames)
            AddByName(dishName, menu);
    }
}