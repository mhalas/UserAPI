using Api.Controllers;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class HeartbeatControllerTests
    {
        private HeartbeatController _controller;

        [SetUp]
        public void Setup()
        {
            _controller = new HeartbeatController();
        }

        [Test]
        public void TestGet_ReturnsOk()
        {
            var result = _controller.HeartbeatStatus();
            Assert.That(result, Is.True);
        }
    }
}
