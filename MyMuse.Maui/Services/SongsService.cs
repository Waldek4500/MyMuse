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
        private const string _host = "http://192.168.0.52:5010";

        public SongsService()
        {
            _httpClient = new()
            {
                Timeout = TimeSpan.FromSeconds(5)
            };
        }

        public async Task<List<SongDto>> GetFeaturedSongsAsync()
        {
            var response = await _httpClient.GetAsync($"{_host}/songs/featured");
            response.EnsureSuccessStatusCode();
                
            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetRecentSongsAsync()
        {
            var response = await _httpClient.GetAsync($"{_host}/songs/featured");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetTrendingSongsAsync()
        {
            var response = await _httpClient.GetAsync($"{_host}/songs/trending/5");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<BandDto>> GetBandsAsync()
        {
            var response = await _httpClient.GetAsync($"{_host}/bands/");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<BandDto>>();
            return data ?? [];
        }
    }
}
