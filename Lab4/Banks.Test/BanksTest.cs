using Banks.Entities;
using Banks.Models;
using Banks.Services;
using Xunit;

namespace Banks.Test;

public class BanksTest
{
    [Fact]
    public void AddBankAndCreateTwoCreditAccounts_BankAndTwoCreditAccountsAreCreated()
    {
        ICentralBank centralBank = new CentralBank();
        Bank sber = centralBank.AddBank("Sberbank", 100000000, 4, 50000, 100000, 3, 4, 5, 1000000, 3);
        var client1 = new Client("Boris");
        client1.Address = "Pushkina 11";
        client1.PassportID = "1312";
        sber.CreateCreditAccount(client1, 100000);
        var client2 = new Client("Maksim");
        client2.Address = "Pushkina 1";
        client2.PassportID = "1357";
        sber.CreateCreditAccount(client2, 100000);
        Assert.Equal(2, sber.CreditAccounts.Count());
    }

    [Fact]
    public void CreateDebitAccountAndGetCashback_DebitAccountIsCreatedAndCashbackIsGot()
    {
        ICentralBank centralBank = new CentralBank();
        Bank sber = centralBank.AddBank("Sberbank", 100000000, 4, 50000, 100000, 3, 4, 5, 1000000, 3);
        var client1 = new Client("Boris");
        client1.Address = "Pushkina 11";
        client1.PassportID = "1312";
        IAccount account = sber.CreateDebitAccount(client1, 100000);
        account.DayChange(30);
        Assert.Equal(138677, decimal.Truncate(account.Balance));
    }
}