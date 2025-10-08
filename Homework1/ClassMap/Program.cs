using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassMap
{
    public abstract class CommunityMember
    {
        public Guid Id { get; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime RegisteredAt { get; } = DateTime.UtcNow;

        protected CommunityMember(string name, string email)
        {
            Name = name;
            Email = email;
        }

        public virtual string Describe()
        {
            return $"[{GetType().Name}] {Name} <{Email}>";
        }

        public abstract string Activity();
    }

    public abstract class Employee : CommunityMember
    {
        public string Department { get; set; }
        public DateTime HireDate { get; set; }
        public decimal MonthlySalary { get; set; }

        protected Employee(string name, string email, string department, DateTime hireDate, decimal salary)
            : base(name, email)
        {
            Department = department;
            HireDate = hireDate;
            MonthlySalary = salary;
        }

        public int YearsOfService
        {
            get
            {
                var days = (DateTime.UtcNow - HireDate).TotalDays;
                return (int)Math.Floor(days / 365.25);
            }
        }

        public virtual decimal ComputeMonthlyPayroll()
        {
            return MonthlySalary;
        }

        public override string Describe()
        {
            return base.Describe() + $" | Dept: {Department} | Years: {YearsOfService} | Salary: {MonthlySalary:C}";
        }
    }

    public abstract class Teacher : Employee
    {
        public List<string> Subjects { get; } = new();

        protected Teacher(string name, string email, string department, DateTime hireDate, decimal salary)
            : base(name, email, department, hireDate, salary) { }

        public void AddSubject(string subject)
        {
            if (!string.IsNullOrWhiteSpace(subject))
                Subjects.Add(subject.Trim());
        }

        public override string Describe()
        {
            var extra = Subjects.Count > 0 ? $" | Subjects: {string.Join(", ", Subjects)}" : "";
            return base.Describe() + extra;
        }

        public override string Activity()
        {
            return "Teaching classes and preparing materials";
        }
    }

    public sealed class Administrator : Teacher
    {
        public string Position { get; set; }

        public Administrator(string name, string email, string department, DateTime hireDate, decimal salary, string position)
            : base(name, email, department, hireDate, salary)
        {
            Position = position;
        }

        public override string Describe()
        {
            return base.Describe() + $" | Position: {Position}";
        }

        public override string Activity()
        {
            return "Managing academic operations and overseeing staff";
        }
    }

    public sealed class Master : Teacher
    {
        public string Degree { get; set; }

        public Master(string name, string email, string department, DateTime hireDate, decimal salary, string degree)
            : base(name, email, department, hireDate, salary)
        {
            Degree = degree;
        }

        public override string Describe()
        {
            return base.Describe() + $" | Degree: {Degree}";
        }

        public override string Activity()
        {
            return "Delivering lectures and mentoring students";
        }
    }

    public sealed class Administrative : Employee
    {
        public string JobTitle { get; set; }

        public Administrative(string name, string email, string department, DateTime hireDate, decimal salary, string jobTitle)
            : base(name, email, department, hireDate, salary)
        {
            JobTitle = jobTitle;
        }

        public override string Describe()
        {
            return base.Describe() + $" | Title: {JobTitle}";
        }

        public override string Activity()
        {
            return "Performing administrative and office tasks";
        }
    }

    public sealed class Student : CommunityMember
    {
        public string Career { get; set; }
        public int Term { get; set; }
        public double GPA { get; set; }

        public Student(string name, string email, string career, int term, double gpa)
            : base(name, email)
        {
            Career = career;
            Term = term;
            GPA = gpa;
        }

        public void EnrollToNextTerm()
        {
            Term += 1;
        }

        public override string Describe()
        {
            return base.Describe() + $" | Career: {Career} | Term: {Term} | GPA: {GPA}";
        }

        public override string Activity()
        {
            return "Attending classes and completing assignments";
        }
    }

    public sealed class Alumni : CommunityMember
    {
        public string GraduatedCareer { get; set; }
        public int GraduationYear { get; set; }
        public string CurrentCompany { get; set; }

        public Alumni(string name, string email, string graduatedCareer, int graduationYear, string company)
            : base(name, email)
        {
            GraduatedCareer = graduatedCareer;
            GraduationYear = graduationYear;
            CurrentCompany = company;
        }

        public override string Describe()
        {
            return base.Describe() + $" | Graduated: {GraduatedCareer} ({GraduationYear}) | Company: {CurrentCompany}";
        }

        public override string Activity()
        {
            return "Working in the industry and collaborating with the university";
        }
    }

    public static class ConsoleHelper
    {
        public static string Ask(string label)
        {
            Console.Write(label);
            return Console.ReadLine() ?? "";
        }

        public static int AskInt(string label, int defaultValue = 0)
        {
            Console.Write(label);
            var txt = Console.ReadLine();
            if (int.TryParse(txt, out var v)) return v;
            return defaultValue;
        }

        public static decimal AskDecimal(string label, decimal defaultValue = 0m)
        {
            Console.Write(label);
            var txt = Console.ReadLine();
            if (decimal.TryParse(txt, out var v)) return v;
            return defaultValue;
        }

        public static DateTime AskDate(string label, DateTime? fallback = null)
        {
            Console.Write(label);
            var txt = Console.ReadLine();
            if (DateTime.TryParse(txt, out var d)) return d;
            return fallback ?? DateTime.UtcNow;
        }

        public static double AskDouble(string label, double defaultValue = 0)
        {
            Console.Write(label);
            var txt = Console.ReadLine();
            if (double.TryParse(txt, out var v)) return v;
            return defaultValue;
        }
    }

    internal class Program
    {
        static readonly List<CommunityMember> Members = new();

        static void Main()
        {
            SeedSampleData();
            RunMenu();
        }

        static void RunMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Class Map Console");
                Console.WriteLine("1) List all members");
                Console.WriteLine("2) List by type");
                Console.WriteLine("3) Show activities");
                Console.WriteLine("4) Add student");
                Console.WriteLine("5) Add alumni");
                Console.WriteLine("6) Add administrative");
                Console.WriteLine("7) Add master");
                Console.WriteLine("8) Add administrator");
                Console.WriteLine("9) Assign subject to a teacher");
                Console.WriteLine("10) Promote student to alumni");
                Console.WriteLine("11) Compute total payroll");
                Console.WriteLine("0) Exit");
                Console.Write("Select: ");
                var op = Console.ReadLine();

                if (op == "0") break;
                if (op == "1") ListAll();
                else if (op == "2") ListByType();
                else if (op == "3") ShowActivities();
                else if (op == "4") AddStudent();
                else if (op == "5") AddAlumni();
                else if (op == "6") AddAdministrative();
                else if (op == "7") AddMaster();
                else if (op == "8") AddAdministrator();
                else if (op == "9") AssignSubjectToTeacher();
                else if (op == "10") PromoteStudentToAlumni();
                else if (op == "11") ComputeTotalPayroll();
                Pause();
            }
        }

        static void ListAll()
        {
            Console.WriteLine();
            foreach (var m in Members) Console.WriteLine(m.Describe());
        }

        static void ListByType()
        {
            Console.WriteLine();
            Console.Write("Type (Student, Alumni, Administrative, Master, Administrator): ");
            var t = Console.ReadLine() ?? "";
            var filtered = Members.Where(m => string.Equals(m.GetType().Name, t, StringComparison.OrdinalIgnoreCase)).ToList();
            if (filtered.Count == 0) Console.WriteLine("No results");
            else foreach (var m in filtered) Console.WriteLine(m.Describe());
        }

        static void ShowActivities()
        {
            Console.WriteLine();
            foreach (var m in Members) Console.WriteLine($"{m.GetType().Name} -> {m.Activity()}");
        }

        static void AddStudent()
        {
            Console.WriteLine();
            var name = ConsoleHelper.Ask("Name: ");
            var email = ConsoleHelper.Ask("Email: ");
            var career = ConsoleHelper.Ask("Career: ");
            var term = ConsoleHelper.AskInt("Term: ");
            var gpa = ConsoleHelper.AskDouble("GPA: ");
            Members.Add(new Student(name, email, career, term, gpa));
            Console.WriteLine("Student added");
        }

        static void AddAlumni()
        {
            Console.WriteLine();
            var name = ConsoleHelper.Ask("Name: ");
            var email = ConsoleHelper.Ask("Email: ");
            var career = ConsoleHelper.Ask("Graduated career: ");
            var year = ConsoleHelper.AskInt("Graduation year: ");
            var company = ConsoleHelper.Ask("Company: ");
            Members.Add(new Alumni(name, email, career, year, company));
            Console.WriteLine("Alumni added");
        }

        static void AddAdministrative()
        {
            Console.WriteLine();
            var name = ConsoleHelper.Ask("Name: ");
            var email = ConsoleHelper.Ask("Email: ");
            var dept = ConsoleHelper.Ask("Department: ");
            var hire = ConsoleHelper.AskDate("Hire date (yyyy-MM-dd): ");
            var salary = ConsoleHelper.AskDecimal("Monthly salary: ");
            var title = ConsoleHelper.Ask("Job title: ");
            Members.Add(new Administrative(name, email, dept, hire, salary, title));
            Console.WriteLine("Administrative added");
        }

        static void AddMaster()
        {
            Console.WriteLine();
            var name = ConsoleHelper.Ask("Name: ");
            var email = ConsoleHelper.Ask("Email: ");
            var dept = ConsoleHelper.Ask("Department: ");
            var hire = ConsoleHelper.AskDate("Hire date (yyyy-MM-dd): ");
            var salary = ConsoleHelper.AskDecimal("Monthly salary: ");
            var degree = ConsoleHelper.Ask("Degree: ");
            var m = new Master(name, email, dept, hire, salary, degree);
            Members.Add(m);
            Console.WriteLine("Master added");
        }

        static void AddAdministrator()
        {
            Console.WriteLine();
            var name = ConsoleHelper.Ask("Name: ");
            var email = ConsoleHelper.Ask("Email: ");
            var dept = ConsoleHelper.Ask("Department: ");
            var hire = ConsoleHelper.AskDate("Hire date (yyyy-MM-dd): ");
            var salary = ConsoleHelper.AskDecimal("Monthly salary: ");
            var position = ConsoleHelper.Ask("Position: ");
            var a = new Administrator(name, email, dept, hire, salary, position);
            Members.Add(a);
            Console.WriteLine("Administrator added");
        }

        static void AssignSubjectToTeacher()
        {
            Console.WriteLine();
            var teachers = Members.OfType<Teacher>().ToList();
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers available");
                return;
            }
            for (int i = 0; i < teachers.Count; i++)
                Console.WriteLine($"{i + 1}) {teachers[i].Describe()}");
            var idx = ConsoleHelper.AskInt("Select teacher number: ", 1) - 1;
            if (idx < 0 || idx >= teachers.Count)
            {
                Console.WriteLine("Invalid selection");
                return;
            }
            var subject = ConsoleHelper.Ask("Subject to add: ");
            teachers[idx].AddSubject(subject);
            Console.WriteLine("Subject assigned");
        }

        static void PromoteStudentToAlumni()
        {
            Console.WriteLine();
            var students = Members.OfType<Student>().ToList();
            if (students.Count == 0)
            {
                Console.WriteLine("No students available");
                return;
            }
            for (int i = 0; i < students.Count; i++)
                Console.WriteLine($"{i + 1}) {students[i].Describe()}");
            var idx = ConsoleHelper.AskInt("Select student number to promote: ", 1) - 1;
            if (idx < 0 || idx >= students.Count)
            {
                Console.WriteLine("Invalid selection");
                return;
            }
            var s = students[idx];
            var year = ConsoleHelper.AskInt("Graduation year: ", DateTime.UtcNow.Year);
            var company = ConsoleHelper.Ask("Company: ");
            var alumn = new Alumni(s.Name, s.Email, s.Career, year, company);
            Members.Remove(s);
            Members.Add(alumn);
            Console.WriteLine("Student promoted to Alumni");
        }

        static void ComputeTotalPayroll()
        {
            Console.WriteLine();
            var employees = Members.OfType<Employee>().ToList();
            if (employees.Count == 0)
            {
                Console.WriteLine("No employees available");
                return;
            }
            decimal total = employees.Sum(e => e.ComputeMonthlyPayroll());
            Console.WriteLine($"Total monthly payroll: {total:C}");
        }

        static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey();
        }

        static void SeedSampleData()
        {
            var s1 = new Student("Anna Smith", "anna@univ.edu", "Software Engineering", 3, 3.8);
            var s2 = new Student("Carlos Lee", "carlos@univ.edu", "Information Systems", 2, 3.4);
            var a1 = new Alumni("John Doe", "john@alumni.edu", "Computer Networks", 2021, "TechCorp");
            var m1 = new Master("Mary Brown", "mary@univ.edu", "Math", new DateTime(2019, 9, 1), 45000m, "MSc");
            m1.AddSubject("Calculus I");
            m1.AddSubject("Linear Algebra");
            var ad1 = new Administrator("Peter White", "peter@univ.edu", "Academic Affairs", new DateTime(2017, 2, 15), 60000m, "School Director");
            ad1.AddSubject("Educational Management");
            var atv = new Administrative("Sophia Green", "sophia@univ.edu", "Registrar", new DateTime(2020, 1, 10), 30000m, "Records Analyst");
            Members.AddRange(new CommunityMember[] { s1, s2, a1, m1, ad1, atv });
        }
    }
}
