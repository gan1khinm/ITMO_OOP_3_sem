using Banks.Entities;
using Banks.Exceptions;
using Banks.Models;

namespace Banks.Services;

public class CentralBank : ICentralBank
{
    private List<Bank> banks;
    public CentralBank()
    {
        banks = new List<Bank>();
    }

    public Bank AddBank(string name, decimal money, decimal yearPercent, decimal firstDepBorder, decimal secondDepBorder, decimal firstDepPercent, decimal secondDepPercent, decimal therdDepPercent, decimal creditLimit, decimal creditCommission)
    {
        var newBank = new Bank(name, money, yearPercent, firstDepBorder, secondDepBorder, firstDepPercent, secondDepPercent, therdDepPercent, creditLimit, creditCommission);
        banks.Add(newBank);
        return newBank;
    }

    public void TransferBetweenBanks(Bank bankFrom, Bank bankTo, decimal amountOfMoney)
    {
        if (bankFrom is null)
        {
            throw new ArgumentNullException(nameof(bankFrom));
        }

        if (bankTo is null)
        {
            throw new ArgumentNullException(nameof(bankTo));
        }

        if (bankFrom.Money - amountOfMoney < 0)
        {
            throw new InvalidAmountOfMoneyException(bankFrom.Money - amountOfMoney);
        }

        bankFrom.Money -= amountOfMoney;
        bankTo.Money += amountOfMoney;
    }
}