using _25_11_24_EFCore_SQL_Lite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _25_11_24_EFCore_SQL_Lite.Contexts
{
    public class ExpensesTrackerContext : DbContext
    {
        //DbSet для категорий расходов
        public DbSet<Category> Categories { get; set; }
        //DbSet для самих расходов
        public DbSet<Expense> Expenses { get; set; }

        //Конструктор класса
        public ExpensesTrackerContext()
        {
            //Удаляем существующую базу данных, если она есть
            //Database.EnsureDeleted();

            //Создаем новую базу данных, если ее нет
            Database.EnsureCreated();
        }

        //Метод для настройки подключения к базе данных
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Используем Sqlite базу данных с указанием имени файла
            optionsBuilder.UseSqlite("Data Source=Expenses.db");
        }
    }
}
