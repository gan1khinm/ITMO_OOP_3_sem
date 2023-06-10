namespace Banks.Exceptions
{
    public class InvalidDateException : Exception
    {
        public InvalidDateException(DateTime date)
        {
            Date = date;
        }

        public DateTime Date { get; set; }
    }
}