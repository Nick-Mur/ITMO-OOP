namespace LAB3.Patterns.State;

using Order.Core;

public class CompletedState : IOrderState
{
    public void Next(Order order)
    {
        throw new InvalidOperationException("Заказ завершён.");
    }

    public void Cancel(Order order)
    {
        throw new InvalidOperationException("Нельзя отменить завершённый заказ.");
    }

    public string GetStatusName() => "Completed";
}
