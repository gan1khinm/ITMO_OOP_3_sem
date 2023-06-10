namespace Isu.Models;

public class Timetable
{
    private List<Lesson> _timetable;
    public Timetable()
    {
        _timetable = new List<Lesson>();
    }

    public List<Lesson> GetTimetable => _timetable;

    public static implicit operator Timetable?(List<Lesson>? v)
    {
        throw new NotImplementedException();
    }
}
