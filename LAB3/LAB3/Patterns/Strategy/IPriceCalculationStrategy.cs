namespace LAB3.Patterns.Strategy;

using Order.Core;
using Menu.Core;

public interface IPriceCalculationStrategy
{
    int CalculatePrice(Order order, Menu menu);
}
