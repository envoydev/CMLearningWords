using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CMLearningWords.AccessToData.Context
{
    public class ApplicationContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public static string Path { get; } = "";
        public ApplicationContextFactory() { }

        public ApplicationContext CreateDbContext(string[] args)
        {
            var builderOption = new DbContextOptionsBuilder<ApplicationContext>();
            builderOption.UseSqlServer(Path, option => option.MigrationsAssembly("CMLearningWords.AccessToData"));
            return new ApplicationContext(builderOption.Options);
        }
    }
}
