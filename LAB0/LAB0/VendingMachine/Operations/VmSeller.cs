namespace LAB0.VendingMachine.Operations;

using System.Linq;
using Objects;

public class VmSeller
{
    private readonly List<Product> _products;
    private readonly List<Coin> _coins;
    private readonly VmChangeGiver _vmChangeGiver;
    private readonly GetterProductBuyName _getterProductBuyName;

    public VmSeller(List<Product> products, List<Coin> coins)
    {
        _products = products;
        _coins = coins;
        _vmChangeGiver = new VmChangeGiver(coins = _coins);
        _getterProductBuyName = new GetterProductBuyName(_products);
    }

    public (Product product, List<Coin> change) Buy(string productName, IEnumerable<Coin> coins)
    {
        Product product = _getterProductBuyName.GetProductByName(productName);
        
        List<Coin> insertedCoins = coins.ToList();
        int insertedSum = insertedCoins.Sum(c => c.Nominal);
        
        if (insertedSum < product.Price)
            throw new InvalidOperationException($"Недостаточно средств. Цена: {product.Price}, внесено: {insertedSum}.");
        
        int changeAmount = insertedSum - product.Price;
        
        if (!_vmChangeGiver.TryReturnChange(changeAmount, out List<Coin> change)) // уже вычел монеты из хранилища
            throw new InvalidOperationException("Автомат не может дать сдачи.");
        
        _coins.AddRange(insertedCoins);
        
        _products.Remove(product);

        return (product, change);
    }
}
