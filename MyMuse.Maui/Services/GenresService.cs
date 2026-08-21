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

        public GenresService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<GenreDto>> GetGenresAsync(int pageSize, int pageNumber = 1)
        {
            var response = await _httpClient.GetAsync($"api/genres?OrderBy=DisplayOrder&IsDescending=true&PageNumber={pageNumber}&PageSize={pageSize}");
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<GenreDto>>();
            return data ?? [];
        }
    }
}
