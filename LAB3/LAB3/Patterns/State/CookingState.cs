namespace LAB3.Patterns.State;

using LAB3.Order.Core;

public class CookingState : IOrderState
{
    public void Next(Order order)
    {
        order.SetState(new DeliveryState());
    }

    public void Cancel(Order order)
    {
        order.SetState(new CancelledState());
    }

    public string GetStatusName() => "Cooking";
}
