using System;
using System.Threading.Tasks;
using NYoutubeDL;

namespace VideoToStreamableTelegramBot.Services
{
    public class YoutubeDlService
    {
        private YoutubeDL _youtubeDl;
        private DownloadQueueService _queueService;
        private StreamableUploadService _streamableService;

        public YoutubeDlService(DownloadQueueService queueService, StreamableUploadService streamableService)
        {
            _queueService = queueService;
            _streamableService = streamableService;
            _youtubeDl = new();
            _youtubeDl.Options.FilesystemOptions.Output = "/app/video.mp4";
        }

        public async Task<string> Download(string url, string name)
        {
            Console.WriteLine("Queueing download...");
            return await _queueService.Enqueue(async () =>
            {
                Console.WriteLine("Starting download...");
                await _youtubeDl.DownloadAsync(url);
                return await _streamableService.Upload(name);
            });
        }
    }
}