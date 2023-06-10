using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuServiceTest
{
    private readonly IsuService service;

    public IsuServiceTest()
    {
        service = new IsuService();
    }

    [Fact]
    public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
    {
        var timetable = new Timetable();
        var lesson = new Lesson(DayOfWeek.Friday, 2);
        timetable.GetTimetable.Add(lesson);
        var group = new Group("M32091", CourseNumber.Second, timetable, "TINT");
        Student student = service.AddStudent(group, "Ivan");
        Assert.Equal(student, group.Students.Last());
    }

    [Fact]
    public void ReachMaxStudentPerGroup_ThrowException()
    {
        var timetable = new Timetable();
        var lesson = new Lesson(DayOfWeek.Friday, 2);
        timetable.GetTimetable.Add(lesson);
        var group = new Group("M32093", CourseNumber.Second, timetable, "TINT");
        for (int i = 0; i < group.MaxAmountOfStudents; i++)
        {
            service.AddStudent(group, "Person");
        }

        Assert.Throws<InvalidAmountOfStudentsException>(() => service.AddStudent(group, "Person"));
    }

    [Theory]
    [InlineData("131Bee")]
    [InlineData("23213M")]
    [InlineData("00")]
    public void CreateGroupWithInvalidName_ThrowException(string invalidName)
    {
        var timetable = new Timetable();
        var lesson = new Lesson(DayOfWeek.Friday, 2);
        timetable.GetTimetable.Add(lesson);
        Assert.Throws<InvalidGroupNameException>(() => service.AddGroup(invalidName, CourseNumber.Second, timetable, "TINT"));
    }

    [Fact]
    public void TransferStudentToAnotherGroup_GroupChanged()
    {
        var timetable = new Timetable();
        var lesson1 = new Lesson(DayOfWeek.Friday, 2);
        timetable.GetTimetable.Add(lesson1);
        var group = new Group("M32091", CourseNumber.Second, timetable, "TINT");
        Student student = service.AddStudent(group, "Ivan Ivanov");
        var timetable1 = new Timetable();
        var lesson2 = new Lesson(DayOfWeek.Tuesday, 2);
        timetable.GetTimetable.Add(lesson2);
        var newGroup = new Group("M32081", CourseNumber.Second, timetable1, "TINT");
        service.ChangeStudentGroup(student, newGroup);
        Assert.Equal(student, newGroup.Students.Last());
    }
}