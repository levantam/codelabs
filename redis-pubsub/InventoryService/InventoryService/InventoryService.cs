using StackExchange.Redis;

namespace InventoryService;
public class InventoryService
{
    private readonly string _redisConnection = "redis server endpoint";
    private readonly string _redisPassword = "password to redis server";
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly string _channelName = "ORDER"; // Channel name to subscribe

    public InventoryService()
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
            Console.WriteLine($"Inventory Service - Received message: {message}");
        });
    }
}
