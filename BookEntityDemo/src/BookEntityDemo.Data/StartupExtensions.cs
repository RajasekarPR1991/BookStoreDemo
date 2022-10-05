using BookEntityDemo.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookEntityDemo.Data
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDBContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BookContext>(x => x.UseSqlServer(configuration.GetConnectionString("DBConnString")));
            services.AddHealthChecks().AddDbContextCheck<BookContext>();
            return services;
        }

        public static IApplicationBuilder ConfigureDBMigration(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using var context = serviceScope.ServiceProvider.GetService<BookContext>();
                context.Database.Migrate();
            }
            app.UseHealthChecks("/health");
            return app;
        }
    }
}
