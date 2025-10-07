using System.Collections.Generic;
using System.Linq;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Services;
using Xunit;

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
}
