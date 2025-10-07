using System;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Services;

/// <summary>
///     Стандартная реализация <see cref="VendingMachineServiceBase"/>.
/// </summary>
public sealed class VendingMachineService : VendingMachineServiceBase
{
    private readonly InMemoryInventoryService _inventoryService;
    private readonly InMemoryPaymentService _paymentService;

    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="VendingMachineService"/>.
    /// </summary>
    /// <param name="inventoryService">Сервис управления инвентарём.</param>
    /// <param name="paymentService">Сервис управления платежами.</param>
    public VendingMachineService(
        InMemoryInventoryService inventoryService,
        InMemoryPaymentService paymentService)
    {
        _inventoryService = inventoryService;
        _paymentService = paymentService;
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<InventorySlotBase> GetAvailableProducts()
    {
        return _inventoryService.GetInventory();
    }

    /// <inheritdoc />
    public override void InsertCoin(CoinBase coin)
    {
        _paymentService.InsertCoin(coin);
    }

    /// <inheritdoc />
    public override TransactionResultBase Purchase(string productId)
    {
        var insertedAmount = _paymentService.GetInsertedAmount();
        var slot = _inventoryService.GetInventory()
            .FirstOrDefault(item => item.Product.Id == productId);

        if (slot is null)
        {
            return new TransactionResult(false, "Товар не найден.", null, Array.Empty<CoinBase>());
        }

        if (slot.Quantity <= 0)
        {
            return new TransactionResult(false, "Товар закончился.", null, Array.Empty<CoinBase>());
        }

        if (insertedAmount < slot.Product.Price)
        {
            return new TransactionResult(false, "Недостаточно средств.", null, Array.Empty<CoinBase>());
        }

        var dispensedSlot = _inventoryService.TryDispense(productId);
        if (dispensedSlot is null)
        {
            return new TransactionResult(false, "Не удалось выдать товар.", null, Array.Empty<CoinBase>());
        }

        var changeAmount = insertedAmount - slot.Product.Price;
        var change = _paymentService.MakeChange(changeAmount);
        if (changeAmount > 0 && change.Count == 0)
        {
            return new TransactionResult(false, "Не удалось выдать сдачу.", null, _paymentService.CancelTransaction());
        }

        _paymentService.CommitTransaction();

        return new TransactionResult(true, "Приятного пользования!", dispensedSlot.Product, change);
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<CoinBase> Cancel()
    {
        return _paymentService.CancelTransaction();
    }

    /// <inheritdoc />
    public override void Restock(ProductBase product, int quantity)
    {
        _inventoryService.AddProduct(product, quantity);
    }

    /// <inheritdoc />
    public override IReadOnlyCollection<CoinBase> CollectFunds()
    {
        return _paymentService.CollectFunds();
    }
}
