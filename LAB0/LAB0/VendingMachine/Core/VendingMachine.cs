namespace LAB0.VendingMachine.Core;

using Objects;
using Operations;

public class VendingMachine : VendingMachineStorage
{
    private readonly string _adminCode;
    private readonly VmAdder _vmAdder;
    private readonly VmRemover _vmRemover;
    private readonly GetterProductBuyName _getterProductBuyName;
    private readonly VmSeller _vmSeller;
    private readonly GetterCoinBuyNominal _getterCoinBuyNominal;
    private readonly VmViewer _vmViewer;

    public VendingMachine(string adminCode = "")
    {
        _adminCode = adminCode;
        _vmAdder = new VmAdder(_products, _coins);
        _vmRemover = new VmRemover(_products,  _coins);
        _getterProductBuyName = new GetterProductBuyName(_products);
        _vmSeller = new VmSeller(_products, _coins);
        _getterCoinBuyNominal = new GetterCoinBuyNominal(_coins);
        _vmViewer = new VmViewer(_products, _coins);
    }

    public void AddProduct(Product product, string adminCode)
    {
        EnsureAdminAccess(adminCode);
        _vmAdder.AddProduct(product);
    }

    public void AddProducts(IEnumerable<Product> products, string adminCode)
    {
        EnsureAdminAccess(adminCode);
        _vmAdder.AddProducts(products);
    }
    
    public void AddCoin(Coin coin, string adminCode)
    {
        EnsureAdminAccess(adminCode);
        _vmAdder.AddCoin(coin);
    }

    public void AddCoins(IEnumerable<Coin> coins, string adminCode)
    {
        EnsureAdminAccess(adminCode);
        _vmAdder.AddCoins(coins);
    }


    public Product RemoveProductByName(string productName, string adminCode)
    {
        EnsureAdminAccess(adminCode);
        Product product =  _getterProductBuyName.GetProductByName(productName);
        _vmRemover.RemoveProduct(product);
        return product;
    }

    public List<Product> RemoveProducts(IEnumerable<string> productNames, string adminCode)
    {
        List<Product> products = new();
        foreach (string productName in productNames)
        {
            products.Add(RemoveProductByName(productName,  adminCode));
        }

        return products;
    }
    
    public Coin RemoveCoinByNominal(int coinNominal, string adminCode)
    {
        EnsureAdminAccess(adminCode);
        Coin coin =  _getterCoinBuyNominal.GetCoinByNominal(coinNominal);
        _vmRemover.RemoveCoin(coin);
        return coin;
    }

    public List<Coin> RemoveCoins(IEnumerable<int> coinNominals, string adminCode)
    {
        List<Coin> coins = new();
        foreach (int coinNominal in coinNominals)
        {
            coins.Add(RemoveCoinByNominal(coinNominal,  adminCode));
        }

        return coins;
    }

    public IReadOnlyDictionary<string, (int count, int price)>  ViewProducts() => _vmViewer.ViewProducts();
    public IReadOnlyDictionary<int, int> ViewCoins() => _vmViewer.ViewCoins();

    public List<Product> PickUpAllProducts(string adminCode)
    {
        EnsureAdminAccess(adminCode);
        List<Product> products = _products.ToList();
        _vmRemover.RemoveProducts(products);
        return products;
    }
    
    public List<Coin> PickUpAllCoins(string adminCode)
    {
        EnsureAdminAccess(adminCode);
        List<Coin> coins = _coins.ToList();
        _vmRemover.RemoveCoins(coins);
        return coins;
    }

    public (Product product, List<Coin> change) Buy(string productName, IEnumerable<Coin> coins) => _vmSeller.Buy(productName, coins);

    private void EnsureAdminAccess(string providedCode)
    {
        if (_adminCode != providedCode)
            throw new UnauthorizedAccessException("Некорректный админ-код.");
    }
}
