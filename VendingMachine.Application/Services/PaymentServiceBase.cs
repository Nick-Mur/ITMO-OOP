using System.Collections.Generic;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Services;

/// <summary>
///     Описывает процесс работы с платежами в торговом автомате.
/// </summary>
public abstract class PaymentServiceBase
{
    /// <summary>
    ///     Добавляет монету в кошелёк текущей транзакции.
    /// </summary>
    /// <param name="coin">Монета, внесённая покупателем.</param>
    public abstract void InsertCoin(CoinBase coin);

    /// <summary>
    ///     Возвращает суммарную внесённую сумму.
    /// </summary>
    /// <returns>Текущая внесённая сумма.</returns>
    public abstract decimal GetInsertedAmount();

    /// <summary>
    ///     Пытается подготовить сдачу для покупателя.
    /// </summary>
    /// <param name="amount">Требуемая сумма сдачи.</param>
    /// <returns>Коллекция монет, составляющая сдачу.</returns>
    public abstract IReadOnlyCollection<CoinBase> MakeChange(decimal amount);

    /// <summary>
    ///     Отменяет текущую операцию и возвращает внесённые монеты.
    /// </summary>
    /// <returns>Монеты, внесённые покупателем.</returns>
    public abstract IReadOnlyCollection<CoinBase> CancelTransaction();

    /// <summary>
    ///     Сохраняет накопленные средства для администратора.
    /// </summary>
    /// <returns>Монеты, хранившиеся в автомате.</returns>
    public abstract IReadOnlyCollection<CoinBase> CollectFunds();
}
