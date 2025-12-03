namespace LAB3.Order.Core;

public abstract class OrderStorage
{
    protected readonly List<string> _items = new();
    public IReadOnlyList<string> Items => _items;
}