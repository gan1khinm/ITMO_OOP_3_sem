namespace Shops.Entities;

public class Order
{
    private DateTime date;
    private int price;
    private List<Product> listOfProducts;
    public Order(int price_, List<Product> listOfProducts_)
    {
        price = price_;
        listOfProducts = listOfProducts_;
        date = DateTime.Today;
    }

    public int Price { get { return price; } }
    public List<Product> ListOfProducts { get { return listOfProducts; } }
    public DateTime Date { get { return date; } }
}