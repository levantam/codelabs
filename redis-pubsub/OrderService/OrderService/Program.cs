using OrderService;

var orderService = new OrderService.OrderService();
var newOrder = new OrderDto
{
    Id = 1,
    Description = "Test",
    Name = "Product 01",
    Quantity = 10
};
await orderService.PlaceOrder(newOrder);