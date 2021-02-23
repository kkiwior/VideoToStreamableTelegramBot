using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using VideoToStreamableTelegramBot.Models;
using VideoToStreamableTelegramBot.Settings;

namespace VideoToStreamableTelegramBot.Services
{
    public class StreamableUploadService
    {
        private IHttpClientFactory _httpClient;
        private string _token;

        public StreamableUploadService(IHttpClientFactory httpClient, IOptions<StreamableAccountSettings> options)
        {
            _httpClient = httpClient;
            var byteArray = Encoding.ASCII.GetBytes($"{options.Value.Login}:{options.Value.Password}");
            _token = Convert.ToBase64String(byteArray);
        }

        public async Task<string> Upload(string name)
        {
            try
            {
                Console.WriteLine("Starting upload...");
                var client = _httpClient.CreateClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", _token);
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.streamable.com/upload");
                var content = new MultipartFormDataContent();
                FileStream fs = File.OpenRead("/app/video.mp4");
                content.Add(new StreamContent(fs), "file", name);
                request.Content = content;

                HttpResponseMessage response = await client.SendAsync(request);
                StreamableResponseModel body = JsonConvert.DeserializeObject<StreamableResponseModel>(await response.Content.ReadAsStringAsync());
                fs.Close();
                File.Delete("/app/video.mp4");
                Console.WriteLine("Upload completed.");
                if (body.Status) return body.Shortcode;
                return "Failed uploading.";
            }
            catch (FileNotFoundException e)
            {
                return "Failed fetching.";
            }
        }
    }
}