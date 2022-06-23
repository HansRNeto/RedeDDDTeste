using Microsoft.EntityFrameworkCore;
using Rede.Infra.CrossCutting.Identity.Data;
using Rede.Infra.Data.Context;

namespace Rede.Application.StartupExtensions;

public static class DatabaseExtension
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddDbContext<AuthDbContext>(options =>
            {
                var connetcion = configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(connetcion, ServerVersion.AutoDetect((configuration.GetConnectionString("DefaultConnection"))));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                // Configuring it to throw an exception when a query is evaluated client side
                // This is no longer logged in Entity Framework Core 3.0.
                // options.ConfigureWarnings(warnings =>
                // {
                //     warnings.Throw(RelationalEventId.QueryClientEvaluationWarning);
                // });
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            services.AddDbContext<RedeContext>(options =>
            {
                var conn = configuration.GetConnectionString("DefaultConnection");
                options.UseMySql(conn, ServerVersion.AutoDetect(conn));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                // if (!env.IsProduction())
                // {
                //     options.EnableDetailedErrors();
                //     options.EnableSensitiveDataLogging();
                // }
            });

            services.AddDbContext<EventStoreContext>(options => {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 21)));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            return services;
        }
    }
