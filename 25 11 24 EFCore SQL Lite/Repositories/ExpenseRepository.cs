﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _25_11_24_EFCore_SQL_Lite.Contexts;
using _25_11_24_EFCore_SQL_Lite.Models;
using Microsoft.EntityFrameworkCore;

namespace _25_11_24_EFCore_SQL_Lite.Repositories
{
    public class ExpenseRepository
    {
        private readonly ExpensesTrackerContext _context;
        public ExpenseRepository(ExpensesTrackerContext context)
        {
            _context = context;
        }

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);
            if(category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories.ToList();
        }

        public void AddExpense(Expense expense)
        {
            _context.Expenses.Add(expense);
            _context.SaveChanges();
        }

        public void DeleteExpense(int expenseId)
        {
            var expense = _context.Expenses.Find(expenseId);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                _context.SaveChanges();
            }
        }

        public IEnumerable<Expense> GetAllExpense()
        {
            return _context.Expenses.ToList();
        }

        public IEnumerable<Expense> GetExpensesByCategoryName(string categoryName)
        {
            return _context.Expenses
                .Where(e => e.Category.Name
                .Contains(categoryName))
                .ToList();
        }

        public IEnumerable<Expense> GetExpensesByCategory(int categoryId)
        {
            return _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.CategoryId == categoryId)
                .ToList();
        }

        public IEnumerable<Expense> GetExpensesByMonthYear(int month, int year)
        {
            return _context.Expenses
                .Include(e => e.Category)
                .Where(e => e.Date.Month == month && e.Date.Year == year)
                .ToList();
        }


    }
}