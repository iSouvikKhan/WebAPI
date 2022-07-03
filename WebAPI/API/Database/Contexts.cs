using API.Models;
using System.Data.Entity;

namespace API.Database
{
    public class Contexts : DbContext
    {
        public Contexts() : base("WebApiDatabase") { }

        public DbSet<User> Users { get; set; }
    }
}