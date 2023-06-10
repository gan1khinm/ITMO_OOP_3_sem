using Shops;
using Shops.Entities;

namespace Services;

public interface IShopService
{
    Shop AddShop(string shopName, string adress);

    Dictionary<Product, int> ProductsSupply(Shop shop, Dictionary<Product, int> products);

    Order Purchase(Customer costomer, Shop shop, List<Product> listOfProducts);

    int ChangePrice(Shop shop, Product product, int newPrice);

    Product RegisterProduct(string nameOfProduct);

    Shop? FindShopWithCheapestPrice(List<Product> listOfProducts);
}
