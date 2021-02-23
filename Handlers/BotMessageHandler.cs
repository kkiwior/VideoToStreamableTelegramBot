using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Telegram.Bot.Args;
using VideoToStreamableTelegramBot.Services;

namespace VideoToStreamableTelegramBot.Handlers
{
    public class BotMessageHandler : BackgroundService
    {
        private readonly TelegramBotService _botService;
        private readonly YoutubeDlService _youtubeDlService;

        public BotMessageHandler(TelegramBotService botService, YoutubeDlService youtubeDlService)
        {
            _botService = botService;
            _youtubeDlService = youtubeDlService;
        }

        private async void BotClient_OnMessage(object sender, MessageEventArgs e)
        {
            var msgParams = e.Message.Text.Split(" ");
            if (Uri.IsWellFormedUriString(msgParams[0], UriKind.Absolute))
            {
                var code = await _youtubeDlService.Download(msgParams[0], (msgParams.Length > 1 ? msgParams[1] : "video"));
                await _botService.BotClient.SendTextMessageAsync(e.Message.Chat.Id, $"http://streamable.com/{code}");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();
            _botService.BotClient.OnMessage += BotClient_OnMessage;
            _botService.BotClient.StartReceiving(cancellationToken: stoppingToken);
            return Task.CompletedTask;
        }
    }
}