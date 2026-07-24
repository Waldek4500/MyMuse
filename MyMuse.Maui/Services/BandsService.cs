using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace MyMuse.Maui.Services
{
    public class BandsService
    {
        private readonly HttpClient _httpClient;

        public BandsService()
        {
            _httpClient = new()
            {
                Timeout = TimeSpan.FromSeconds(5),
                BaseAddress = new Uri("http://192.168.0.52:5010")
            };
        }

        public async Task<List<BandDto>> GetBandsAsync()
        {
            var response = await _httpClient.GetAsync("/bands/");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<BandDto>>();
            return data ?? [];
        }
    }
}
