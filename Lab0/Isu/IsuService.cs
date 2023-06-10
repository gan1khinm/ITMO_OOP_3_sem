using Isu.Entities;
using Isu.Exceptions;
using Isu.Models;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private Dictionary<string, Group> groups;
        private int idCounter;
        public IsuService()
        {
            groups = new Dictionary<string, Group>();
            idCounter = 100000;
        }

        public Dictionary<string, Group> Groups { get { return groups; } }

        public Group AddGroup(string groupName, CourseNumber courseNumber, Timetable timetable, string megafaculty)
        {
            if (!Group.TryParse(groupName))
            {
                throw new InvalidGroupNameException(groupName);
            }

            var newGroup = new Group(groupName, courseNumber, timetable, megafaculty);
            groups.Add(groupName, newGroup);

            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group.Students.Count() < group.MaxAmountOfStudents)
            {
                idCounter++;
                var student = new Student(idCounter, name, group);
                group.Students.Add(student);
                return student;
            }

            throw new InvalidAmountOfStudentsException(group.Students.Count());
        }

        public Student GetStudent(int id)
        {
            foreach (KeyValuePair<string, Group> group in groups)
            {
                foreach (Student student in group.Value.Students)
                {
                    if (student.Id == id)
                    {
                        return student;
                    }
                }
            }

            throw new InvalidStudentIdException(id);
        }

        public Student? FindStudent(int id)
        {
            foreach (KeyValuePair<string, Group> group in groups)
            {
                foreach (Student student in group.Value.Students)
                {
                    if (student.Id == id)
                    {
                        return student;
                    }
                }
            }

            return null;
        }

        public List<Student>? FindStudents(string groupName)
        {
            if (!Group.TryParse(groupName))
            {
                throw new InvalidGroupNameException(groupName);
            }

            foreach (KeyValuePair<string, Group> group_ in groups)
            {
                if (group_.Value.GroupName == groupName)
                {
                    return group_.Value.Students;
                }
            }

            return null;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var studentsFromThisCourse = new List<Student>();
            foreach (KeyValuePair<string, Group> group in groups)
            {
                foreach (Student student in group.Value.Students)
                {
                    if (student.Group!.CourseNumber == courseNumber)
                    {
                        studentsFromThisCourse.Add(student);
                    }
                }
            }

            return studentsFromThisCourse;
        }

        public Group? FindGroup(string groupName)
        {
            if (!Group.TryParse(groupName))
            {
                throw new InvalidGroupNameException(groupName);
            }

            foreach (KeyValuePair<string, Group> group_ in groups)
            {
                if (group_.Value.GroupName == groupName)
                {
                    return group_.Value;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var groupsFromThisCourse = new List<Group>();
            foreach (KeyValuePair<string, Group> group in groups)
            {
                if (group.Value.CourseNumber == courseNumber)
                {
                    groupsFromThisCourse.Add(group.Value);
                }
            }

            return groupsFromThisCourse;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            student.Group?.Students.Remove(student);
            newGroup.Students.Add(student!);
        }
    }
}