using System.Globalization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VideoToStreamableTelegramBot.Services;
using VideoToStreamableTelegramBot.Handlers;
using VideoToStreamableTelegramBot.Settings;

namespace VideoToStreamableTelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo("en-US");
            culture.NumberFormat.NumberDecimalSeparator = ".";
            CultureInfo.CurrentCulture = culture;
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostBuilderContext, services) =>
                {
                    services.AddHttpClient();
                    services.Configure<StreamableAccountSettings>(
                        hostBuilderContext.Configuration.GetSection("StreamableAccountSettings"));
                    services.Configure<TelegramBotSettings>(
                        hostBuilderContext.Configuration.GetSection("TelegramBotSettings"));
                    services.AddSingleton<YoutubeDlService>()
                        .AddSingleton<DownloadQueueService>()
                        .AddSingleton<TelegramBotService>()
                        .AddSingleton<StreamableUploadService>()
                        .AddHostedService<BotMessageHandler>();
                });
    }
}
