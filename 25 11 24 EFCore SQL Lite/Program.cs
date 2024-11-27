using ExpensesTrackerLibrary.Contexts;
using ExpensesTrackerLibrary.Models;
using ExpensesTrackerLibrary.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var context = new ExpensesTrackerContext())
        {
            var repo = new ExpenseRepository(context);
            
            var categories = repo.GetAllCategories();

            //foreach(var cat in categories)
            //{
            //    Console.WriteLine(cat.Name);
            //}

            repo.AddExpense(new Expense
            {
                Date = DateTime.Now,
                Amount = 80,
                Description = "Bus",
                Category = categories.Skip(1).FirstOrDefault()
            });

            //Retrieve and display all expenses
            var expenses = repo.GetAllExpense();
            foreach (var expense in expenses)
            {
                Console.WriteLine($"Date: {expense.Date}, Amount: {expense.Amount:0.00}, " +
                    $"Description: {expense.Description}, Category: {expense.Category.Name}");
            }
        }
    }
}