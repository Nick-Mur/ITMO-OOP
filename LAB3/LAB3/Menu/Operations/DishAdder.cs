namespace LAB3.Menu.Operations;
using Objects;

public class DishAdder(List<Dish> items)
{
    private readonly List<Dish> _items = items;

    public void Add(Dish item) => _items.Add(item);

    public void AddRange(IEnumerable<Dish> items)
    {
        foreach (var item in items)
            _items.Add(item);
    }
}
