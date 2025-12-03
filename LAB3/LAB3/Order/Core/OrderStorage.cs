namespace LAB3.Order.Core;

public abstract class OrderStorage
{
    protected readonly Dictionary<string, int> _items = new();
    public IReadOnlyDictionary<string, int> Items => _items;
}