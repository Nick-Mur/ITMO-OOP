using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services;

/// <summary>
///     Базовый контракт для доступа и управления данными об инвентаре.
/// </summary>
public abstract class InventoryServiceBase
{
    /// <summary>
    ///     Возвращает все ячейки инвентаря.
    /// </summary>
    /// <returns>Коллекция ячеек инвентаря.</returns>
    public abstract IReadOnlyCollection<InventorySlotBase> GetInventory();

    /// <summary>
    ///     Пытается выдать товар из инвентаря.
    /// </summary>
    /// <param name="productId">Идентификатор товара для выдачи.</param>
    /// <returns>Ячейка, выдавшая товар, или <c>null</c>, если выдача невозможна.</returns>
    public abstract InventorySlotBase? TryDispense(string productId);

    /// <summary>
    ///     Пополняет запас товара.
    /// </summary>
    /// <param name="productId">Идентификатор пополняемого товара.</param>
    /// <param name="amount">Количество добавляемых единиц.</param>
    public abstract void Refill(string productId, int amount);

    /// <summary>
    ///     Добавляет новый товар в инвентарь.
    /// </summary>
    /// <param name="product">Добавляемый товар.</param>
    /// <param name="quantity">Начальное количество.</param>
    public abstract void AddProduct(ProductBase product, int quantity);
}
