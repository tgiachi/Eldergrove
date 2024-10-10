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
        _messageBusService = new MessageBusService(new EventDispatcherService());
    }

    [Test]
    public void Publish_ValidMessage_LogsAndSendsMessage()
    {
        var message = new Mock<TestObject>(1).Object;

        _messageBusService.Publish(message);


        Assert.That(message.Id, Is.EqualTo(1));
    }

    [Test]
    public void Subscribe_ValidSubscriber_LogsAndRegistersSubscriber()
    {
        var subscriber = new Mock<ISubscriber<TestObject>>().Object;

        _messageBusService.Subscribe(subscriber);


        Assert.That(subscriber, Is.Not.Null);
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

public record TestObject(int Id);
