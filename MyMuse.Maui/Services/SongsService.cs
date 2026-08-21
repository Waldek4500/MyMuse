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

        public SongsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<SongDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"api/songs/{id}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<SongDto>();

            return data is null ? throw new Exception("There's no song with given id") : data;
        }

        public async Task<List<SongDto>> GetFeaturedAsync(int count = 4)
        {
            var response = await _httpClient.GetAsync($"api/songs?OrderBy=Random&PageSize={count}");
            response.EnsureSuccessStatusCode();
                
            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetRecentAsync(int count = 10)
        {
            var response = await _httpClient.GetAsync($"api/songs?OrderBy=Random&PageSize={count}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetTrendingAsync(int count)
        {
            var response = await _httpClient.GetAsync($"api/songs?OrderBy=PlaysNumber&IsDescending=true&PageSize={count}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }

        public async Task<List<SongDto>> GetRandomFromBandAsync(int artistId, int count)
        {
            var response = await _httpClient.GetAsync($"api/songs?ArtistId={artistId}&PageSize={count}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();
            return data ?? [];
        }
    }
}
