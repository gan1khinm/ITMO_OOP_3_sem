using Shops;
using Shops.Entities;
using Shops.Exceptions;

namespace Services;
public class ShopService : IShopService
{
    private readonly Dictionary<string, Shop> shops;
    private int idCounter;
    private List<Product> registeredProducts;
    public ShopService()
    {
        idCounter = 0;
        registeredProducts = new List<Product>();
        shops = new Dictionary<string, Shop>();
    }

    public Shop AddShop(string shopName, string adress)
    {
        if (shopName is null)
        {
            throw new ArgumentNullException(nameof(shopName));
        }

        if (adress is null)
        {
            throw new ArgumentNullException(nameof(adress));
        }

        idCounter++;
        var shop = new Shop(shopName, adress, idCounter);
        shops.Add(shopName, shop);

        return shop;
    }

    public Dictionary<Product, int> ProductsSupply(Shop shop, Dictionary<Product, int> products)
    {
        if (shop is null)
        {
            throw new ArgumentNullException(nameof(shop));
        }

        if (products is null)
        {
            throw new ArgumentNullException(nameof(products));
        }

        foreach (Product product in products.Keys)
        {
             if (!registeredProducts.Contains(product))
             {
                throw new InvalidNameOfProductException(product.Name);
             }

             shop.Products.Add(product, products[product]);
        }

        return shop.Products;
    }

    public int ChangePrice(Shop shop, Product product, int newPrice)
    {
        if (shop is null)
        {
            throw new ArgumentNullException(nameof(shop));
        }

        if (product is null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        shop.Products[product] = newPrice;
        return newPrice;
    }

    public Order Purchase(Customer costomer, Shop shop, List<Product> listOfProducts)
    {
        if (costomer is null)
        {
            throw new ArgumentNullException(nameof(costomer));
        }

        if (shop is null)
        {
            throw new ArgumentNullException(nameof(shop));
        }

        if (listOfProducts is null)
        {
            throw new ArgumentNullException(nameof(listOfProducts));
        }

        int price = 0;

        foreach (Product product in listOfProducts)
        {
            if (!shop.Products.Keys.Contains(product))
            {
                throw new InvalidListOfProductsException(listOfProducts);
            }

            if (costomer.Money < shop.Products[product])
            {
                throw new InvalidAmountOfMoneyException(costomer.Money);
            }

            costomer.Money -= shop.Products[product];
            shop.Money += shop.Products[product];
            price += shop.Products[product];
            shop.Products.Remove(product);
        }

        var order = new Order(price, listOfProducts);

        return order;
    }

    public Product RegisterProduct(string nameOfProduct)
    {
        if (nameOfProduct is null)
        {
            throw new ArgumentNullException(nameof(nameOfProduct));
        }

        var product = new Product(nameOfProduct);
        registeredProducts.Add(product);

        return product;
    }

    public Shop? FindShopWithCheapestPrice(List<Product> listOfProducts)
    {
        if (listOfProducts is null)
        {
            throw new ArgumentNullException(nameof(listOfProducts));
        }

        int fl = int.MaxValue;
        string nameOfRightShop = string.Empty;

        foreach (KeyValuePair<string, Shop> shop in shops)
        {
            int priceOfList = 0;
            int listExist = 1;
            foreach (Product product in listOfProducts)
            {
                if (shop.Value.Products.Keys.Contains(product))
                {
                    priceOfList += shop.Value.Products[product];
                }
                else
                {
                    listExist = 0;
                    break;
                }
            }

            if (listExist == 0)
            {
                continue;
            }

            if (priceOfList < fl)
            {
                fl = priceOfList;
                nameOfRightShop = shop.Value.ShopName;
            }
        }

        if (nameOfRightShop == string.Empty)
        {
            throw new InvalidListOfProductsException(listOfProducts);
        }

        return shops[nameOfRightShop];
    }
}
