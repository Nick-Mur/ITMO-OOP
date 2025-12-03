namespace LAB3.OrdersSystem.Operations;
using Order.Core;
using Patterns.State;

public class OrdersSystemStatusChanger(List<Order> items)
{
    private readonly List<Order> _items = items;

    public void ChangeStatus(Order order, string newStatus)
    {
        if (!_items.Contains(order))
            throw new InvalidOperationException("Заказ не найден в системе.");

        ArgumentException.ThrowIfNullOrWhiteSpace(newStatus, nameof(newStatus));

        // Map string to state
        // This is a simple factory logic inside the operation
        switch (newStatus)
        {
            case "New":
                order.SetState(new NewState());
                break;
            case "Cooking":
                order.SetState(new CookingState());
                break;
            case "Delivery":
                order.SetState(new DeliveryState());
                break;
            case "Completed":
                order.SetState(new CompletedState());
                break;
            case "Cancelled":
                order.SetState(new CancelledState());
                break;
            default:
                throw new ArgumentException($"Unknown status: {newStatus}");
        }
    }

    public void ChangeStatusForMultiple(IEnumerable<Order> orders, string newStatus)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(newStatus, nameof(newStatus));

        foreach (var order in orders)
        {
            ChangeStatus(order, newStatus);
        }
    }
}
