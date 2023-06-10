namespace Isu.Exceptions
{
    public class InvalidAmountOfStudentsException : Exception
    {
        public InvalidAmountOfStudentsException(int amountOfStudents)
        {
            AmountOfStudents = amountOfStudents;
        }

        public int AmountOfStudents { get; set; }
    }
}
