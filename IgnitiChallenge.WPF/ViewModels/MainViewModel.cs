using IgnitiChallenge.BitcoinDataApi.Interfaces;
using IgnitiChallenge.WPF.Command;
using IgnitiChallenge.WPF.State;
using System;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IgnitiChallenge.WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public INavigator Navigator { get; set; } = new Navigator();
        public MainViewModel(IBitcoinPriceService bitcoinPriceService, IBitcoinDataUpdateService bitcoinDataUpdateService, ILogger logger)
        {
            Navigator.CurrentViewModel = new BitcoinVisualizeViewModel(bitcoinPriceService, bitcoinDataUpdateService, logger);
        }
    }
}
