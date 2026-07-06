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
        private HttpClient _httpClient;

        public SongsService()
        {
            _httpClient = new();
        }

        public async Task<List<SongDto>> GetFeaturedSongsAsync()
        {
            List<SongDto> songs = [];

            string url = "http://10.0.2.2:5010/songs/featured";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();

                    if (data != null)
                        songs = data;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd: {ex}");
            }

            return songs;
        }

        public async Task<List<SongDto>> GetRecentSongsAsync()
        {
            List<SongDto> songs = [];

            string url = "http://10.0.2.2:5010/songs/featured";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return songs;
                
                var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();

                if (data != null)
                    songs = data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd: {ex}");
            }

            return songs;
        }

        public async Task<List<SongDto>> GetTrendingSongsAsync()
        {
            List<SongDto> songs = [];

            string url = "http://10.0.2.2:5010/songs/trending/5";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return songs;

                var data = await response.Content.ReadFromJsonAsync<List<SongDto>>();

                if (data != null)
                    songs = data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd: {ex}");
            }

            return songs;
        }

        public async Task<List<BandDto>> GetBandsAsync()
        {
            List<BandDto> bands = [];

            string url = "http://10.0.2.2:5010/bands/";

            try
            {
                var response = await _httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                    return bands;

                var data = await response.Content.ReadFromJsonAsync<List<BandDto>>();

                if (data != null)
                    bands = data;

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Błąd: {ex}");
            }

            return bands;
        }
    }
}
