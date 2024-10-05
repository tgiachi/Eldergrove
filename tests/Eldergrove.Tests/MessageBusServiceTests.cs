using Eldergrove.Engine.Core.Services;
using GoRogue.Messaging;
using Moq;
using Serilog;

namespace Eldergrove.Tests;

public class MessageBusServiceTests
{
    private Mock<MessageBus> _mockMessageBus;
    private Mock<ILogger> _mockLogger;
    private MessageBusService _messageBusService;

    [SetUp]
    public void Setup()
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
        _mockMessageBus = new Mock<MessageBus>();
        _mockLogger = new Mock<ILogger>();
        _messageBusService = new MessageBusService();
    }

    [Test]
    public void Publish_ValidMessage_LogsAndSendsMessage()
    {
        var message = new Mock<object>().Object;

        _messageBusService.Publish(message);

        _mockLogger.Verify(logger => logger.Debug("Publishing message {Message}", message.GetType()), Times.Once);
        _mockMessageBus.Verify(bus => bus.Send(message), Times.Once);
    }

    [Test]
    public void Subscribe_ValidSubscriber_LogsAndRegistersSubscriber()
    {
        var subscriber = new Mock<ISubscriber<object>>().Object;

        _messageBusService.Subscribe(subscriber);

        _mockLogger.Verify(logger => logger.Debug("Subscribing to message {Message}", typeof(object)), Times.Once);
        _mockMessageBus.Verify(bus => bus.RegisterSubscriber(subscriber), Times.Once);
    }

    [Test]
    public void Publish_NullMessage_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _messageBusService.Publish<object>(null));
    }

    [Test]
    public void Subscribe_NullSubscriber_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _messageBusService.Subscribe<object>(null));
    }
}
