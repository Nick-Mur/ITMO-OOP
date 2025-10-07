using System;
using System.Collections.Generic;
using System.Globalization;
using VendingMachine.Domain.Entities;
using VendingMachine.Infrastructure.Services;

var slots = new List<InventorySlot>
{
    new(new Product("A1", "Газированная вода", 1.50m), 5),
    new(new Product("B2", "Шоколад", 2.00m), 4),
    new(new Product("C3", "Чипсы", 1.80m), 6)
};

var machineWallet = new Wallet(new List<CoinBase>
{
    new Coin(0.50m),
    new Coin(0.50m),
    new Coin(1.00m),
    new Coin(1.00m),
    new Coin(2.00m)
});

var inventoryService = new InMemoryInventoryService(slots);
var paymentService = new InMemoryPaymentService(machineWallet);
var vendingMachine = new VendingMachineService(inventoryService, paymentService);

var isRunning = true;

while (isRunning)
{
    Console.WriteLine();
    Console.WriteLine("=== Торговый автомат ===");
    Console.WriteLine("1. Показать товары");
    Console.WriteLine("2. Внести монету");
    Console.WriteLine("3. Купить товар");
    Console.WriteLine("4. Отменить операцию");
    Console.WriteLine("5. Админ: Пополнить товар");
    Console.WriteLine("6. Админ: Забрать средства");
    Console.WriteLine("7. Выход");
    Console.Write("Выберите пункт меню: ");

    var choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            DisplayProducts();
            break;
        case "2":
            InsertCoin();
            break;
        case "3":
            PurchaseProduct();
            break;
        case "4":
            CancelTransaction();
            break;
        case "5":
            RestockProduct();
            break;
        case "6":
            CollectFunds();
            break;
        case "7":
            isRunning = false;
            break;
        default:
            Console.WriteLine("Неизвестный пункт меню.");
            break;
    }
}

void DisplayProducts()
{
    var products = vendingMachine.GetAvailableProducts();
    Console.WriteLine("Товары:");
    foreach (var slot in products)
    {
        Console.WriteLine(
            $"{slot.Product.Id}: {slot.Product.Name} - {slot.Product.Price.ToString("C", CultureInfo.CurrentCulture)} (Кол-во: {slot.Quantity})");
    }
}

void InsertCoin()
{
    Console.Write("Введите номинал монеты: ");
    if (decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out var value) && value > 0)
    {
        vendingMachine.InsertCoin(new Coin(value));
        Console.WriteLine($"Внесено {value.ToString("C", CultureInfo.CurrentCulture)}");
    }
    else
    {
        Console.WriteLine("Некорректный номинал монеты.");
    }
}

void PurchaseProduct()
{
    Console.Write("Введите идентификатор товара: ");
    var productId = Console.ReadLine() ?? string.Empty;
    var result = vendingMachine.Purchase(productId);
    Console.WriteLine(result.Message);

    if (result.IsSuccessful)
    {
        if (result.DispensedProduct is not null)
        {
            Console.WriteLine($"Выдано: {result.DispensedProduct.Name}");
        }

        if (result.Change.Count > 0)
        {
            Console.WriteLine("Возвращена сдача:");
            foreach (var coin in result.Change)
            {
                Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
            }
        }
    }
    else
    {
        if (result.Change.Count > 0)
        {
            Console.WriteLine("Возвращены монеты:");
            foreach (var coin in result.Change)
            {
                Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
            }
        }
    }
}

void CancelTransaction()
{
    var refund = vendingMachine.Cancel();
    if (refund.Count == 0)
    {
        Console.WriteLine("Нет монет для возврата.");
        return;
    }

    Console.WriteLine("Операция отменена. Возвращённые монеты:");
    foreach (var coin in refund)
    {
        Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
    }
}

void RestockProduct()
{
    Console.Write("Введите идентификатор товара: ");
    var id = Console.ReadLine() ?? string.Empty;
    Console.Write("Введите название товара: ");
    var name = Console.ReadLine() ?? string.Empty;
    Console.Write("Введите цену: ");
    if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Number, CultureInfo.InvariantCulture, out var price))
    {
        Console.WriteLine("Некорректная цена.");
        return;
    }

    Console.Write("Введите количество: ");
    if (!int.TryParse(Console.ReadLine(), out var quantity))
    {
        Console.WriteLine("Некорректное количество.");
        return;
    }

    vendingMachine.Restock(new Product(id, name, price), quantity);
    Console.WriteLine("Товар пополнен.");
}

void CollectFunds()
{
    var coins = vendingMachine.CollectFunds();
    if (coins.Count == 0)
    {
        Console.WriteLine("Нет средств для сбора.");
        return;
    }

    Console.WriteLine("Собранные монеты:");
    foreach (var coin in coins)
    {
        Console.WriteLine(coin.Value.ToString("C", CultureInfo.CurrentCulture));
    }
}
