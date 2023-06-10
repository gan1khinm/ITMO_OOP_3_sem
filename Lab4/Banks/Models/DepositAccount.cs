using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class DepositAccount : IAccount
{
    private decimal balance;
    private long _id;
    private Client _owner;
    private bool isdoubtful = false;
    private DateTime _endingOfTerm;
    private DateTime dateOfBegining;
    private int allDaysMonth;
    private DateTime endOfMounth;
    private decimal _percent;
    private decimal startBalance;
    public DepositAccount(long id, Client owner, decimal amountOfMoney, decimal percent, DateTime endingOfTerm)
    {
        balance = amountOfMoney;
        startBalance = amountOfMoney;
        _id = id;
        _owner = owner;
        DayOfMounth = 1;
        Cashback = 0;
        _endingOfTerm = endingOfTerm;
        dateOfBegining = DateTime.Now;
        allDaysMonth = DateTime.DaysInMonth(dateOfBegining.Year, dateOfBegining.Month);
        endOfMounth = new DateTime(dateOfBegining.Year, dateOfBegining.Month, allDaysMonth);
        _percent = percent;
    }

    public Client Owner => _owner;
    public decimal StartBalance => startBalance;
    public decimal Percent { get { return _percent; } set { } }
    public decimal Cashback { get; set; }
    public int DayOfMounth { get; set; }
    public decimal Balance => balance;

    public long Id => _id;

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

        var today = default(DateTime);

        if (today < _endingOfTerm)
        {
            throw new InvalidDateException(today);
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

        var today = default(DateTime);

        if (today < _endingOfTerm)
        {
            throw new InvalidDateException(today);
        }

        balance -= amountOfMoney;
    }

    public void DayChange(int amountOfDays)
    {
        for (int i = 0; i < amountOfDays; i++)
        {
            decimal dayPercent = _percent / 365;
            Cashback += Balance * dayPercent;
            dateOfBegining.AddDays(1);

            if (dateOfBegining == endOfMounth)
            {
                balance += Cashback;
                Cashback = 0;
                DayOfMounth = 0;
                allDaysMonth = DateTime.DaysInMonth(dateOfBegining.Year, dateOfBegining.Month + 1);
                endOfMounth = new DateTime(dateOfBegining.Year, dateOfBegining.Month + 1, allDaysMonth);
            }
        }
    }
}