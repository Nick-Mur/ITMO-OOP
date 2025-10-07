using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services;

/// <summary>
///     Описывает основные сценарии работы торгового автомата.
/// </summary>
public abstract class VendingMachineServiceBase
{
    /// <summary>
    ///     Возвращает перечень товаров, доступных для покупки.
    /// </summary>
    /// <returns>Коллекция ячеек инвентаря.</returns>
    public abstract IReadOnlyCollection<InventorySlotBase> GetAvailableProducts();

    /// <summary>
    ///     Добавляет монету в торговый автомат.
    /// </summary>
    /// <param name="coin">Монета, внесённая пользователем.</param>
    public abstract void InsertCoin(CoinBase coin);

    /// <summary>
    ///     Пытается приобрести товар по указанному идентификатору.
    /// </summary>
    /// <param name="productId">Идентификатор товара.</param>
    /// <returns>Результат операции покупки.</returns>
    public abstract TransactionResultBase Purchase(string productId);

    /// <summary>
    ///     Отменяет текущую операцию.
    /// </summary>
    /// <returns>Монеты, возвращённые пользователю.</returns>
    public abstract IReadOnlyCollection<CoinBase> Cancel();

    /// <summary>
    ///     Пополняет запас товара.
    /// </summary>
    /// <param name="product">Пополняемый товар.</param>
    /// <param name="quantity">Количество для добавления.</param>
    public abstract void Restock(ProductBase product, int quantity);

    /// <summary>
    ///     Собирает накопленные средства для администратора.
    /// </summary>
    /// <returns>Монеты, которые были собраны.</returns>
    public abstract IReadOnlyCollection<CoinBase> CollectFunds();
    
    /// <summary>
    ///     Возвращает текущую внесённую (но ещё не зафиксированную) сумму пользователя.
    /// </summary>
    /// <returns>Сумма внесённых монет в рамках текущей транзакции.</returns>
    public abstract decimal GetInsertedAmount();
}
