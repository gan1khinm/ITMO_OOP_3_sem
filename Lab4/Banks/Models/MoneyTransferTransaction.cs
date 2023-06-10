using Banks.Entities;
using Banks.Exceptions;

namespace Banks.Models;

public class MoneyTransferTransaction : ITransaction
{
    private decimal _balanceFrom;
    private IAccount _accountTo;
    private decimal _money;
    private bool isTransaction;
    public MoneyTransferTransaction(decimal balanceFrom, IAccount accountTo, decimal money)
    {
        _balanceFrom = balanceFrom;
        _accountTo = accountTo;
        _money = money;
    }

    public decimal Money => _money;
    public decimal BalanceFrom { get { return _balanceFrom; } set { } }
    public IAccount AccountTo => _accountTo;
    public void Execute()
    {
        if (isTransaction)
        {
            throw new InvalideTransactionException(isTransaction);
        }

        _balanceFrom -= _money;
        _accountTo.TopUpAnAccount(_money);
        isTransaction = true;
    }

    public void Rollback()
    {
        if (isTransaction)
        {
            _balanceFrom += _money;
            AccountTo.WithdrawMoney(_money);
            isTransaction = false;
        }
    }
}
