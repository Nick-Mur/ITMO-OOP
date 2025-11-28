namespace Inventory;
using Objects;

public class InventoryAdder(List<Item> items)
{
    private readonly List<Item> _items = items;

    public void Add(Item item) => _items.Add(item);

    public void Add(params Item[] items)
    {
        foreach (var item in items)
            _items.Add(item);
    }

    public void AddRange(IEnumerable<Item> items)
    {
        foreach (var item in items)
            _items.Add(item);
    }
}
