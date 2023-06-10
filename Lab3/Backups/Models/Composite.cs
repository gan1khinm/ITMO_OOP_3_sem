namespace Backups;

public class Composite : Component
{
    private List<Component> children = new List<Component>();
    private string _name;
    private string _path;
    public Composite(string name, string path)
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

    public List<Component> Children => children;
    public void Add(Component component)
    {
        if (component is null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        children.Add(component);
    }

    public void Remove(Component component)
    {
        if (component is null)
        {
            throw new ArgumentNullException(nameof(component));
        }

        children.Remove(component);
    }
}