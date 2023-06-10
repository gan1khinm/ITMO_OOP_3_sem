using Services;
using Shops.Entities;
using Xunit;

namespace Shops.Test;
public class ShopsServiceTest
{
    private readonly ShopService service;

    public ShopsServiceTest()
    {
        service = new ShopService();
    }

    [Fact]

    public void SupplyProductsToTheShop_ProductsAreInTHeShop()
    {
        Shop shop = service.AddShop("Magnit", "Lomonosova 5");
        var listOfProducts = new Dictionary<Product, int>();
        Product bread = service.RegisterProduct("Bread");
        Product snikers = service.RegisterProduct("Snikers");
        listOfProducts.Add(bread, 30);
        listOfProducts.Add(snikers, 45);
        service.ProductsSupply(shop, listOfProducts);

        Assert.Equal(listOfProducts, shop.Products);
    }

    [Fact]

    public void ChangeProductPrice_PriceIsChanged()
    {
        Shop shop = service.AddShop("Fasol", "Moskovskaya 31");
        Product mars = service.RegisterProduct("Mars");
        var listOfProducts = new Dictionary<Product, int>();
        listOfProducts.Add(mars, 37);
        service.ProductsSupply(shop, listOfProducts);
        service.ChangePrice(shop, mars, 31);

        Assert.Equal(31, shop.Products[mars]);
    }

    [Fact]

    public void FindShopWithCheapestPrice_ShopIsFound()
    {
        Shop shop1 = service.AddShop("Magnit", "Ivanova 4");
        Shop shop2 = service.AddShop("Spar", "Ivanova 8");
        Shop shop3 = service.AddShop("Metro", "Ivanova 7");
        Product bread = service.RegisterProduct("Bread");
        Product snikers = service.RegisterProduct("Snikers");
        Product banana = service.RegisterProduct("Banana");

        var listOfProducts = new List<Product>();
        listOfProducts.Add(bread);
        listOfProducts.Add(snikers);
        listOfProducts.Add(banana);

        var firstShopList = new Dictionary<Product, int>();
        firstShopList.Add(bread, 30);
        firstShopList.Add(snikers, 20);
        firstShopList.Add(banana, 10);
        service.ProductsSupply(shop1, firstShopList);

        var secondShopList = new Dictionary<Product, int>();
        secondShopList.Add(bread, 40);
        secondShopList.Add(snikers, 30);
        secondShopList.Add(banana, 10);
        service.ProductsSupply(shop2, secondShopList);

        var therdShopList = new Dictionary<Product, int>();
        therdShopList.Add(bread, 20);
        service.ProductsSupply(shop3, therdShopList);

        service.FindShopWithCheapestPrice(listOfProducts);

        Assert.Equal(shop1, service.FindShopWithCheapestPrice(listOfProducts));
    }

    [Fact]

    public void BuyProducts_ProductsAreBought()
    {
        Shop shop = service.AddShop("Magnit", "Lomonosova 5");
        var listOfProducts = new Dictionary<Product, int>();
        Product bread = service.RegisterProduct("Bread");
        Product snikers = service.RegisterProduct("Snikers");
        Product banana = service.RegisterProduct("Banana");
        listOfProducts.Add(bread, 30);
        listOfProducts.Add(snikers, 45);
        listOfProducts.Add(banana, 15);
        service.ProductsSupply(shop, listOfProducts);
        var person = new Customer("Boris", 150);
        var products = new List<Product>();
        products.Add(bread);
        products.Add(snikers);
        products.Add(banana);
        int moneyBefore = shop.Money;
        int amountOfProductsBefore = shop.Products.Count;
        Order order = service.Purchase(person, shop, products);

        var rightOrder = new Order(shop.Money, products);

        Assert.Equal(rightOrder.Price, order.Price);
        Assert.Equal(rightOrder.ListOfProducts, order.ListOfProducts);
        Assert.Equal(rightOrder.Date, order.Date);
        Assert.Equal(90, shop.Money - moneyBefore);
        Assert.Equal(60, person.Money);
        Assert.NotEqual(shop.Products.Count, amountOfProductsBefore);
    }
}
