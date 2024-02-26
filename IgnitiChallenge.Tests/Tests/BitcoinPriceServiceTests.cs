using IgnitiChallenge.BitcoinDataApi.Interfaces;
using IgnitiChallenge.BitcoinDataApi.Models;
using IgnitiChallenge.BitcoinDataApi.Services;
using NSubstitute;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgnitiChallenge.Tests.Tests
{
    [TestFixture]
    public class BitcoinPriceServiceTests
    {
        private IBitcoinPriceService _bitcoinPriceService;
        private ILogger _loggerMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = Substitute.For<ILogger>();
            _bitcoinPriceService = new BitcoinPriceService(_loggerMock);
        }

        [Test]
        public async Task GetPriceHistoryForLast31Days_ReturnsListWith31BitcoinDataItems()
        {
            int expectedCount = 31;

            var bitcoinData = await _bitcoinPriceService.GetPriceHistoryForLast31Days();

            Assert.That(bitcoinData.Count, Is.EqualTo(expectedCount));
        }

        [TestCase("10.02.24", "10.02.24", 1)]
        [TestCase("10.02.24", "12.02.24", 3)]
        public async Task GetPriceHistoryForPeriod_ReturnsListWithBitcoinDataForPeriod(string start, string end, int expectedCount)
        {
            var bitcoinData = await _bitcoinPriceService.GetPriceHistoryForTimePeriod(start, end);

            Assert.That(bitcoinData.Count, Is.EqualTo(expectedCount));
        }

        [TestCase("10.02.24", "09.02.24", 0)]
        [TestCase("00.00.00", "00.00.00", 0)]
        public async Task GetPriceHistoryForIncorrectPeriod_ReturnsListWith0BitcoinData(string start, string end, int expectedCount)
        {
            var bitcoinData = await _bitcoinPriceService.GetPriceHistoryForTimePeriod(start, end);

            Assert.That(bitcoinData.Count, Is.EqualTo(expectedCount));
        }
    }
}
