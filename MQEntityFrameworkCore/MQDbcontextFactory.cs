using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace MQEntityFrameworkCore
{
    public class MQDbcontextFactory : IDesignTimeDbContextFactory<MQDbcontext>
    {
        private readonly IConfiguration _configuration;

        public MQDbcontextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public MQDbcontext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MQDbcontext>();
            optionsBuilder.UseNpgsql(_configuration.GetValue("ConnectionStrings:PostgreSql", "Debug"));

            return new MQDbcontext(optionsBuilder.Options, _configuration);
        }
    }
}