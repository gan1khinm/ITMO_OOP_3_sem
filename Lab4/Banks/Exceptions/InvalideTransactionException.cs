namespace Banks.Exceptions
{
    public class InvalideTransactionException : Exception
    {
        public InvalideTransactionException(bool isTransaction)
        {
            IsTransaction = isTransaction;
        }

        public bool IsTransaction { get; set; }
    }
}