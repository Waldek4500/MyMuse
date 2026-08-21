 using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace MyMuse.Maui.Services
{
    public class ArtistsService
    {
        private readonly HttpClient _httpClient;

        public ArtistsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ArtistDto>> GetBandsAsync()
        {
            var response = await _httpClient.GetAsync("api/artists");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<ArtistDto>>();
            return data ?? [];
        }
    }
}
