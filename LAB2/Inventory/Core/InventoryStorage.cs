namespace Inventory;
using Objects;

public class InventoryStorage
{
    protected readonly List<Item> _items = new();
    public IReadOnlyList<Item> Items => _items;
}