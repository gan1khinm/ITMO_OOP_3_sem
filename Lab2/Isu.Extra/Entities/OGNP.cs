using Isu.Models;

namespace Isu.Entities;
public class OGNP
{
    private string _name;
    private string _megafaculty;
    private List<Flow> flows;
    public OGNP(string name, string megafaculty, CourseNumber courseNumber)
    {
        _name = name;
        _megafaculty = megafaculty;
        flows = new List<Flow>();
        CourseNumber = courseNumber;
    }

    public List<Flow> Flows => flows;
    public CourseNumber CourseNumber { get; set; }
    public string Name => _name;
    public string MegaFaculty => _megafaculty;
}