using Isu.Models;

namespace Isu.Entities;
public class Flow
{
    private string _teacher;
    private List<GroupOGNP> groups;
    private Timetable _timetable;
    private string _name;
    public Flow(string teacher, int classroom, string name, Timetable timetable)
    {
        _teacher = teacher;
        Classroom = classroom;
        groups = new List<GroupOGNP>();
        _timetable = timetable;
        _name = name;
    }

    public Timetable Timetable => _timetable;
    public string Teacher => _teacher;
    public string Name => _name;
    public List<GroupOGNP> Groups => groups;
    public int Classroom { get; set; }
}
