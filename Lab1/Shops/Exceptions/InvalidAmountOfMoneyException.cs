namespace Shops.Exceptions
{
    public class InvalidAmountOfMoneyException : Exception
    {
        public InvalidAmountOfMoneyException(int money)
        {
            Money = money;
        }

        public int Money { get; set; }
    }
}