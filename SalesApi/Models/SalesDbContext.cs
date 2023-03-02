/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;*/
using Microsoft.EntityFrameworkCore;

namespace SalesApi.Models;

public class SalesDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Orderline> Orderlines { get; set; }
    public DbSet<Item> Items { get; set; }  

    public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }


}
