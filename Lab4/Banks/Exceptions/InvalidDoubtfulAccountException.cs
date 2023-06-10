namespace Banks.Exceptions
{
    public class InvalidDoubtfulAccountException : Exception
    {
        public InvalidDoubtfulAccountException(bool isDoubtful)
        {
            IsDoubtful = isDoubtful;
        }

        public bool IsDoubtful { get; set; }
    }
}