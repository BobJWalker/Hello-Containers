using Octopus.OpenFeature.Provider;
using OpenFeature;
using OpenFeature.Model;
using OpenFeature.Contrib.Providers.EnvVar;

namespace HelloContainers.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            ConfigureOpenFeature(builder).GetAwaiter().GetResult();

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            Models.Quote.Initialize();

            app.Run();
        }

        private static async Task ConfigureOpenFeature(WebApplicationBuilder builder)
        {
            var clientIdentifier = Environment.GetEnvironmentVariable("OPEN_FEATURE_CLIENT_ID");

            if (builder.Environment.IsDevelopment())
            {
                await OpenFeature.Api.Instance.SetProviderAsync(new EnvVarProvider("FeatureToggle_"));
            }
            else
            {
                await OpenFeature.Api.Instance.SetProviderAsync(new OctopusFeatureProvider(new OctopusFeatureConfiguration(clientIdentifier, new ProductMetadata("hello-containers"))));
            }
        }
    }
}
