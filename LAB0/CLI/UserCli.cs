namespace CLI;

using LAB0.VendingMachine.Core;
using LAB0.Objects;

public static class UserCli
{
    public static void Run(VendingMachine vm, List<Coin> insertedCoins)
    {
        while (true)
        {
            Console.WriteLine("\n--- ПОЛЬЗОВАТЕЛЬ ---");
            Console.WriteLine("1) Показать товары");
            Console.WriteLine("2) Вставить монету");
            Console.WriteLine("3) Показать внесённую сумму");
            Console.WriteLine("4) Купить товар");
            Console.WriteLine("5) Отмена");
            Console.WriteLine("0) Назад");
            Console.Write("Выбор: ");

            var choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        ShowProducts(vm);
                        break;

                    case "2":
                        InsertCoin(insertedCoins);
                        break;

                    case "3":
                        ShowInserted(insertedCoins);
                        break;

                    case "4":
                        Buy(vm, insertedCoins);
                        break;

                    case "5":
                        Cancel(insertedCoins);
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

    private static void ShowProducts(VendingMachine vm)
    {
        var products = vm.ViewProducts();
        if (products.Count == 0)
        {
            Console.WriteLine("Товаров нет.");
            return;
        }

        foreach (var (name, info) in products)
            Console.WriteLine($"{name} | цена: {info.price} | кол-во: {info.count}");
    }

    private static void InsertCoin(List<Coin> insertedCoins)
    {
        Console.Write("Номинал: ");
        if (!int.TryParse(Console.ReadLine(), out var nominal) || nominal <= 0)
        {
            Console.WriteLine("Некорректный номинал.");
            return;
        }

        insertedCoins.Add(new Coin(nominal));
        Console.WriteLine("Монета добавлена.");
    }

    private static void ShowInserted(List<Coin> insertedCoins)
    {
        var sum = insertedCoins.Sum(c => c.Nominal);
        Console.WriteLine($"Внесено: {sum}");
    }

    private static void Buy(VendingMachine vm, List<Coin> insertedCoins)
    {
        if (insertedCoins.Count == 0)
        {
            Console.WriteLine("Нет внесённых монет.");
            return;
        }

        Console.Write("Название товара: ");
        var name = Console.ReadLine();

        var (product, change) = vm.Buy(name!, insertedCoins);

        Console.WriteLine($"Выдано: {product.Name}");

        if (change.Count > 0)
            Console.WriteLine("Сдача: " + string.Join(", ", change.Select(c => c.Nominal)));

        insertedCoins.Clear();
    }

    private static void Cancel(List<Coin> insertedCoins)
    {
        if (insertedCoins.Count == 0)
        {
            Console.WriteLine("Нечего возвращать.");
            return;
        }

        Console.WriteLine("Возврат: " +
            string.Join(", ", insertedCoins.Select(c => c.Nominal)));

        insertedCoins.Clear();
    }
}
