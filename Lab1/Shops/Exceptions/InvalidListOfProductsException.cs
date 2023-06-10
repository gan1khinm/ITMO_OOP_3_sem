using Shops.Entities;

namespace Shops.Exceptions
{
    public class InvalidListOfProductsException : Exception
    {
        public InvalidListOfProductsException(List<Product> products)
        {
            Products = products;
        }

        public List<Product> Products { get; set; }
    }
}