using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class DebitAccount : IAccount
{
    private decimal balance;
    private long _id;
    private Client _owner;
    private bool isdoubtful = false;
    private decimal _percent;
    private DateTime dateTimeOfBegining;
    private DateOnly dateOnlyOfBegining;
    private int allDaysMonth;
    private DateOnly endOfMounth;
    private decimal cashback;
    public DebitAccount(long id, Client owner, decimal amountOfMoney, decimal percent)
    {
        balance = amountOfMoney;
        _id = id;
        _owner = owner;
        _percent = percent;
        cashback = 0;
        dateTimeOfBegining = DateTime.Now;
        dateOnlyOfBegining = new DateOnly(dateTimeOfBegining.Year, dateTimeOfBegining.Month, dateTimeOfBegining.Day);
        allDaysMonth = DateTime.DaysInMonth(dateTimeOfBegining.Year, dateTimeOfBegining.Month);
        endOfMounth = new DateOnly(dateOnlyOfBegining.Year, dateOnlyOfBegining.Month, allDaysMonth);
    }

    public decimal Percent { get { return _percent; } set { _percent = value; } }
    public Client Owner => _owner;

    public long Id => _id;
    public decimal Balance { get { return balance; } set { balance = value; } }

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

        if (balance - amountOfMoney < 0)
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

        if (balance - amountOfMoney < 0)
        {
            throw new InvalidAmountOfMoneyException(balance - amountOfMoney);
        }

        balance -= amountOfMoney;
    }

    public void DayChange(int amountOfDays)
    {
        for (int i = 0; i < amountOfDays; i++)
        {
            decimal dayPercent = _percent / 365;
            cashback += balance * dayPercent;
            dateOnlyOfBegining.AddDays(1);
            balance += balance * dayPercent;
            if (dateOnlyOfBegining == endOfMounth)
            {
                balance += cashback;
                cashback = 0;
                allDaysMonth = DateTime.DaysInMonth(dateOnlyOfBegining.Year, dateOnlyOfBegining.Month + 1);
                endOfMounth = new DateOnly(dateOnlyOfBegining.Year, dateOnlyOfBegining.Month + 1, allDaysMonth);
            }
        }
    }
}