namespace VendingMachine.Domain.Entities;

/// <summary>
///     Конкретная реализация результата операции.
/// </summary>
public sealed class TransactionResult : TransactionResultBase
{
    /// <summary>
    ///     Инициализирует новый экземпляр класса <see cref="TransactionResult"/>.
    /// </summary>
    /// <param name="isSuccessful">Флаг успешности операции.</param>
    /// <param name="message">Сообщение о результате.</param>
    /// <param name="dispensedProduct">Выданный товар.</param>
    /// <param name="change">Монеты, возвращённые как сдача.</param>
    public TransactionResult(
        bool isSuccessful,
        string message,
        ProductBase? dispensedProduct,
        IReadOnlyCollection<CoinBase> change)
        : base(isSuccessful, message, dispensedProduct, change)
    {
    }
}
