using IgnitiChallenge.BitcoinDataApi.Interfaces;
using IgnitiChallenge.BitcoinDataApi.Services;
using IgnitiChallenge.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace IgnitiChallenge.WPF
{
    public partial class App : Application
    {
        private IServiceCollection services;

        protected override void OnExit(ExitEventArgs e)
        {
            IServiceProvider serviceProvider = services.BuildServiceProvider();

            var loggerService = serviceProvider.GetRequiredService<ILogger>();

            loggerService.Info("Application shutting down.");
            loggerService.Shutdown();

            base.OnExit(e); 
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            IServiceProvider serviceProvider = CreateSerivceProvider();

            MainWindow window = serviceProvider.GetRequiredService<MainWindow>();
            window.Show();

            base.OnStartup(e);
        }

        private IServiceProvider CreateSerivceProvider()
        {
            services = new ServiceCollection();

            services.AddSingleton<ILogger, LoggerService>();
            services.AddSingleton<IBitcoinPriceService, BitcoinPriceService>();
            services.AddSingleton<IBitcoinDataUpdateService, BitcoinDataUpdateService>();

            services.AddScoped<BitcoinVisualizeViewModel>();
            services.AddScoped<MainViewModel>();
            services.AddScoped<MainWindow>(s => new MainWindow(s.GetRequiredService<MainViewModel>()));

            return services.BuildServiceProvider();
        }
    }
}
