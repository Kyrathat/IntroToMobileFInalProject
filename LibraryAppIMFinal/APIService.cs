using LibraryAppIMFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LibraryAppIMFinal
{
    public class APIService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public APIService()
        {
            var handler = new HttpClientHandler()
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            };

            _httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://10.0.2.2:7262/api/")
            };

            _jsonOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };
        }

        public async Task<List<Media>> GetMediaAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("Media");
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Media>>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to connect to API: {ex.Message}", ex);
            }
            catch (TaskCanceledException ex)
            {
                throw new Exception("Request timed out. Please check your connection.", ex);
            }
            catch (JsonException ex)
            {
                throw new Exception("Failed to parse server response.", ex);
            }
        }

        public async Task<Media> PostMediaAsync(Media newMedia)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(newMedia, _jsonOptions), Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("Media", jsonContent);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Media>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to create media: {ex.Message}", ex);
            }
        }

        public async Task<Media> UpdateMediaAsync(int id, Media newMedia)
        {
            try
            {
                var jsonContent = new StringContent(JsonSerializer.Serialize(newMedia, _jsonOptions), Encoding.UTF8, "application/json");
                var response = await _httpClient.PutAsync($"Media/{id}", jsonContent);
                response.EnsureSuccessStatusCode();
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Media>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to update media: {ex.Message}", ex);
            }
        }

        public async Task<bool> DeleteMediaAsync(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"Media/{id}");
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException ex)
            {
                throw new Exception($"Failed to delete media: {ex.Message}", ex);
            }
        }
    }
}