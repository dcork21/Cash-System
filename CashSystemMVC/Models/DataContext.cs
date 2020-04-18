using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Contracts;
using CashSystemMVC.Views;

namespace CashSystemMVC.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Atm> Atms { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<Identity> Identities { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
    }
}