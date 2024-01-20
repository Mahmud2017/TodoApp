using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace Todo.DataAccess.Models
{
    public class TaskDbContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DbSet<ATask> Tasks { get; set;}

        public TaskDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("default");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString, new MySqlServerVersion(new Version(8, 0, 19)));
        }
    }
}
