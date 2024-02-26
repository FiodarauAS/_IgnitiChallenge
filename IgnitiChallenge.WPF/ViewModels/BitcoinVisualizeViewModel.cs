using IgnitiChallenge.BitcoinDataApi.Helpers;
using IgnitiChallenge.BitcoinDataApi.Interfaces;
using IgnitiChallenge.BitcoinDataApi.Model;
using IgnitiChallenge.WPF.Command;
using IgnitiChallenge.WPF.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace IgnitiChallenge.WPF.ViewModels
{
    public class BitcoinVisualizeViewModel : ViewModelBase
    {
        private double baseTimerInterval = 24000 * 60 * 60;

        private string _startDate;
        public string StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                SetProperty(ref _startDate, value);
            }
        }

        private string _endDate;
        public string EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                SetProperty(ref _endDate, value);
            }
        }

        private double _btcAveragePrice;
        public double BtcAveragePrice
        {
            get => _btcAveragePrice;
            set
            {
                SetProperty(ref _btcAveragePrice, value);
            }
        }

        private string _lastUpdated;
        public string LastUpdated
        {
            get { return _lastUpdated; }
            set
            {
                SetProperty(ref _lastUpdated, value);
            }
        }

        public ObservableCollection<BitcoinDataModel> BitcoinData { get; set; }
        public ICommand CalculateAverage { get; set; }
        public ICommand UpdateData { get; set; }

        private bool _canExecute = true;
        private Timer dataUpdateTimer;
        private readonly IBitcoinPriceService _bitcoinPriceService;
        private readonly IBitcoinDataUpdateService _bitcoinDataUpdateService;
        private readonly ILogger _logger;

        public BitcoinVisualizeViewModel(IBitcoinPriceService bitcoinPriceService,
                                         IBitcoinDataUpdateService bitcoinDataUpdateService,
                                         ILogger logger)
        {
            UpdateData = new RelayCommand(UpdateBitcoinData, CanExecuteCommand);
            CalculateAverage = new RelayCommand(CalculateAvarage);

            _bitcoinPriceService = bitcoinPriceService;
            _bitcoinDataUpdateService = bitcoinDataUpdateService;
            _logger = logger;

            BitcoinData = new ObservableCollection<BitcoinDataModel>();

            LoadBitcoinData();
            ConfigureTimer();
        }
        private void ConfigureTimer()
        {
            dataUpdateTimer = new Timer();
            dataUpdateTimer.Elapsed += UpdateBitcoinDataTimerElapsed;
            dataUpdateTimer.AutoReset = true;

            double nextUpdate = _bitcoinDataUpdateService.IntervalTillNextUpdate();

            if (nextUpdate >= baseTimerInterval)
            { 
                UpdateData.Execute(null);
                dataUpdateTimer.Interval = baseTimerInterval;
            }
            else
            {
                dataUpdateTimer.Interval = nextUpdate;
            }

            dataUpdateTimer.Start();
        }
        private void UpdateLastUpdatedTime()
        {
            LastUpdated = Helper.LocalTimeFormattedString();
            _logger.Info($"Bitcoin prices update at {LastUpdated}");
            _canExecute = true;
        }
        private void UpdateBitcoinDataTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            UpdateData.Execute(null);

            _bitcoinDataUpdateService.UpdateData();

            dataUpdateTimer.Interval = baseTimerInterval;
        }
        private async void UpdateBitcoinData(object parameter)
        {
            await Application.Current.Dispatcher.Invoke(async () =>
            {
                _canExecute = false;

                BitcoinData.Clear();

                var result = await _bitcoinPriceService.GetPriceHistoryForLast31Days();

                foreach (var item in result)
                {
                    BitcoinData.Add(item);
                }

                UpdateLastUpdatedTime();
            });
        }
        private async void CalculateAvarage(object parameter)
        {
            if (string.IsNullOrEmpty(StartDate) && string.IsNullOrEmpty(EndDate))
            {
                DialogsHelper.DatesMissingValuesDialog();
                return;
            }

            if (string.IsNullOrEmpty(StartDate))
            {
                DialogsHelper.StartDateMissingValueDialog();
                return;
            }

            if (string.IsNullOrEmpty(EndDate))
            {
                DialogsHelper.EndDateMissingValueDialog();
                return;
            }

            DateTime stDate = DateTime.Parse(StartDate);
            DateTime endDate = DateTime.Parse(EndDate);

            if (stDate <= endDate)
            {
                var result = await _bitcoinPriceService.GetPriceHistoryForTimePeriod(StartDate, EndDate);

                double avgPricePeriod = result.Select(s => s.Close).Sum();

                BtcAveragePrice = avgPricePeriod / result.Count;

                _logger.Info($"Average bitcoin price from {StartDate} to {EndDate} is {BtcAveragePrice}");
            }
            else
            {
                DialogsHelper.StartLaterThanEndErrorDialog();
            }
        }
        private async void LoadBitcoinData()
        {
            var result = await _bitcoinPriceService.GetPriceHistoryForLast31Days();

            foreach (var item in result)
            {
                BitcoinData.Add(item);
            }

            UpdateLastUpdatedTime();
        }
        private bool CanExecuteCommand(object parameter)
        {
            return _canExecute;
        }
    }
}
