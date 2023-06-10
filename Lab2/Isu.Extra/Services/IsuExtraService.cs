using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services
{
    public class IsuExtraService : IsuService
    {
        private readonly List<OGNP> listOfOGNPs;
        private Dictionary<Student, OGNP> studentsOGNPs;
        public IsuExtraService()
        {
            listOfOGNPs = new List<OGNP>();
            studentsOGNPs = new Dictionary<Student, OGNP>();
        }

        public OGNP AddOGNP(string name, string megaFaculty, CourseNumber courseNumber)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (megaFaculty is null)
            {
                throw new ArgumentNullException(nameof(megaFaculty));
            }

            var newOGHP = new OGNP(name, megaFaculty, courseNumber);
            listOfOGNPs.Add(newOGHP);

            return newOGHP;
        }

        public Flow AddFlow(OGNP ognp, string teacher, int classroom, string name, Timetable timetable)
        {
            if (ognp is null)
            {
                throw new ArgumentNullException(nameof(ognp));
            }

            if (teacher is null)
            {
                throw new ArgumentNullException(nameof(teacher));
            }

            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (timetable is null)
            {
                throw new ArgumentNullException(nameof(timetable));
            }

            var flow = new Flow(teacher, classroom, name, timetable);
            ognp.Flows.Add(flow);
            return flow;
        }

        public GroupOGNP AddGroupOGNP(Flow flow, string groupName, string teacher, int classroom, Timetable timetable)
        {
            if (flow is null)
            {
                throw new ArgumentNullException(nameof(flow));
            }

            if (groupName is null)
            {
                throw new ArgumentNullException(nameof(groupName));
            }

            if (teacher is null)
            {
                throw new ArgumentNullException(nameof(teacher));
            }

            if (timetable is null)
            {
                throw new ArgumentNullException(nameof(timetable));
            }

            CheckOfTimetable(flow.Timetable.GetTimetable, timetable.GetTimetable);

            var group_ognp = new GroupOGNP(groupName, teacher, classroom, timetable);
            flow.Groups.Add(group_ognp);

            return group_ognp;
        }

        public List<Student> AddStudentToOGNP(Student student, OGNP ognp, Flow flow, GroupOGNP groupOGNP)
        {
            if (student is null)
            {
                throw new ArgumentNullException(nameof(student));
            }

            if (ognp is null)
            {
                throw new ArgumentNullException(nameof(ognp));
            }

            foreach (KeyValuePair<string, Group> group in Groups)
            {
                if (group.Value.Students.Contains(student))
                {
                    if (ognp.MegaFaculty == group.Value.Megafaculty)
                    {
                        throw new InvalidMegaFacultyException(ognp.MegaFaculty);
                    }
                }
            }

            if (groupOGNP.Students.Count < groupOGNP.MaxAmountOfStudents)
            {
                groupOGNP.Students.Add(student);
            }
            else
            {
                throw new InvalidAmountOfStudentsException(30);
            }

            CheckOfTimetable(flow.Timetable.GetTimetable, student.Group.Timetable.GetTimetable);

            CheckOfTimetable(groupOGNP.Timetable.GetTimetable, student.Group.Timetable.GetTimetable);

            studentsOGNPs.Add(student, ognp);

            return groupOGNP.Students;
        }

        public void CheckOfTimetable(List<Lesson> lessons1, List<Lesson> lessons2)
        {
            foreach (var lesson in lessons1.SelectMany(lesson1 => lessons2.Where(lesson2 => lesson1.DayOfWeek == lesson2.DayOfWeek &&
            lesson1.NumberOfLesson == lesson2.NumberOfLesson).Select(lesson2 => new { })))
            {
            throw new InvalidTimetableException(lessons2);
            }
        }

        public List<Student> DeleteStudentFromOGNP(int studentId, OGNP ognp)
        {
            if (ognp is null)
            {
                throw new ArgumentNullException(nameof(ognp));
            }

            var flagListOfStudents = new List<Student>();
            foreach ((GroupOGNP groupOGNP, Student student) in ognp.Flows.SelectMany(flow => flow.Groups.SelectMany(groupOGNP =>
            groupOGNP.Students.Where(student => student.Id == studentId).Select(student => (groupOGNP, student)))))
            {
                groupOGNP.Students.Remove(student);
                flagListOfStudents = groupOGNP.Students;
                break;
            }

            return flagListOfStudents;
        }

        public List<Flow>? GetFlows(CourseNumber courseNumber)
        {
            var flows = listOfOGNPs.Where(ognp => ognp.CourseNumber == courseNumber).SelectMany(ognp => ognp.Flows).ToList();
            return flows;
        }

        public List<Student>? GetStudentsFromOGNPgroup(OGNP ognp, GroupOGNP groupOGNP)
        {
            if (ognp is null)
            {
                throw new ArgumentNullException(nameof(ognp));
            }

            if (groupOGNP is null)
            {
                throw new ArgumentNullException(nameof(groupOGNP));
            }

            var students = new List<Student>();
            foreach (var group in ognp.Flows.SelectMany(flow => flow.Groups.Where(groupOGNP_ => groupOGNP_ ==
            groupOGNP).Select(groupOGNP_ => new { })))
            {
                students = groupOGNP.Students;
            }

            return students;
        }

        public List<Student>? GetStudentsWithoutOGNP()
        {
            var studentsWithoutOGNP = new List<Student>();
            foreach (KeyValuePair<string, Group> group in Groups)
            {
                studentsWithoutOGNP.AddRange(group.Value.Students.Where(student => !studentsOGNPs.Keys.Contains(student)));
            }

            return studentsWithoutOGNP;
        }
    }
}