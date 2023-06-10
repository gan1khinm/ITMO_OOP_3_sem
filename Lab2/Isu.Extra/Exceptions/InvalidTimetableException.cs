using Isu.Models;

namespace Isu.Exceptions
{
    public class InvalidTimetableException : Exception
    {
        public InvalidTimetableException(List<Lesson> lessons)
        {
            Timetable = lessons;
        }

        public Timetable? Timetable { get; set; }
    }
}
