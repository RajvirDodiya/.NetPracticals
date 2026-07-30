using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practical2
{
    class Employee{


           public int id;
           public string name;
           public double basicSalary;
        public Employee() {
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("--------Employee Payroll System----------");
            Console.WriteLine("-----------------------------------------");


        }
        public void AcceptDetails(){
            Console.WriteLine("Enter Employee ID:");
            id = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Enter Employee Name:");
            name =Console.ReadLine();

            Console.WriteLine("Enter Employee Basic Salary:");
            basicSalary = Convert.ToDouble(Console.ReadLine());

        }

        public void DisplayDetails()
        {
            Console.WriteLine("Employee ID:"+id);
            Console.WriteLine("Employee Name:"+name);
            Console.WriteLine("Employee BasicSalary:"+basicSalary);
        }

        public virtual void CalculateSalary()
        {
            Console.WriteLine("Calculation of Salary");
        }
    }

    class FullTimeEmployee : Employee
    {
            double hra = 0.2, da = 0.1;
        public override void CalculateSalary()
        {   
            double salary = basicSalary + basicSalary * (hra + da);
            Console.WriteLine("Final FullTime Salary: " + salary);
        }
    }

    class PartTimeEmployee : Employee {

        public override void CalculateSalary()
        {
            Console.WriteLine("Final PartTime Salary: " + basicSalary);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            int choice;

            Console.WriteLine("Select Employee Type");
            Console.WriteLine("1) Full-Time");
            Console.WriteLine("2) Part-Time");
            Console.Write("Enter Choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            Employee E1;

            if (choice == 1)
            {
                E1 = new FullTimeEmployee();
            }
            else if (choice == 2)
            {
                E1 = new PartTimeEmployee();
            }
            else
            {
                Console.WriteLine("Invalid Choice");
                return;
            }

            E1.AcceptDetails();
            E1.DisplayDetails();
            E1.CalculateSalary();
        }
     }
    }

