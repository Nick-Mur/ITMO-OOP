namespace LAB3.Menu.Operations;
using Objects;


public class DishGetter(List<Dish> items)
{
    private readonly List<Dish> _items = items;
    
    public Dish GetDishByName(string dishName)
    {
        foreach (var item in _items)
        {
            if (item.Name == dishName)
                return item;
        }

        throw new InvalidOperationException($"Блюдо '{dishName}' не найдено в меню.");
    }
}
