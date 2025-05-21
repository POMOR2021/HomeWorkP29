using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManagementApp
{
    // Модели данных
    public class Order
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public Table Table { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class Table
    {
        public int TableId { get; set; }
        public int TableNumber { get; set; }
        public int Capacity { get; set; }
    }

    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public MenuItem MenuItem { get; set; }
    }

    // Контекст базы данных
    public class RestaurantDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Table)
                .WithMany(t => t.Orders)
                .HasForeignKey(o => o.TableId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.MenuItem)
                .WithMany(mi => mi.OrderItems)
                .HasForeignKey(oi => oi.MenuItemId);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId);
        }
    }

    // Класс для работы с данными
    public class RestaurantManager
    {
        private readonly RestaurantDbContext _context;

        public RestaurantManager(RestaurantDbContext context)
        {
            _context = context;
        }

        // CRUD операции для MenuItem
        public void AddMenuItem(MenuItem menuItem)
        {
            _context.MenuItems.Add(menuItem);
            _context.SaveChanges();
        }

        // CRUD операции для Table
        public void AddTable(Table table)
        {
            _context.Tables.Add(table);
            _context.SaveChanges();
        }

        // CRUD операции для Order
        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrderStatus(int orderId, string newStatus)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.Status = newStatus;
                _context.SaveChanges();
            }
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }

        // Запросы
        public List<Order> GetOrdersForTable(int tableId)
        {
            return _context.Orders.Where(o => o.TableId == tableId).ToList();
        }

        public decimal GetTotalOrdersForDay(DateTime date)
        {
            return _context.Orders
                .Where(o => o.OrderDate.Date == date.Date)
                .Sum(o => o.OrderItems.Sum(oi => oi.Quantity * oi.MenuItem.Price));
        }
    }

    // Программа
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new RestaurantDbContext())
            {
                //Добавление данных
                var manager = new RestaurantManager(context);

                // Add 10 menu items
                for (int i = 1; i <= 10; i++)
                {
                    manager.AddMenuItem(new MenuItem { Name = $"Dish {i}", Price = (decimal)(i * 5.5) });
                }

                // Add 5 tables
                for (int i = 1; i <= 5; i++)
                {
                    manager.AddTable(new Table { TableNumber = i, Capacity = 4 });
                }

                // Add 15 orders
                for (int i = 1; i <= 15; i++)
                {
                    var order = new Order
                    {
                        TableId = 1,
                        OrderDate = DateTime.Now,
                        Status = "New",
                        OrderItems = new List<OrderItem>()
                        {
                            new OrderItem { MenuItemId = 1, Quantity = i }
                        }
                    };
                    manager.AddOrder(order);
                }

                // Вывод списка заказов для стола 1
                Console.WriteLine("Orders for Table 1:");
                foreach (var order in manager.GetOrdersForTable(1))
                {
                    Console.WriteLine($"Order ID: {order.OrderId}, Date: {order.OrderDate}, Status: {order.Status}");
                }

                // Вывод общей суммы заказов за сегодня
                Console.WriteLine($"Total orders for today: {manager.GetTotalOrdersForDay(DateTime.Now)}");

                // Обновление статуса заказа
                manager.UpdateOrderStatus(1, "Completed");

                // Удаление заказа
                manager.DeleteOrder(2);
            }
        }
    }
}