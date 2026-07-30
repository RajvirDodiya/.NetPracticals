using System;
using System.Collections.Generic;

namespace ExpenseTracking
{
    class Expense
    {
        public int ExpenseId;
        public string Category;
        public int Amount;
        public string PaymentMode;
        public DateTime Date;

        public bool AddExpense()
        {
            try
            {
                Console.Write("Enter Expense Id: ");
                int id = Convert.ToInt32(Console.ReadLine());

                if (id <= 0)
                    throw new ArgumentException("Expense ID must be greater than 0.");

                Console.Write("Enter Category: ");
                string category = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(category))
                    throw new ArgumentException("Category cannot be empty.");

                Console.Write("Enter Amount: ");
                int amount = Convert.ToInt32(Console.ReadLine());

                if (amount <= 0)
                    throw new ArgumentException("Amount must be greater than 0.");

                Console.Write("Enter Payment Mode (Cash/UPI/Card/Other): ");
                string paymentMode = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(paymentMode))
                    throw new ArgumentException("Payment mode cannot be empty.");

                ExpenseId = id;
                Category = category;
                Amount = amount;
                PaymentMode = paymentMode;
                Date = DateTime.Now;

                return true;
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input. Enter numeric values for Expense ID and Amount.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Number entered is too large.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return false;
        }

        public void DisplayExpense()
        {
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Expense Id   : " + ExpenseId);
            Console.WriteLine("Category     : " + Category);
            Console.WriteLine("Amount       : " + Amount);
            Console.WriteLine("Payment Mode : " + PaymentMode);
            Console.WriteLine("Date         : " + Date.ToString("dd/MM/yyyy"));
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Expense> expenses = new List<Expense>();
            int choice = 0;

            do
            {
                Console.WriteLine("\n==================================");
                Console.WriteLine("     Expense Tracking System");
                Console.WriteLine("==================================");
                Console.WriteLine("1. Add Expense");
                Console.WriteLine("2. Display All Expenses");
                Console.WriteLine("3. Total Expenses");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your choice: ");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Expense e = new Expense();

                            if (e.AddExpense())
                            {
                                expenses.Add(e);
                                Console.WriteLine("Expense added successfully.");
                            }
                            break;

                        case 2:
                            if (expenses.Count == 0)
                            {
                                Console.WriteLine("No expenses found.");
                            }
                            else
                            {
                                foreach (Expense exp in expenses)
                                {
                                    exp.DisplayExpense();
                                }
                            }
                            break;

                        case 3:
                            int total = 0;

                            foreach (Expense exp in expenses)
                            {
                                total += exp.Amount;
                            }

                            Console.WriteLine("==================================");
                            Console.WriteLine("Total Expenses: " + total);
                            Console.WriteLine("==================================");
                            break;

                        case 4:
                            Console.WriteLine("Thank you for using Expense Tracking System.");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Select between 1 and 4.");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid choice. Enter a number between 1 and 4.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Number entered is too large.");
                }

            } while (choice != 4);

            Console.ReadKey();
        }
    }
}