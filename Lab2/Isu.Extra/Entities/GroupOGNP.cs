using Isu.Models;

namespace Isu.Entities;
public class GroupOGNP
{
    private List<Student> students;
    private string _teacher;
    public GroupOGNP(string groupName, string teacher, int classroom, Timetable timetable)
    {
        GroupName = groupName;
        students = new List<Student>();
        _teacher = teacher;
        Classroom = classroom;
        Timetable = timetable;
    }

    public string Teacher => _teacher;
    public List<Student> Students => students;
    public string GroupName { get; set; }
    public int MaxAmountOfStudents { get; } = 30;
    public Timetable Timetable { get; set; }
    public int Classroom { get; set; }
}
