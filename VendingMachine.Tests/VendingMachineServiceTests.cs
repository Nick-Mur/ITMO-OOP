using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Services;


namespace VendingMachine.Tests;

public class VendingMachineServiceTests
{
    [Fact]
    public void Purchase_ShouldDispenseProduct_WhenFundsAreSufficient()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("A1", "Вода", 1.50m), 1)
        };
        var wallet = new Wallet(new List<CoinBase> { new Coin(0.50m), new Coin(1.00m) });
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        service.InsertCoin(new Coin(1.00m));
        service.InsertCoin(new Coin(0.50m));

        var result = service.Purchase("A1");

        Assert.True(result.IsSuccessful);
        Assert.Equal("Вода", result.DispensedProduct?.Name);
        Assert.Empty(result.Change);
        Assert.Equal(0, inventoryService.GetInventory().Single().Quantity);
    }

    [Fact]
    public void Purchase_ShouldReturnFailure_WhenFundsAreInsufficient()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("B2", "Шоколад", 2.00m), 1)
        };
        var wallet = new Wallet(new List<CoinBase>());
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        service.InsertCoin(new Coin(1.00m));

        var result = service.Purchase("B2");

        Assert.False(result.IsSuccessful);
        Assert.Equal("Недостаточно средств.", result.Message);
        Assert.Null(result.DispensedProduct);
        Assert.Empty(result.Change);
        Assert.Equal(1, inventoryService.GetInventory().Single().Quantity);
    }

    [Fact]
    public void Cancel_ShouldReturnInsertedCoins()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("C3", "Чипсы", 1.80m), 1)
        };
        var wallet = new Wallet(new List<CoinBase>());
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        service.InsertCoin(new Coin(1.00m));
        service.InsertCoin(new Coin(0.50m));

        var refund = service.Cancel();

        Assert.Equal(2, refund.Count);
        Assert.Equal(1.50m, refund.Sum(coin => coin.Value));
    }

    [Fact]
    public void GetInsertedAmount_ShouldAccumulateCoins()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("D4", "Сок", 3.00m), 2)
        };
        var wallet = new Wallet(new List<CoinBase>());
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        Assert.Equal(0m, service.GetInsertedAmount());

        service.InsertCoin(new Coin(1.00m));
        service.InsertCoin(new Coin(0.50m));

        Assert.Equal(1.50m, service.GetInsertedAmount());
    }

    [Fact]
    public void Purchase_ShouldReturnChange_WhenOverpaidAndChangeAvailable()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("E5", "Кофе", 1.50m), 1)
        };
        // Машина имеет монету 0.50 для сдачи
        var wallet = new Wallet(new List<CoinBase> { new Coin(0.50m) });
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        service.InsertCoin(new Coin(1.00m));
        service.InsertCoin(new Coin(1.00m)); // 2.00 внесено

        var result = service.Purchase("E5");

        Assert.True(result.IsSuccessful);
        Assert.NotNull(result.DispensedProduct);
        Assert.Single(result.Change);
        Assert.Equal(0.50m, result.Change.Sum(c => c.Value));
    }

    [Fact]
    public void Purchase_ShouldFailAndRefund_WhenChangeNotAvailable()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("F6", "Чай", 1.50m), 1)
        };
        // Кошелёк автомата пуст — нет монет для сдачи 0.50
        var wallet = new Wallet(new List<CoinBase>());
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        service.InsertCoin(new Coin(1.00m));
        service.InsertCoin(new Coin(1.00m)); // 2.00 внесено

        var result = service.Purchase("F6");

        Assert.False(result.IsSuccessful);
        Assert.Equal("Недостаточно монет для сдачи.", result.Message);
        Assert.Null(result.DispensedProduct);
        Assert.Equal(2.00m, result.Change.Sum(c => c.Value));
        // Количество товара не уменьшилось
        Assert.Equal(1, inventoryService.GetInventory().Single().Quantity);
    }

    [Fact]
    public void Purchase_ShouldNotDecrementInventory_WhenChangeImpossible_LargeOverpay()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("X1", "Печенье", 2.00m), 3)
        };
        // Машина имеет мало монет (не сможет вернуть 10.00)
        var wallet = new Wallet(new List<CoinBase> { new Coin(0.50m), new Coin(1.00m) });
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        service.InsertCoin(new Coin(12.00m)); // гипотетический номинал

        var result = service.Purchase("X1");

        Assert.False(result.IsSuccessful);
        Assert.Equal("Недостаточно монет для сдачи.", result.Message);
        Assert.Null(result.DispensedProduct);
        Assert.Single(result.Change);
        Assert.Equal(12.00m, result.Change.Sum(c => c.Value));
        Assert.Equal(3, inventoryService.GetInventory().Single().Quantity);
    }

    [Fact]
    public void Restock_ShouldIncreaseQuantity_WhenProductExists()
    {
        var product = new Product("G7", "Батончик", 1.00m);
        var slots = new List<InventorySlot>
        {
            new(product, 2)
        };
        var wallet = new Wallet(new List<CoinBase>());
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        service.Restock(product, 3);

        var slot = inventoryService.GetInventory().Single(s => s.Product.Id == "G7");
        Assert.Equal(5, slot.Quantity);
    }

    [Fact]
    public void Restock_ShouldAddNewProduct_WhenNotExists()
    {
        var slots = new List<InventorySlot>();
        var wallet = new Wallet(new List<CoinBase>());
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        var product = new Product("H8", "Орехи", 2.20m);
        service.Restock(product, 4);

        var added = inventoryService.GetInventory().Single(s => s.Product.Id == "H8");
        Assert.Equal(4, added.Quantity);
    }

    [Fact]
    public void CollectFunds_ShouldReturnAndEmptyWallet()
    {
        var slots = new List<InventorySlot>
        {
            new(new Product("I9", "Печенье", 1.00m), 1)
        };
        var wallet = new Wallet(new List<CoinBase>
        {
            new Coin(1.00m),
            new Coin(0.50m)
        });
        var inventoryService = new InMemoryInventoryService(slots);
        var paymentService = new InMemoryPaymentService(wallet);
        var service = new VendingMachineService(inventoryService, paymentService);

        var funds = service.CollectFunds();
        Assert.Equal(2, funds.Count);
        Assert.Equal(1.50m, funds.Sum(c => c.Value));

        var second = service.CollectFunds();
        Assert.Empty(second);
    }
}