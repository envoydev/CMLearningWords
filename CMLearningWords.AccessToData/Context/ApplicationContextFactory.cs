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
        public static string Path { get; } = "Server=38b6b917-16a9-473b-be01-a92a00a451e8.sqlserver.sequelizer.com;Database=db38b6b91716a9473bbe01a92a00a451e8;User ID=gqfoikmmqtnyqggz;Password=kHEcg2xD45tDTdANX2v6eAEteSFasVDCpsFY3x5zh7c73y77gbhbZqqSdk6wtwkg;";
        public ApplicationContextFactory() { }

        public ApplicationContext CreateDbContext(string[] args)
        {
            var builderOption = new DbContextOptionsBuilder<ApplicationContext>();
            builderOption.UseSqlServer(Path, option => option.MigrationsAssembly("CMLearningWords.AccessToData"));
            return new ApplicationContext(builderOption.Options);
        }
    }
}
