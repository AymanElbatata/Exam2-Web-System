using AYMDating.DAL.Contexts;
using Exam.DAL.BaseEntity;
using Exam.DAL.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Exam.API.Helpers
{
    public class SeedInitialData
    {
        public static async void SeedData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var loggerFactory = services.GetRequiredService<ILoggerFactory>();

                try
                {
                    var context = services.GetRequiredService<ExamDbContext>();
                    await context.Database.MigrateAsync();

                    var userManager = services.GetRequiredService<UserManager<AppUser>>();
                    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();

                    await ExamDbContextSeed.SeedAsync(context, userManager, roleManager, loggerFactory);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }

            }
        }
    }
}
