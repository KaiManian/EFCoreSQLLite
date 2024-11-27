using _25_11_24_EFCore_SQL_Lite.Contexts;
using _25_11_24_EFCore_SQL_Lite.Models;

internal class Program
{
    private static void Main(string[] args)
    {
        using (var context = new ExpensesTrackerContext())
        {
            //context.Categories.Add(new Category { Name = "Food" });
            //context.Categories.Add(new Category { Name = "Transportation" });

            //context.SaveChanges();

            context.Expenses.Add(new Expense
            {
                Date = DateTime.Now,
                Amount = 150,
                Description = "Diner",
                Category = context.Categories.FirstOrDefault()
            });

            context.SaveChanges();

            //Retrieve and display all expenses
            var expenses = context.Expenses.ToList();
            foreach (var expense in expenses)
            {
                Console.WriteLine($"Date: {expense.Date}, Amount: {expense.Amount}, " +
                    $"Description: {expense.Description}, Category: {expense.Category.Name}");
            }
        }
    }
}