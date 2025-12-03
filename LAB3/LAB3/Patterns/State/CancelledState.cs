namespace LAB3.Patterns.State;

using Order.Core;

public class CancelledState : IOrderState
{
    public void Next(Order order)
    {
        throw new InvalidOperationException("Невозможно перевести отменённый заказ в следующее состояние.");
    }

    public void Cancel(Order order)
    {
        throw new InvalidOperationException("Заказ уже отменён.");
    }

    public string GetStatusName() => "Cancelled";
}
