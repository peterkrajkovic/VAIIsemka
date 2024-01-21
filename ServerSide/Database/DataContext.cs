using Classes;
using Microsoft.EntityFrameworkCore;

namespace ServerApi.Database
{
    public class DataContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Like> Like { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Test> Test { get; set; }
        public DbSet<Verification> Verification { get; set; }
        public DbSet<Follow> Follow { get; set; }
        public DbSet<Session> Session { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                   .AddJsonFile("Database/appsettings.Database.json")
                   .Build();

            // Retrieve the connection string
            bool isTest = configuration.GetValue<bool>("IsTest");
            string connectionString = string.Empty;
            if (isTest)
            {
                connectionString = (string)configuration.GetConnectionString("TestConnection")!.Trim();
            }
            else
            {
                connectionString = (string)configuration.GetConnectionString("DefaultConnection")!.Trim();
            }
            

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
