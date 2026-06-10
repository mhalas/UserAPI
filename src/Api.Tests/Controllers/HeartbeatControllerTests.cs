using Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Api.Tests.Controllers
{
    [TestFixture]
    public class HeartbeatControllerTests
    {
        private HeartbeatController _controller = null!;

        [SetUp]
        public void SetUp()
        {
            _controller = new HeartbeatController();
        }

        [Test]
        public void HeartbeatStatus_ShouldReturnOkTrue()
        {
            
            var result = _controller.HeartbeatStatus();

            
            Assert.That(result, Is.TypeOf<OkObjectResult>());
            var okResult = (OkObjectResult)result;
            Assert.That(okResult.Value, Is.EqualTo(true));
        }
    }
}
