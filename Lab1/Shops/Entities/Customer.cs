using Shops.Exceptions;
namespace Shops;

public class Customer
{
    public Customer(string name, int moneyBefore)
    {
        if (moneyBefore < 0)
        {
            throw new InvalidAmountOfMoneyException(moneyBefore);
        }

        if (name is null)
        {
            throw new ArgumentNullException(name);
        }

        Name = name;
        Money = moneyBefore;
    }

    public int Money { get; set; }
    public string Name { get; set; }
}
