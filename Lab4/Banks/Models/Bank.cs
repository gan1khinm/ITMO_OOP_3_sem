using Banks.Entities;

namespace Banks.Models;

public class Bank
{
    private List<Client> clients;
    private List<IAccount> creditAccounts;
    private List<IAccount> debitAccounts;
    private List<IAccount> depositAccounts;
    private string _name;
    private decimal _money;
    private int id;
    private decimal _yearPercent;
    private decimal _firstDepBorder;
    private decimal _secondDepBorder;
    private decimal _firstDepPercent;
    private decimal _secondDepPercent;
    private decimal _therdDepPercent;
    private decimal _creditLimit;
    private decimal _creditCommission;
    public Bank(string name, decimal money, decimal yearPercent, decimal firstDepBorder, decimal secondDepBorder, decimal firstDepPercent, decimal secondDepPercent, decimal therdDepPercent, decimal creditLimit, decimal creditCommission)
    {
        if (name is null)
        {
            throw new ArgumentNullException(name);
        }

        clients = new List<Client>();
        creditAccounts = new List<IAccount>();
        debitAccounts = new List<IAccount>();
        depositAccounts = new List<IAccount>();
        _name = name;
        _money = money;
        id = 0;
        _yearPercent = yearPercent;
        _firstDepBorder = firstDepBorder;
        _secondDepBorder = secondDepBorder;
        _firstDepPercent = firstDepPercent;
        _secondDepPercent = secondDepPercent;
        _therdDepPercent = therdDepPercent;
        _creditLimit = creditLimit;
        _creditCommission = creditCommission;
    }

    public decimal Money { get { return _money; } set { } }
    public string Name => _name;
    public List<Client> Clients => clients;
    public List<IAccount> CreditAccounts => creditAccounts;
    public List<IAccount> DebitAccounts => debitAccounts;
    public List<IAccount> DepositAccounts => depositAccounts;
    public void TransactionCancellation(ITransaction transaction)
    {
        if (transaction is null)
        {
            throw new ArgumentNullException(nameof(transaction));
        }

        transaction.Rollback();
    }

    public CreditAccount CreateCreditAccount(Client owner, decimal amountOfMoney)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        id++;
        var account = new CreditAccount(id, owner, amountOfMoney, _creditLimit, _creditCommission);
        creditAccounts.Add(account);
        return account;
    }

    public DebitAccount CreateDebitAccount(Client owner, decimal amountOfMoney)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        id++;
        var account = new DebitAccount(id, owner, amountOfMoney, _yearPercent);
        debitAccounts.Add(account);
        return account;
    }

    public DepositAccount CreateDepositAccount(Client owner, decimal amountOfMoney, DateTime endingOfTerm)
    {
        if (owner == null)
        {
            throw new ArgumentNullException(nameof(owner));
        }

        decimal percent;

        if (amountOfMoney <= _firstDepBorder)
        {
            percent = _firstDepPercent;
        }
        else if (amountOfMoney > _firstDepBorder && amountOfMoney < _secondDepBorder)
        {
            percent = _secondDepPercent;
        }
        else
        {
            percent = _therdDepPercent;
        }

        id++;
        var account = new DepositAccount(id, owner, amountOfMoney, percent, endingOfTerm);
        depositAccounts.Add(account);
        return account;
    }

    public void ChangeYearPercemt(decimal newYearPercent)
    {
        _yearPercent = newYearPercent;
        foreach (DebitAccount account in debitAccounts)
        {
            account.Percent = newYearPercent;
        }
    }

    public void ChangeDepositPercents(decimal firstPercent, decimal secondPercent, decimal therdPercent)
    {
        foreach (DepositAccount account in depositAccounts)
        {
            if (account.StartBalance <= _firstDepBorder)
            {
                account.Percent = firstPercent;
            }
            else if (account.StartBalance > _firstDepBorder && account.StartBalance < _secondDepBorder)
            {
                account.Percent = secondPercent;
            }
            else
            {
                account.Percent = therdPercent;
            }
        }
    }
}
