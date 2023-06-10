namespace Banks.Exceptions
{
    public class InvalidAmountOfMoneyException : Exception
    {
        public InvalidAmountOfMoneyException(decimal amountOfMoney)
        {
            AmountOfMoney = amountOfMoney;
        }

        public decimal AmountOfMoney { get; set; }
    }
}