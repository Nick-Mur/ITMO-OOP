namespace LAB0.VendingMachine.Operations;

using Objects;

public class VmViewer(List<Product> products, List<Coin> coins)
{
    private readonly List<Coin>  _coins = coins;
    private readonly List<Product>  _products = products;
    
    public IReadOnlyDictionary<string, (int count, int price)> ViewProducts()
    {
        return _products
            .GroupBy(p => p.Name)
            .ToDictionary(
                g => g.Key,
                g => (
                    count: g.Count(),
                    price: g.First().Price
                )
            );
    }

    
    public IReadOnlyDictionary<int, int> ViewCoins()
    {
        return _coins
            .GroupBy(c => c.Nominal)
            .ToDictionary(g => g.Key, g => g.Count());
    }


}