using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using WpfCryptoTracker.Models;

namespace WpfCryptoTracker.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://api.coincap.io/v2/")
            };
            _apiKey = "c2870ba2-fb03-4e00-8241-ad1af327f216"; //expires on 06.06.2035
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<List<CryptoCurrency>> GetTopCryptoCurrencies()
        {
            var response = await _httpClient.GetStringAsync("assets?limit=10");
            Console.WriteLine("API Response: " + response);

            var cryptoCurrencies = JsonConvert.DeserializeObject<ApiResponse>(response)?.Data;

            return cryptoCurrencies;
        }
    }

    public class ApiResponse
    {
        public List<CryptoCurrency> Data { get; set; }
    }
}
