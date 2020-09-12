﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Rubens.Components.Configuration;

namespace Rubens.Components.ControlPlane
{
    public class ControlPlane : IControlPlane
    {
        private readonly HttpClient _httpClient;
        private readonly HttpListener _httpListener;
        private readonly string _rubensConnectionString;

        public ControlPlane(RubensConfiguration rubensConfiguration)
        {
            _rubensConnectionString = rubensConfiguration.ConnectionString;
            _httpListener = new HttpListener();
            _httpClient = new HttpClient();
            // _httpListener.Prefixes.Add("http://localhost:5000");
            // Task.Run(() => _httpListener.Start());
        }

        public EventHandler<EventEmit> Emit { get; set; }

        public async Task Subscribe(string @event)
        {
            await _httpClient.PostAsJsonAsync($"{_rubensConnectionString}/subs",
                new SubscribeToEvent {EventName = @event});
        }

        public async Task Invoke<T>(T @event)
        {
            await _httpClient.PostAsJsonAsync($"{_rubensConnectionString}/subs/emit",
                new EmitEvent {Value = "dsadsa", EventName = "String"});
        }
    }
}