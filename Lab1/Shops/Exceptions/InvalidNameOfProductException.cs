namespace Shops.Exceptions
{
    public class InvalidNameOfProductException : Exception
    {
        public InvalidNameOfProductException(string nameOfGood)
        {
            NameOfGood = nameOfGood;
        }

        public string NameOfGood { get; set; }
    }
}