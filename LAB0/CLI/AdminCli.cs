namespace CLI;

using LAB0.VendingMachine.Core;
using LAB0.Objects;

public static class AdminCli
{
    public static void Run(VendingMachine vm)
    {
        Console.Write("Админ-код: ");
        var code = Console.ReadLine() ?? "";

        while (true)
        {
            Console.WriteLine("\n--- АДМИН ---");
            Console.WriteLine("1) Добавить товар");
            Console.WriteLine("2) Добавить монеты");
            Console.WriteLine("3) Забрать все монеты");
            Console.WriteLine("4) Забрать все товары");
            Console.WriteLine("0) Назад");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        AddProduct(vm, code);
                        break;

                    case "2":
                        AddCoins(vm, code);
                        break;

                    case "3":
                        vm.PickUpAllCoins(code);
                        Console.WriteLine("Монеты забраны.");
                        break;

                    case "4":
                        vm.PickUpAllProducts(code);
                        Console.WriteLine("Товары забраны.");
                        break;

                    case "0":
                        return;

                    default:
                        Console.WriteLine("Неизвестная команда.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    private static void AddProduct(VendingMachine vm, string code)
    {
        Console.Write("Название: ");
        var name = Console.ReadLine();

        Console.Write("Цена: ");
        if (!int.TryParse(Console.ReadLine(), out var price)) return;

        Console.Write("Количество: ");
        if (!int.TryParse(Console.ReadLine(), out var count)) return;

        for (int i = 0; i < count; i++)
            vm.AddProduct(new Product(name!, price), code);

        Console.WriteLine("Товар добавлен.");
    }

    private static void AddCoins(VendingMachine vm, string code)
    {
        Console.Write("Номинал: ");
        if (!int.TryParse(Console.ReadLine(), out var nominal)) return;

        Console.Write("Количество: ");
        if (!int.TryParse(Console.ReadLine(), out var count)) return;

        for (int i = 0; i < count; i++)
            vm.AddCoin(new Coin(nominal), code);

        Console.WriteLine("Монеты добавлены.");
    }
}
