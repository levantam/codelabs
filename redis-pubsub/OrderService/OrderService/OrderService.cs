using StackExchange.Redis;
using System.Text.Json;

namespace OrderService;
public class OrderService
{
    private readonly string _redisConnection = "redis server endpoint";
    private readonly string _redisPassword = "password to redis server";
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly string _channelName = "ORDER"; // Channel name to send message

    public OrderService()
    {
        var options = ConfigurationOptions.Parse(_redisConnection);
        options.Password = _redisPassword;
        _connectionMultiplexer = ConnectionMultiplexer.Connect(options);
    }
    public async Task PlaceOrder(OrderDto order)
    {
        var redis = _connectionMultiplexer.GetSubscriber();
        Console.WriteLine("OrderService: Send message to channel");
        await redis.PublishAsync(channel: _channelName, JsonSerializer.Serialize(order));
    }
}
