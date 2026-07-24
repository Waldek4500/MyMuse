using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace MyMuse.Maui.Services
{
    public class GenresService
    {
        private readonly HttpClient _httpClient;

        public GenresService()
        {
            _httpClient = new()
            {
                Timeout = TimeSpan.FromSeconds(5),
                BaseAddress = new Uri("http://192.168.0.52:5010")
            };
        }

        public async Task<List<GenreDto>> GetGenresAsync()
        {
            var response = await _httpClient.GetAsync("/genres/");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<GenreDto>>();
            return data ?? [];
        }
    }
}
