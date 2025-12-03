namespace LAB3.Menu.Core;
using Objects;


public abstract class MenuStorage
{
    protected readonly List<Dish> _items = new();
    public IReadOnlyList<Dish> Items => _items;
}