using Backend_BPKB.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend_BPKB.DataAccessLayer
{
    public class ConnectionDB : DbContext
    {
        private readonly IConfiguration _configuration;

        public ConnectionDB(DbContextOptions<ConnectionDB> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("DefaultConnection");
        }

        public DbSet<ms_user> ms_user { get; set; }
        public DbSet<tr_bpkb> tr_bpkb { get; set; }


        //public ConnectionDB(DbContextOptions<ConnectionDB> options)
        //    : base(options)
        //{
        //}

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        var configuration = new ConfigurationBuilder()
        //            .SetBasePath(Directory.GetCurrentDirectory())
        //            .AddJsonFile("appsettings.json")
        //            .Build();

        //        var connectionString = configuration.GetConnectionString("DefaultConnection");
        //    }
        //}
    }
}
