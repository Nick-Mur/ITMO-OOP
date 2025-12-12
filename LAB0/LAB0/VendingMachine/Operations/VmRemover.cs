namespace LAB0.VendingMachine.Operations;

using Objects;

public class VmRemover(List<Product> products, List<Coin> coins)
{
    private readonly List<Product> _products = products;
    private readonly List<Coin> _coins = coins;

    public void RemoveProduct(Product product) => _products.Remove(product);
    public void RemoveCoin(Coin coin) => _coins.Remove(coin);

    public void RemoveProducts(IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            RemoveProduct(product);
        }
    }
    public void RemoveCoins(IEnumerable<Coin> coins)
    {
        foreach (var coin in coins)
        {
            RemoveCoin(coin);
        }
    }
}
