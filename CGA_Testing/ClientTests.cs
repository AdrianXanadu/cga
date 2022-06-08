using NUnit.Framework;
using CGA_Client.Models;

namespace CGA_Testing
{
    public class ClientTests
    {
        private Quiz _quiz;

        [SetUp]
        public void Setup()
        {
            _quiz = new Quiz();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}