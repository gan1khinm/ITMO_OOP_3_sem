using Isu.Entities;
using Isu.Models;
using Isu.Services;
using Xunit;

namespace Isu.Test;

public class IsuExtraServiceTest
{
    private readonly IsuExtraService service;

    public IsuExtraServiceTest()
    {
        service = new IsuExtraService();
    }

    [Fact]
    public void AddStudentToOGNP_StudentHasOGNPAndOGNPContainsStudent()
    {
        var timetable = new Timetable();
        var lesson1 = new Lesson(DayOfWeek.Monday, 1);
        timetable.GetTimetable.Add(lesson1);
        Group group = service.AddGroup("M32091", CourseNumber.Second, timetable, "TINT");
        Student student = service.AddStudent(group, "Maksim");
        OGNP ognp = service.AddOGNP("CyberBez", "FITiP", CourseNumber.Second);
        var timetable1 = new Timetable();
        var lesson2 = new Lesson(DayOfWeek.Monday, 5);
        timetable1.GetTimetable.Add(lesson2);
        Flow flow = service.AddFlow(ognp, "Isayeva", 313, "first", timetable1);

        var timetable2 = new Timetable();
        var lesson3 = new Lesson(DayOfWeek.Thursday, 1);
        timetable2.GetTimetable.Add(lesson3);
        GroupOGNP ognpGroup = service.AddGroupOGNP(flow, "A2", "Stoyeva A.V.", 123, timetable2);
        service.AddStudentToOGNP(student, ognp, flow, ognpGroup);

        Assert.Contains(student, flow.Groups[0].Students);
    }

    [Fact]
    public void DeleteStudentFromOGNP_StudentHasNotOGNPAndOGNPDoesNotCoantainStudent()
    {
        var timetable = new Timetable();
        var lesson1 = new Lesson(DayOfWeek.Monday, 1);
        timetable.GetTimetable.Add(lesson1);
        Group group = service.AddGroup("M32091", CourseNumber.Second, timetable, "TINT");
        Student student = service.AddStudent(group, "Maksim");
        OGNP ognp = service.AddOGNP("CyberBez", "FITiP", CourseNumber.Second);
        var timetable1 = new Timetable();
        var lesson2 = new Lesson(DayOfWeek.Monday, 6);
        timetable1.GetTimetable.Add(lesson2);
        Flow flow = service.AddFlow(ognp, "Isayeva", 313, "first", timetable1);

        var timetable2 = new Timetable();
        var lesson3 = new Lesson(DayOfWeek.Wednesday, 1);
        timetable2.GetTimetable.Add(lesson3);
        GroupOGNP groupOGNP = service.AddGroupOGNP(flow, "A2", "Stoyeva A.V.", 123, timetable2);
        service.AddStudentToOGNP(student, ognp, flow, groupOGNP);
        service.DeleteStudentFromOGNP(student.Id, ognp);

        Assert.DoesNotContain(student, flow.Groups[0].Students);
    }

    [Fact]
    public void GetFlows_FlowsAreGot()
    {
        OGNP ognp1 = service.AddOGNP("CyberBez", "FITiP", CourseNumber.Second);
        OGNP ognp2 = service.AddOGNP("CyberBez", "FITiP", CourseNumber.Third);

        var timetable1 = new Timetable();
        var lesson1 = new Lesson(DayOfWeek.Monday, 1);
        timetable1.GetTimetable.Add(lesson1);
        var rightFlows = new List<Flow>();
        Flow flow1 = service.AddFlow(ognp1, "Isayeva", 313, "first", timetable1);
        rightFlows.Add(flow1);

        var timetable2 = new Timetable();
        var lesson2 = new Lesson(DayOfWeek.Monday, 3);
        timetable2.GetTimetable.Add(lesson2);
        Flow flow2 = service.AddFlow(ognp1, "Martinova", 310, "Second", timetable2);
        rightFlows.Add(flow2);

        var timetable3 = new Timetable();
        var lesson3 = new Lesson(DayOfWeek.Friday, 1);
        timetable1.GetTimetable.Add(lesson3);
        service.AddFlow(ognp2, "Isayeva", 313, "first", timetable3);

        Assert.Equal(rightFlows, service.GetFlows(CourseNumber.Second));
    }

    [Fact]
    public void GetStudentsFromOGNPgroup_StudentsAreGot()
    {
        var timetable = new Timetable();
        var lesson1 = new Lesson(DayOfWeek.Monday, 1);
        timetable.GetTimetable.Add(lesson1);
        Group group = service.AddGroup("M32091", CourseNumber.Second, timetable, "TINT");
        Student student1 = service.AddStudent(group, "Maksim");
        Student student2 = service.AddStudent(group, "Ivan");
        OGNP ognp = service.AddOGNP("CyberBez", "FITiP", CourseNumber.Second);

        var timetable1 = new Timetable();
        var lesson2 = new Lesson(DayOfWeek.Saturday, 1);
        timetable1.GetTimetable.Add(lesson2);
        Flow flow = service.AddFlow(ognp, "Isayeva", 313, "first", timetable1);

        var timetable2 = new Timetable();
        var lesson3 = new Lesson(DayOfWeek.Monday, 7);
        timetable2.GetTimetable.Add(lesson3);
        GroupOGNP groupOGNP = service.AddGroupOGNP(flow, "A2", "Stoyeva A.V.", 123, timetable2);
        service.AddStudentToOGNP(student1, ognp, flow, groupOGNP);
        service.AddStudentToOGNP(student2, ognp, flow, groupOGNP);

        Assert.Equal(groupOGNP.Students, service.GetStudentsFromOGNPgroup(ognp, groupOGNP));
    }

    [Fact]
    public void GetStudentsWithoutOGNP_StudentsWithoutOGNPareGot()
    {
        var timetable = new Timetable();
        var lesson1 = new Lesson(DayOfWeek.Monday, 1);
        timetable.GetTimetable.Add(lesson1);
        Group group = service.AddGroup("M32091", CourseNumber.Second, timetable, "TINT");
        Student student1 = service.AddStudent(group, "Maksim");
        Student student2 = service.AddStudent(group, "Vadim");
        Student student3 = service.AddStudent(group, "Ivan");
        OGNP ognp = service.AddOGNP("CyberBez", "FITiP", CourseNumber.Second);

        var timetable1 = new Timetable();
        var lesson2 = new Lesson(DayOfWeek.Monday, 2);
        timetable1.GetTimetable.Add(lesson2);
        Flow flow = service.AddFlow(ognp, "Isayeva", 313, "first", timetable1);

        var timetable2 = new Timetable();
        var lesson3 = new Lesson(DayOfWeek.Tuesday, 1);
        timetable2.GetTimetable.Add(lesson3);
        GroupOGNP groupOGNP = service.AddGroupOGNP(flow, "A2", "Stoyeva A.V.", 123, timetable2);

        service.AddStudentToOGNP(student1, ognp, flow, groupOGNP);
        var rightListOfStudents = new List<Student>();
        rightListOfStudents.Add(student2);
        rightListOfStudents.Add(student3);

        Assert.Equal(rightListOfStudents, service.GetStudentsWithoutOGNP());
    }
}