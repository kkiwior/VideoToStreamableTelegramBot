using Microsoft.Extensions.Options;
using Telegram.Bot;
using VideoToStreamableTelegramBot.Settings;

namespace VideoToStreamableTelegramBot.Services
{
    public class TelegramBotService
    {
        public ITelegramBotClient BotClient { get; }

        public TelegramBotService(IOptions<TelegramBotSettings> settings)
        {
            BotClient = new TelegramBotClient(settings.Value.Token);
        }
    }
}