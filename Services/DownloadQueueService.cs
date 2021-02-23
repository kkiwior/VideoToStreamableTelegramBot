using System;
using System.Threading;
using System.Threading.Tasks;

namespace VideoToStreamableTelegramBot.Services
{
    public class DownloadQueueService
    {
        //private ConcurrentQueue<Func<Task>> _queue;
        private SemaphoreSlim _semaphore;
        
        public DownloadQueueService()
        {
            _semaphore = new SemaphoreSlim(1);
        }

        public async Task<string> Enqueue(Func<Task<string>> task)
        {
            await _semaphore.WaitAsync();
            try
            {
                return await task();
            }
            finally
            {
                _semaphore.Release();
            }
        }
    }
}