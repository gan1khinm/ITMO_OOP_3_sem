using System.Text.RegularExpressions;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Entities;

public class Group
{
    private List<Student> _students;
    private string _megafaculty;
    public Group(string groupName, CourseNumber courseNumber, Timetable timetable, string megafaculty)
    {
        if (!TryParse(groupName))
        {
            throw new InvalidGroupNameException(groupName);
        }

        _megafaculty = megafaculty;
        GroupName = groupName;
        CourseNumber = courseNumber;
        _students = new List<Student>();
        Timetable = timetable;
    }

    public string Megafaculty => _megafaculty;
    public Timetable Timetable { get; set; }
    public CourseNumber CourseNumber { get; set; }
    public string GroupName { get; set; }
    public int MaxAmountOfStudents { get; } = 30;
    public List<Student> Students
    {
        get
        {
            return _students;
        }
    }

    public static bool TryParse(string groupName)
    {
        var regex = new Regex(@"^[A-Z]3[1,2,3,4]\d{2,3}");
        MatchCollection matches = regex.Matches(groupName);
        if (matches.Count == 0)
        {
            return false;
        }

        return true;
    }
}
