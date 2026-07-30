using System;

namespace Practical1
{
    public class Student
    {
        // Private data members
        private int id;
        private string name;
        private string mobileNum;
        private string course;
        private double percentage;
        private double fees;

        // Public constructor
        public Student()
        {
            Console.WriteLine("====================================");
            Console.WriteLine("===== Student Admission System =====");
            Console.WriteLine("====================================");
        }

        // Public method to accept student details
        public void GetData()
        {
            Console.Write("Enter Student ID: ");
            id = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter Student Name: ");
            name = Console.ReadLine();

            Console.Write("Enter Mobile Number: ");
            mobileNum = Console.ReadLine();

            Console.Write("Enter Course Name: ");
            course = Console.ReadLine();

            Console.Write("Enter Percentage: ");
            percentage = Convert.ToDouble(Console.ReadLine());

            Console.Write("Enter Course Fees: ");
            fees = Convert.ToDouble(Console.ReadLine());
        }

        // Public method to display details
        public void Display()
        {
            Console.WriteLine("\n===== Student Details =====");
            Console.WriteLine("Student ID      : " + id);
            Console.WriteLine("Student Name    : " + name);
            Console.WriteLine("Mobile Number   : " + mobileNum);
            Console.WriteLine("Course          : " + course);
            Console.WriteLine("Percentage      : " + percentage + "%");
            Console.WriteLine("Course Fees     : " + fees);
        }

        // Public method for scholarship calculation
        public void ScholarshipCheck()
        {
            double rate;
            double finalFees;

            if (percentage >= 90)
                rate = 0.50;       // 50% scholarship
            else if (percentage >= 80)
                rate = 0.30;       // 30% scholarship
            else if (percentage >= 70)
                rate = 0.10;       // 10% scholarship
            else
                rate = 0;

            finalFees = fees - (fees * rate);

            Console.WriteLine("\n===== Scholarship Details =====");
            Console.WriteLine("Scholarship Rate: " + (rate * 100) + "%");
            Console.WriteLine("Final Fees      : " + finalFees);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Object creation
            Student student = new Student();

            student.GetData();
            student.Display();
            student.ScholarshipCheck();

            Console.ReadKey();
        }
    }
}