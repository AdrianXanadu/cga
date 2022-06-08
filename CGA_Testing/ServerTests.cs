using CGA_Server.Controllers;
using NUnit.Framework;

namespace CGA_Testing
{
    public class ServerTests
    {
        private AdministratorsController _adc;
        private CountriesController _coc;
        [SetUp]
        public void Setup()
        {
            _adc = new AdministratorsController();
            _coc = new CountriesController();
        }

        [TestCase(1)]
        public void CheckAdminAsyncExists(int value)
        {
            var result = _adc.GetAdministrator(value).Result.Value;

            Assert.IsNotNull(result, $"Admin with ID {value} should exist!"); 
            Assert.AreEqual(result.Id, value);
            Assert.AreEqual(result.Name, "Verwalter");
        }

        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        public void CheckAdminNotExists(int value)
        {
            var result = _adc.GetAdministrator(value).Result.Value;

            Assert.IsNull(result, $"Admin with ID {value} should not exist!");
        }

        [TestCase(1)]
        public void CheckCountryExists(int value)
        {
            var result = _coc.GetCountry(value).Result.Value;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.Name, "Austria");
            Assert.AreEqual(result.NameNative, "Österreich");
        }
    }
}