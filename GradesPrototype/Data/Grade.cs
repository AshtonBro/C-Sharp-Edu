using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GradesPrototype.Data
{
    // Types of user
    public enum Role { Teacher, Student };

    // WPF Databinding requires properties

    // TODO: Exercise 1: Task 1a: Convert Grade into a class and define constructors
    public class Grade
    {
        public int StudentID { get; set; }
        public string AssessmentDate { get; set; }
        public string SubjectName { get; set; }
        public string Assessment { get; set; }
        public string Comments { get; set; }

        public Grade(int studentID, string assessmentDate, string subjectName, string assessment, string сomments)
        {
            this.StudentID = studentID;
            this.AssessmentDate = assessmentDate;
            this.SubjectName = subjectName;
            this.Assessment = assessment;
            this.Comments = сomments;
        }

        public Grade()
        {
            StudentID = 0;
            AssessmentDate = DateTime.Now.ToString("d");
            SubjectName = "Math";
            Assessment = "A-";
            Comments = "Good";
        }

    }

    // TODO: Exercise 1: Task 2a: Convert Student into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Student
    {
        public int StudentID { get; set; }
        public string UserName { get; set; }
        private string _password = " ";
        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public bool VerifyPassword(string pass)
        {
            return (String.Compare(pass, _password) == 0);
        }
        public int TeacherID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(int studentID, string userName, string password, int teacherID, string firstName, string lastName)
        {
            this.StudentID = studentID;
            this.UserName = userName;
            this.Password = password;
            this.TeacherID = teacherID;
            this.FirstName = firstName;
            this.LastName = lastName;
        }

        public Student()
        {
            StudentID = 0;
            UserName = String.Empty;
            Password = String.Empty;
            TeacherID = 0;
            FirstName = String.Empty;
            LastName = String.Empty;
        }
    }

    // TODO: Exercise 1: Task 2b: Convert Teacher into a class, make the password property write-only, add the VerifyPassword method, and define constructors
    public class Teacher
    {
        public int TeacherID { get; set; }
        public string UserName { get; set; }
        private string _password = " ";
        public string Password
        {
            set
            {
                _password = value;
            }
        }

        public bool VerifyPassword(string pass)
        {
            return (String.Compare(pass, _password) == 0);
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Class { get; set; }
       
        public Teacher(int teacherID, string userName, string password, string firstName, string lastName, string _class)
        {
            this.TeacherID = teacherID;
            this.UserName = userName;
            this.Password = password;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Class = _class;
        }

        public Teacher()
        {
            TeacherID = 0;
            UserName = String.Empty;
            Password = String.Empty;
            FirstName = String.Empty;
            LastName = String.Empty;
            Class = String.Empty;
        }
    }
}
