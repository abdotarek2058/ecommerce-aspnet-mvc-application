using IMDB.Models;
using Microsoft.EntityFrameworkCore;

namespace IMDB.Data.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _context;
        public OrdersService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = _context.Orders.Include(n=>n.OrderItems).ThenInclude(n=>n.Movie).Include(n=>n.User).AsQueryable();

            if(userRole != "Admin")
            {
                orders = orders.Where(n=>n.UserId == userId);
            }

            return await orders.ToListAsync();
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress, string paymentMethod, string paymentStatus)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress,
                PaymentMethod = paymentMethod,
                PaymentStatus = paymentStatus,
                OrderStatus = paymentMethod == "PayPal" ? "Completed" : "Pending",
                OrderDate = DateTime.Now
            };

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach (var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };

                await _context.OrderItems.AddAsync(orderItem);
            }

            await _context.SaveChangesAsync();
        }
        public async Task CancelOrderAsync(int orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

            if (order != null)
            {
                order.OrderStatus = "Cancelled";
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)=> await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
        
    }
}
