namespace LAB3.Patterns.State;

using LAB3.Order.Core;

public class NewState : IOrderState
{
    public void Next(Order order)
    {
        order.SetState(new CookingState());
    }

    public void Cancel(Order order)
    {
        order.SetState(new CancelledState());
    }

    public string GetStatusName() => "New";
}
