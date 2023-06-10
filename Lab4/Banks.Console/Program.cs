using Banks.Entities;
using Banks.Models;
using Banks.Services;

var centralBank = new CentralBank();
Bank sberbank = centralBank.AddBank("Sberbank", 100000000, 4, 50000, 100000, 3, 4, 5, 1000000, 3);
Bank tinkoff = centralBank.AddBank("Tinkoff", 100030000, 3, 60000, 110000, 3, 4, 6, 1000000, 3);
Bank alfa = centralBank.AddBank("Alfa-Bank", 100035400, 3, 65000, 100000, 2, 3, 4, 900000, 4);

Console.WriteLine("Please register your profile: ");
Console.WriteLine("Enter your name: ");

var builder = new ClientBuilder();
Client client = builder.SetNameOfClient(Console.ReadLine() !);

Console.WriteLine("Enter your address if you need: ");
builder.SetAddressOfClient(client, Console.ReadLine() !);
Console.WriteLine("Enter your passport id if you need: ");
builder.SetIdOfClient(client, Console.ReadLine() !);

Console.WriteLine("Choose a bank you need: ");
Console.WriteLine("\t" + sberbank.Name);
Console.WriteLine("\t" + tinkoff.Name);
Console.WriteLine("\t" + alfa.Name);
IAccount account = new DebitAccount(0, client, 0, 0);
switch (Console.ReadLine())
{
    case "Sberbank":
        Console.WriteLine("Choose a type of Account: ");
        Console.WriteLine("\tCredit - credit account");
        Console.WriteLine("\tDebit - debit account");
        Console.WriteLine("\tDeposit - deposit account");
        switch (Console.ReadLine())
        {
            case "Credit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney1 = decimal.Parse(Console.ReadLine() !);
                account = sberbank.CreateCreditAccount(client, amountOfMoney1);
                break;
            case "Debit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney2 = decimal.Parse(Console.ReadLine() !);
                account = sberbank.CreateDebitAccount(client, amountOfMoney2);
                break;
            case "Deposit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney3 = decimal.Parse(Console.ReadLine() !);
                Console.WriteLine("Enter amount of mounths: ");
                int amountOfMounths = int.Parse(Console.ReadLine() !);
                DateTime endOfTerm = DateTime.Now.AddMonths(amountOfMounths);
                account = sberbank.CreateDepositAccount(client, amountOfMoney3, endOfTerm);
                break;
        }

        break;
    case "Tinkoff":
        Console.WriteLine("Choose a type of Account: ");
        Console.WriteLine("\tCredit - credit account");
        Console.WriteLine("\tDebit - debit account");
        Console.WriteLine("\tDeposit - deposit account");
        switch (Console.ReadLine())
        {
            case "Credit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney1 = decimal.Parse(Console.ReadLine() !);
                account = tinkoff.CreateCreditAccount(client, amountOfMoney1);
                break;
            case "Debit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney2 = decimal.Parse(Console.ReadLine() !);
                account = tinkoff.CreateDebitAccount(client, amountOfMoney2);
                break;
            case "Deposit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney3 = decimal.Parse(Console.ReadLine() !);
                Console.WriteLine("Enter amount of mounths: ");
                int amountOfMounths = int.Parse(Console.ReadLine() !);
                DateTime endOfTerm = DateTime.Now.AddMonths(amountOfMounths);
                account = tinkoff.CreateDepositAccount(client, amountOfMoney3, endOfTerm);
                break;
        }

        break;
    case "Alfa-Bank":
        Console.WriteLine("Choose a type of Account: ");
        Console.WriteLine("\tCredit - credit account");
        Console.WriteLine("\tDebit - debit account");
        Console.WriteLine("\tDeposit - deposit account");
        switch (Console.ReadLine())
        {
            case "Credit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney1 = decimal.Parse(Console.ReadLine() !);
                account = alfa.CreateCreditAccount(client, amountOfMoney1);
                break;
            case "Debit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney2 = decimal.Parse(Console.ReadLine() !);
                account = alfa.CreateDebitAccount(client, amountOfMoney2);
                break;
            case "Deposit":
                Console.WriteLine("Enter an amount of money you want to put up: ");
                decimal amountOfMoney3 = decimal.Parse(Console.ReadLine() !);
                Console.WriteLine("Enter amount of mounths: ");
                int amountOfMounths = int.Parse(Console.ReadLine() !);
                DateTime endOfTerm = DateTime.Now.AddMonths(amountOfMounths);
                account = alfa.CreateDepositAccount(client, amountOfMoney3, endOfTerm);
                break;
        }

        break;
}

Console.WriteLine("Choose the operation you need: ");
Console.WriteLine("\tWithdraw - withdraw money");
Console.WriteLine("\tTop up - top up money");
Console.WriteLine("\tTransfer - transfer money to another account");

switch (Console.ReadLine())
{
    case "Withdraw":
        Console.WriteLine("Enter the amount of money you need to withdraw: ");
        account.WithdrawMoney(decimal.Parse(Console.ReadLine() !));
        break;
    case "Top up":
        Console.WriteLine("Enter the amount of money you need to top up: ");
        account.TopUpAnAccount(decimal.Parse(Console.ReadLine() !));
        break;
    case "Transfer":
        Console.WriteLine("Enter the amount of money you need to transfer: ");
        account.MoneyTransfer(account, decimal.Parse(Console.ReadLine() !));
        break;
}
