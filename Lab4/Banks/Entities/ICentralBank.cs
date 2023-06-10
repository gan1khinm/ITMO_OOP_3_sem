using Banks.Models;

namespace Banks.Entities;
public interface ICentralBank
{
    Bank AddBank(string name, decimal money, decimal yearPercent, decimal firstDepBorder, decimal secondDepBorder, decimal firstDepPercent, decimal secondDepPercent, decimal therdDepPercent, decimal creditLimit, decimal creditCommission);
    void TransferBetweenBanks(Bank bankFrom, Bank bankTo, decimal amountOfMoney);
}