using Microsoft.EntityFrameworkCore;
using TheBank2.Model;
using MyLib;

namespace TheBank2.Data
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<User<int>> Users { get; set; }
        public DbSet<Department<int>> Departments { get; set; }
        public DbSet<Position<int>> Positions { get; set; }
        public DbSet<Client<int>> Clients { get; set; }
        public DbSet<Deposit<int>> Deposits { get; set; }

        /// <summary>
        /// конструктор
        /// </summary>
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS; Database=Bank; Integrated Security=false; User ID=sa; Password=Master1234; Connect Timeout=200; Pooling='true'; Max Pool Size=200; Trust Server Certificate=true; MultipleActiveResultSets=true;");
        }
    }
}
