using Allure.NUnit;
using NUnit.Framework;
using HelloContainers.Web.Models;

namespace HelloContainers.Tests
{
    [AllureNUnit]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            Quote.Initialize();
        }

        [Test]
        public void WhenGettingRandomQuote_WorksWithoutError()
        {
            Assert.That(Quote.GetRandomQuote().QuoteText != "Something went wrong");
        }

        [Test]
        public void WhenGettingRandomQuote_AuthorIsNotSystem()
        {
            Assert.That(Quote.GetRandomQuote().QuoteText != "System");
        }
    }
}