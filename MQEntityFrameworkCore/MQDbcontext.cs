using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MQEntityModal.Modal;
using System;
using System.Configuration;

namespace MQEntityFrameworkCore
{
    public class MQDbcontext : DbContext
    {
        public MQDbcontext()
        {
        }

        public MQDbcontext(DbContextOptions<MQDbcontext> options) : base(options)
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=111111;Host=47.105.139.190;Port=5432;Database=MQdataDb");
        }
    }
}