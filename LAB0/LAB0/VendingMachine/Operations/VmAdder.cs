namespace LAB0.VendingMachine.Operations;

using Objects;

public class VmAdder(List<Product> products, List<Coin> coins)
{
    private readonly List<Product> _products = products;
    private readonly List<Coin> _coins = coins;
    
    public void AddProduct(Product product)
    {
        EnsureSamePrice(product);
        _products.Add(product);
    }
    
    public void AddCoin(Coin coin) => _coins.Add(coin);
    
    public void AddProducts(IEnumerable<Product> products)
    {
        foreach (var product in products)
            AddProduct(product);
    }
    public void AddCoins(IEnumerable<Coin> coins) => _coins.AddRange(coins);

    private void EnsureSamePrice(Product product)
    {
        var conflict = _products.FirstOrDefault(p =>
            p.Name.Equals(product.Name, StringComparison.OrdinalIgnoreCase) &&
            p.Price != product.Price);

        if (conflict != null)
        {
            throw new InvalidOperationException(
                $"Товар '{product.Name}' уже существует с другой ценой. " +
                $"Существующая цена: {conflict.Price}, новая цена: {product.Price}");
        }
    }
}
