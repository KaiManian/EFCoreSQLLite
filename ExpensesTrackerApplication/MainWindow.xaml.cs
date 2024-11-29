using System.Windows;
using ExpensesTrackerLibrary.Contexts;
using ExpensesTrackerLibrary.Repositories;
using ExpensesTrackerLibrary.Models;
using System.Collections.Generic;
using System;
using System.Windows.Automation.Peers;
using System.Windows.Controls;


namespace ExpensesTrackerApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExpensesTrackerContext _context;
        private ExpenseRepository _repository;
        public MainWindow()
        {
            InitializeComponent();

            _context = new ExpensesTrackerContext();
            _repository = new ExpenseRepository(_context);

            dataGridCategories.ItemsSource = _repository.GetAllCategoriesObservable();
            dataGridExpenses.ItemsSource = _repository.GetAllExpensesObservable();

            comboBoxExpenseCategory.ItemsSource = _repository.GetAllCategoriesObservable();

            buttonAddCategory.Click += ButtonAddCategory_Click;
            buttonDeleteCategories.Click += ButtonDeleteCategories_Click;
            buttonAddExpense.Click += ButtonAddExpense_Click;
        }

        private void ButtonAddExpense_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? dateTime = (DateTime)datePickerExpenseDate.SelectedDate;
                Category category = comboBoxExpenseCategory.SelectedItem as Category;
                decimal amount = Convert.ToDecimal(textBoxExpenseAmount.Text);
                string description = textBoxExpenseDescription.Text;

                _repository.AddExpense(new Expense()
                {
                    Date= dateTime != null ? (DateTime)dateTime : DateTime.Now,
                    Category = category,
                    Amount = amount,
                    Description = description
                });
            }
            catch(Exception ex)
            {
                ShoeError(ex.Message);
            }
        }

        private void ButtonDeleteCategories_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridCategories.SelectedItems != null)
            {
                var categoriesToDelete = new List<Category>();
                foreach(var item in dataGridCategories.SelectedItems)
                {
                    categoriesToDelete.Add(item as Category);
                }
                _repository.DeleteCategories(categoriesToDelete);
            }
        }

        private void ButtonAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryName = textBoxCategoryName.Text.Trim();
            if(categoryName != string.Empty)
            {
                _repository.AddCategory(new Category()
                {
                    Name = categoryName,
                });
            }
            else
            {
                ShoeError("Category Name can't be empty!");
            }            
        }

        private void ShoeError(string message)
        {
            MessageBox.Show(message,
                "Error", MessageBoxButton.OK,
                MessageBoxImage.Error);
        }
    }
}
