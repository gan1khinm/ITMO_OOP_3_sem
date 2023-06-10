using Shops.Entities;

namespace Shops;
public class Shop
{
    private int id;
    private string address;
    private Dictionary<Product, int> products;
    public Shop(string shopName, string address_, int id_)
    {
        ShopName = shopName;
        address = address_;
        id = id_;
        products = new Dictionary<Product, int>();
        Money = 0;
    }

    public int Money { get; set; }
    public Dictionary<Product, int> Products
    {
        get
        {
            return products;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }
    }

    public string Address
    {
        get
        {
            return address;
        }
    }

    public string ShopName { get; set; }
}
