using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace StackOverflowClone.Data
{
    public class StackOverflowFactory : IDesignTimeDbContextFactory<StackOverflowContext>
    {
        public StackOverflowContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}StackOverflowClone.Web"))
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new StackOverflowContext(config.GetConnectionString("ConnectionString"));
        }

    }
}
