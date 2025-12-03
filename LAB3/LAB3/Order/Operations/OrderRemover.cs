namespace LAB3.Order.Operations;

public class OrderRemover(Dictionary<string, int> items)
{
    private readonly Dictionary<string, int> _items = items;
    
    public void RemoveByName(string dishName, int quantity = 1)
    {
        if (!_items.ContainsKey(dishName))
            return;

        if (quantity <= 0)
            throw new ArgumentException("Количество должно быть больше нуля.", nameof(quantity));

        if (_items[dishName] <= quantity)
            _items.Remove(dishName);
        else
            _items[dishName] -= quantity;
    }

    public void RemoveCompletelyByName(string dishName)
    {
        _items.Remove(dishName);
    }

    public void RemoveRangeByName(IEnumerable<string> dishNames, int quantity = 1)
    {
        foreach (var dishName in dishNames)
            RemoveByName(dishName, quantity);
    }

    public void Clear()
    {
        _items.Clear();
    }
}
