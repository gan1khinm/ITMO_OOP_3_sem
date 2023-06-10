namespace Backups;

public abstract class Component
{
    private string _name;
    private string _path;
    public Component(string name, string path)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (path is null)
        {
            throw new ArgumentNullException(nameof(path));
        }

        _path = path;
        _name = name;
    }

    public string Name => _name;
    public string Path => _path;
}