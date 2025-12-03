namespace Tests;

using Xunit;
using LAB3.OrdersSystem.Core;
using LAB3.Order.Core;
using LAB3.Menu.Core;
using LAB3.Objects;

public class OrdersSystemTests
{
    private readonly Menu _menu;
    private readonly OrdersSystem _ordersSystem;

    public OrdersSystemTests()
    {
        _menu = new Menu();
        _menu.Add(new Dish("Pizza", 500, 20));
        _menu.Add(new Dish("Burger", 300, 15));
        _menu.Add(new Dish("Water", 100, 0));

        _ordersSystem = new OrdersSystem();
    }

    [Fact]
    public void Add_ShouldAddOrderToSystem()
    {
        Order order = new Order();
        _ordersSystem.Add(order);

        Assert.Contains(order, _ordersSystem.Items);
    }

    [Fact]
    public void Remove_ShouldRemoveOrderFromSystem()
    {
        Order order = new Order();
        _ordersSystem.Add(order);
        _ordersSystem.Remove(order);

        Assert.DoesNotContain(order, _ordersSystem.Items);
    }

    [Fact]
    public void AddDishToOrder_ShouldAddDishCorrectly()
    {
        Order order = new Order();
        _ordersSystem.Add(order);

        _ordersSystem.AddDishToOrder(order, "Pizza", _menu, 2);

        Assert.True(order.Items.ContainsKey("Pizza"));
        Assert.Equal(2, order.Items["Pizza"]);
    }

    [Fact]
    public void RemoveDishFromOrder_ShouldRemoveDishCorrectly()
    {
        Order order = new Order();
        _ordersSystem.Add(order);
        _ordersSystem.AddDishToOrder(order, "Pizza", _menu, 2);

        _ordersSystem.RemoveDishFromOrder(order, "Pizza");

        Assert.Equal(1, order.Items["Pizza"]);
    }

    [Fact]
    public void RemoveDishCompletelyFromOrder_ShouldRemoveDishEntirely()
    {
        Order order = new Order();
        _ordersSystem.Add(order);
        _ordersSystem.AddDishToOrder(order, "Pizza", _menu, 2);

        _ordersSystem.RemoveDishCompletelyFromOrder(order, "Pizza");

        Assert.False(order.Items.ContainsKey("Pizza"));
    }

    [Fact]
    public void ChangeStatus_ShouldUpdateOrderStatus()
    {
        Order order = new Order();
        _ordersSystem.Add(order);

        _ordersSystem.ChangeStatus(order, "Completed");

        Assert.Equal("Completed", order.OrderStatus);
    }

    [Fact]
    public void ChangeType_ShouldUpdateOrderType()
    {
        Order order = new Order();
        _ordersSystem.Add(order);

        _ordersSystem.ChangeType(order, "Fast");

        Assert.Equal("Fast", order.OrderType);
    }

    [Fact]
    public void GetTotalPrice_ShouldCalculateCorrectly()
    {
        Order order = new Order();
        _ordersSystem.Add(order);
        _ordersSystem.AddDishToOrder(order, "Pizza", _menu, 2); // 500 * 2 = 1000
        _ordersSystem.AddDishToOrder(order, "Water", _menu); // 100 * 1 = 100

        int totalPrice = _ordersSystem.GetTotalPrice(order, _menu);

        Assert.Equal(1100, totalPrice);
    }

    [Fact]
    public void GetTotalPrice_FastOrder_ShouldAddExtraCharge()
    {
        Order order = new Order("Fast");
        _ordersSystem.Add(order);
        _ordersSystem.AddDishToOrder(order, "Pizza", _menu); // 500

        int totalPrice = _ordersSystem.GetTotalPrice(order, _menu);

        Assert.Equal(600, totalPrice); // 500 + 100 (Fast charge)
    }

    [Fact]
    public void GetTotalPriceForAll_ShouldSumAllOrders()
    {
        Order order1 = new Order();
        _ordersSystem.Add(order1);
        _ordersSystem.AddDishToOrder(order1, "Pizza", _menu); // 500

        Order order2 = new Order();
        _ordersSystem.Add(order2);
        _ordersSystem.AddDishToOrder(order2, "Burger", _menu); // 300

        int total = _ordersSystem.GetTotalPriceForAll(_menu);

        Assert.Equal(800, total);
    }
    [Fact]
    public void Builder_ShouldCreateOrderCorrectly()
    {
        var builder = new LAB3.Patterns.Builder.OrderBuilder(_menu);
        var order = builder
            .SetType("Fast")
            .AddDish("Pizza", 2)
            .Build();

        _ordersSystem.Add(order);

        Assert.Equal("Fast", order.OrderType);
        Assert.Equal(2, order.Items["Pizza"]);
        Assert.Equal(1100, _ordersSystem.GetTotalPrice(order, _menu));
    }
}
