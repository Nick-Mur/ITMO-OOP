using System.Collections.Generic;

namespace VendingMachine.Domain.Entities;

/// <summary>
///     Описывает результат операции в торговом автомате.
/// </summary>
public abstract class TransactionResultBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="TransactionResultBase"/>.
    /// </summary>
    /// <param name="isSuccessful">Флаг успешности операции.</param>
    /// <param name="message">Сообщение о результате.</param>
    /// <param name="dispensedProduct">Выданный клиенту товар.</param>
    /// <param name="change">Монеты, возвращённые в качестве сдачи.</param>
    protected TransactionResultBase(
        bool isSuccessful,
        string message,
        ProductBase? dispensedProduct,
        IReadOnlyCollection<CoinBase> change)
    {
        IsSuccessful = isSuccessful;
        Message = message;
        DispensedProduct = dispensedProduct;
        Change = change;
    }

    /// <summary>
    ///     Возвращает признак успешности операции.
    /// </summary>
    public bool IsSuccessful { get; }

    /// <summary>
    ///     Возвращает текстовое сообщение о результате.
    /// </summary>
    public string Message { get; }

    /// <summary>
    ///     Возвращает выданный товар, если он был выдан.
    /// </summary>
    public ProductBase? DispensedProduct { get; }

    /// <summary>
    ///     Возвращает сдачу, возвращённую покупателю.
    /// </summary>
    public IReadOnlyCollection<CoinBase> Change { get; }
}
