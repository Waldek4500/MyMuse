using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http.Json;
using System.Text;

namespace MyMuse.Maui.Services
{
    public class SongsService
    {
        private readonly HttpClient _httpClient;

        public SongsService()
        {
            _httpClient = new()
            {
                Timeout = TimeSpan.FromSeconds(5),
                BaseAddress = new Uri("http://192.168.0.52:5010")
            };
        }

        public async Task<List<SongDto>> GetFeaturedSongsAsync(int count)
        {
            var response = await _httpClient.GetAsync($"/songs/featured/{count}");
            response.EnsureSuccessStatusCode();
                
            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetRecentSongsAsync(int count = 10)
        {
            var response = await _httpClient.GetAsync($"/songs/featured/{count}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetTrendingSongsAsync(int count)
        {
            var response = await _httpClient.GetAsync($"/songs/trending/{count}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetRandomSongsFromBandAsync(int bandId, int count)
        {
            var response = await _httpClient.GetAsync($"/songs/bandRandom/{bandId}/{count}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }
    }
}
