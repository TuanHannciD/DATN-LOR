using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AuthDemo.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            var connectionString =
                Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection")
                ?? "Server=localhost,1433;Database=DATNLor;User Id=sa;Password=YourPassword123!;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);

            var httpContextAccessor = new HttpContextAccessor();

            return new ApplicationDbContext(optionsBuilder.Options, httpContextAccessor);
        }
    }
}