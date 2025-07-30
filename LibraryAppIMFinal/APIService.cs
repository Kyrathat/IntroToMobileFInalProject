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
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("http://10.0.2.2:5280/")
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
            var response = await _httpClient.GetAsync("Medias");
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<List<Media>>(json, _jsonOptions);
        }

        public async Task<Media> PostMediaAsync(Media newMedia)
        {
            var _jsonContent = new StringContent(JsonSerializer.Serialize(newMedia, _jsonOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Medias", _jsonContent);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<Media>(json, _jsonOptions);
        }

        public async Task<Media> UpdateMediaAsync(int id, Media newMedia)
        {
            var _jsonContent = new StringContent(JsonSerializer.Serialize(newMedia, _jsonOptions), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"medias/{id}", _jsonContent);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<Media>(json, _jsonOptions);
        }

        public async Task<bool> DeleteMediaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"medias/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
