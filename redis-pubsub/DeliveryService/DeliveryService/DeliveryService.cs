using StackExchange.Redis;

namespace DeliveryService;
public class DeliveryService
{
    private readonly string _redisConnection = "redis server endpoint";
    private readonly string _redisPassword = "password to redis server";
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly string _channelName = "ORDER"; // Channel name to subscribe

    public DeliveryService()
    {
        var options = ConfigurationOptions.Parse(_redisConnection);
        options.Password = _redisPassword;
        _connectionMultiplexer = ConnectionMultiplexer.Connect(options);
    }
    public void SubscribeOrderEvent()
    {
        var conn = _connectionMultiplexer.GetDatabase();
        var redis = _connectionMultiplexer.GetSubscriber();
        redis.Subscribe(_channelName, (channel, message) =>
        {
            Console.WriteLine($"Delivery Service - Received message: {message}");
        });
    }
}
