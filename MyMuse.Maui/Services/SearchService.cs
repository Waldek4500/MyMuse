using MyMuse.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MyMuse.Maui.Services
{
    public class SearchService(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<SearchResultDto>> SearchAsync(string query, int pageSize, int pageNumber, CancellationToken ct)
        {
            var response = await _httpClient.GetAsync($"api/search?SearchFor={query}&PageNumber={pageNumber}&PageSize={pageSize}", ct);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<List<SearchResultDto>>(ct);
            return data ?? [];
        }
    }
}
