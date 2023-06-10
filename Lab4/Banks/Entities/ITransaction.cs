namespace Banks.Entities;

public interface ITransaction
{
    IAccount AccountTo { get;  }
    decimal Money { get; }
    void Execute();
    void Rollback();
}
