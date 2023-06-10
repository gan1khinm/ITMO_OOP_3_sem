using Banks.Models;

namespace Banks.Entities;
public interface IAccount
{
    decimal Balance { get; }
    long Id { get; }
    Client Owner { get; }
    void WithdrawMoney(decimal amountOfMoney);
    void TopUpAnAccount(decimal amountOfMoney);
    void MoneyTransfer(IAccount account, decimal amountOfMoney);
    void DayChange(int amountOfDays);
}