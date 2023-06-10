namespace Isu.Models;

public class Lesson
{
    public Lesson(DayOfWeek dayOfWeek, int numberOfLesson)
    {
        DayOfWeek = dayOfWeek;
        NumberOfLesson = numberOfLesson;
    }

    public DayOfWeek DayOfWeek { get; set; }
    public int NumberOfLesson { get; set; }
}
