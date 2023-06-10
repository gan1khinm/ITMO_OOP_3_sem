namespace Isu.Entities;

public class Student
{
    private int _id;
    public Student(int id, string name, Group group)
    {
        _id = id;
        Name = name;
        Group = group;
    }

    public string Name { get; set; }
    public Group Group { get; set; }
    public int Id
    {
        get
        {
            return _id;
        }
    }
}