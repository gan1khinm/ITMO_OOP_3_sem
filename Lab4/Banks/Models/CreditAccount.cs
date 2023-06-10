using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class CreditAccount : IAccount
{
    private decimal balance;
    private long _id;
    private Client _owner;
    private bool isdoubtful = false;
    private decimal _creditLimit;
    private decimal _commission;
    private DateTime dateOfBegining;
    public CreditAccount(long id, Client owner, decimal amountOfMoney, decimal creditLimit, decimal commission)
    {
        if (owner is null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        dateOfBegining = DateTime.Now;
        balance = amountOfMoney;
        _id = id;
        _owner = owner;
        _creditLimit = creditLimit;
        _commission = commission;
    }

    public Client Owner => _owner;
    public long Id => _id;
    public decimal Balance => balance;
    public bool IsDoubtful()
    {
        if (_owner.PassportID == null || _owner.Address == null)
        {
            isdoubtful = true;
        }
        else
        {
            isdoubtful = false;
        }

        return isdoubtful;
    }

    public void MoneyTransfer(IAccount account, decimal amountOfMoney)
    {
        if (IsDoubtful())
        {
            throw new InvalidDoubtfulAccountException(isdoubtful);
        }

        if (balance - amountOfMoney < 0 && balance - amountOfMoney < -_creditLimit)
        {
            throw new InvalidAmountOfMoneyException(balance - amountOfMoney);
        }

        var moneyTransaction = new MoneyTransferTransaction(balance, account, amountOfMoney);
        moneyTransaction.Execute();
        balance = moneyTransaction.BalanceFrom;
    }

    public void TopUpAnAccount(decimal amountOfMoney)
    {
        balance += amountOfMoney;
    }

    public void WithdrawMoney(decimal amountOfMoney)
    {
        if (IsDoubtful())
        {
            throw new InvalidDoubtfulAccountException(isdoubtful);
        }

        if (balance - amountOfMoney < 0 && balance - amountOfMoney < -_creditLimit)
        {
            throw new InvalidAmountOfMoneyException(balance - amountOfMoney);
        }

        balance -= amountOfMoney;
    }

    public void DayChange(int amountOfDays)
    {
        for (int i = 0; i < amountOfDays; i++)
        {
            dateOfBegining.AddDays(1);
            if (balance < 0)
            {
                decimal debt = -balance * _commission;
                balance -= debt;
            }
        }
    }
}