using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Rubens.Components
{
    public class ControlPlane : IControlPlane
    {
        private readonly HttpListener _httpListener;
        private readonly HttpClient _httpClient;
        private readonly string _rubensConnectionString;
        public EventHandler<EventEmit> Emit { get; set; }

        public ControlPlane(string rubensConnectionString)
        {
            _rubensConnectionString = rubensConnectionString;
            _httpListener = new HttpListener();
            _httpClient = new HttpClient();
            // _httpListener.Prefixes.Add("http://localhost:5000");
            // Task.Run(() => _httpListener.Start());
        }

        public async Task Subscribe(string client, string @event)
        {
            try
            {
                Console.WriteLine($"{_rubensConnectionString}/subs");
                await _httpClient.PostAsJsonAsync($"{_rubensConnectionString}/subs",
                    new SubscribeToEvent {EventName = @event});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task Invoke<T>(T @event)
        {
            try
            {
                Console.WriteLine($"{_rubensConnectionString}/subs/emit");
                await _httpClient.PostAsJsonAsync($"{_rubensConnectionString}/subs/emit",
                    new EmitEvent {Value = "dsadsa", EventName = "String"});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}