namespace LAB3.Patterns.State;

using LAB3.Order.Core;

public class DeliveryState : IOrderState
{
    public void Next(Order order)
    {
        order.SetState(new CompletedState());
    }

    public void Cancel(Order order)
    {
        throw new InvalidOperationException("Заказ доставляется, его нельзя отменить.");
    }

    public string GetStatusName() => "Delivery";
}
