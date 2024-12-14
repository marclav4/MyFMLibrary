using MyFMLibrary;
using MyFMLibrary.Services;

namespace MyFMLibrary.Test
{
    [TestClass]
    public class BrowsingTest
    {
        private static RadioBrowserService _radioBrowserService;
        public BrowsingTest() 
        {
            
        }

        [TestMethod]
        public async void TestFetching_WithPass()
        {
            int actual = (await _radioBrowserService.GetStations("Uruguay", null)).Count;
            int expected = 15;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public async void TestNameFilter_WithPass()
        {
            int val1 = (await _radioBrowserService.GetStations("Uruguay", null)).Count;
            int val2 = (await _radioBrowserService.GetStations("Uruguay", "FM")).Count;
            Assert.AreNotEqual(val1, val2);
        }

        [TestMethod]
        public async void TestPageLimit_WithPass()
        {
            int actual = (await _radioBrowserService.GetStations("Uruguay", null, 1, 5)).Count;
            int expected = 5;
            Assert.AreEqual(expected, actual);
        }
    }
}