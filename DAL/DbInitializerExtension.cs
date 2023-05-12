using WebAdminConsole.Models;

namespace WebAdminConsole.DAL
{
    internal static class DbInitializerExtension
    {
        public static async Task<IApplicationBuilder> UseItToSeedSqlServerAsync(this IApplicationBuilder app)
        {
            ArgumentNullException.ThrowIfNull(app, nameof(app));

            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppIdentityDbContext>();

                
                await DbInitializer.SeedAdminUser(context);
                await DbInitializer.SeedPeter(context);
                await DbInitializer.SeedCaptains(context);
                await DbInitializer.Initialize(context);

                //await DbInitializer.SeedRunners(context);


            }
            catch (Exception ex)
            {

            }

            return app;
        }
    }
}
