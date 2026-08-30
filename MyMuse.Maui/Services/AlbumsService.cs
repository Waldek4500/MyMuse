using MyMuse.Shared.Dtos;
using System.Net.Http.Json;

namespace MyMuse.Maui.Services;

public class AlbumsService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<AlbumDto> GetByIdAsync(int id)
    {
        var response = await _httpClient.GetAsync($"api/albums/{id}");
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<AlbumDto>();
        return data is null ? throw new Exception("There's no album with given id") : data;
    }
}
