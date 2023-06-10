namespace Banks.Models;

public class Client
{
    private string _name;
    private string? _address;
    private string? _passportId;
    public Client(string name)
    {
        _name = name;
    }

    public string Name => _name;
    public string? Address { get { return _address; } set { _address = value; } }
    public string? PassportID { get { return _passportId; } set { _passportId = value; } }
}