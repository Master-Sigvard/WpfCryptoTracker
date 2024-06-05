using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
            var jsonDocument = JsonDocument.Parse(response);
            var root = jsonDocument.RootElement;
            var data = root.GetProperty("data");

            var cryptoCurrencies = new List<CryptoCurrency>();

            foreach (var element in data.EnumerateArray())
            {
                var cryptoCurrency = new CryptoCurrency
                {
                    Id = element.GetProperty("id").GetString(),
                    Symbol = element.GetProperty("symbol").GetString(),
                    Name = element.GetProperty("name").GetString(),
                    CurrentPrice = element.GetProperty("priceUsd").GetDouble(),
                    MarketCap = element.GetProperty("marketCapUsd").GetDouble(),
                    TotalVolume = element.GetProperty("volumeUsd24Hr").GetDouble(),
                    PriceChangePercentage24h = element.GetProperty("changePercent24Hr").GetDouble()
                };

                cryptoCurrencies.Add(cryptoCurrency);
            }

            return cryptoCurrencies;
        }
    }
}
