namespace GeekShopping.Product.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                #if DEBUG
                    config.AddJsonFile("config/appsettings.Development.json",
                                        optional: true,
                                        reloadOnChange: true);
                #else
                    config.AddJsonFile("config/appsettings.json", 
                                        optional: true, 
                                        reloadOnChange: true);
                #endif
                    config.AddEnvironmentVariables();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}