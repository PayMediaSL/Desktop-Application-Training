using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LearningDesctopApplication.Model
{
    public class DbContext
    {
        [Key]
        public string NIC { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class ApplicationContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<DbContext> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;database=AuthSystem;user=root;password=admin",
                new MySqlServerVersion(new Version(8, 0, 21)));
        }
    }
}
