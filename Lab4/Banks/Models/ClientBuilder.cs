namespace Banks.Models;

public class ClientBuilder
{
    public Client SetNameOfClient(string name)
    {
        var client = new Client(name);
        return client;
    }

    public void SetAddressOfClient(Client client, string address)
    {
        client.Address = address;
    }

    public void SetIdOfClient(Client client, string passportId)
    {
        client.PassportID = passportId;
    }
}
