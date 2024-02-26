using IgnitiChallenge.BitcoinDataApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Threading.Tasks;

namespace IgnitiChallenge.BitcoinDataApi.Services
{
    public class LoggerService : Interfaces.ILogger
    {
        private const string _fileName = "IgnitiBtcLog.log";
        private readonly Logger _logger;
        public LoggerService()
        {
            ConfigureLog();
            _logger = LogManager.GetCurrentClassLogger();
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Shutdown()
        {
            LogManager.Shutdown();
        }

        private void ConfigureLog()
        {
            var config = new NLog.Config.LoggingConfiguration();

            var fileTarget = new NLog.Targets.FileTarget()
            {
                FileName = _fileName,
                Layout = "${date:format=HH\\:mm\\:ss.fff} |${level}| ${message} ${exception:format=ToString}"
            };

            config.AddTarget("file", fileTarget);
            config.AddRule(LogLevel.Trace, LogLevel.Fatal, fileTarget);

            LogManager.Configuration = config;
        }
    }
}
