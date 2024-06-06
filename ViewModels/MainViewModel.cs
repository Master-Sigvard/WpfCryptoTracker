using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfCryptoTracker.Models;
using WpfCryptoTracker.Services;

namespace WpfCryptoTracker.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly ApiService _apiService;
        private ObservableCollection<CryptoCurrency> _cryptoCurrencies;
        private CryptoCurrency _selectedCurrency;

        public MainViewModel()
        {
            _apiService = new ApiService();
            LoadDataCommand = new RelayCommand(async _ => await LoadData());

            LoadData();
        }

        public ObservableCollection<CryptoCurrency> CryptoCurrencies
        {
            get => _cryptoCurrencies;
            set
            {
                _cryptoCurrencies = value;
                OnPropertyChanged();
            }
        }

        public CryptoCurrency SelectedCurrency
        {
            get => _selectedCurrency;
            set
            {
                _selectedCurrency = value;
                OnPropertyChanged();
                // Navigate to detail view or load detail information
            }
        }

        public RelayCommand LoadDataCommand { get; }

        private async Task LoadData()
        {
            try
            {
                var data = await _apiService.GetTopCryptoCurrencies();
                CryptoCurrencies = new ObservableCollection<CryptoCurrency>(data);
                Debug.WriteLine("Data loaded successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
            }
        }

    }
}
