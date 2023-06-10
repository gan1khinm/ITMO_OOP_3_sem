namespace Backups;

public class Leaf : Component
{
    private string _name;
    private string _path;
    public Leaf(string name, string path)
        : base(name, path)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        _name = name;
        _path = path;
    }
}