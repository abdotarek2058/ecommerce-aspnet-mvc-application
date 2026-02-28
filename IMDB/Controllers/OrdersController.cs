using IMDB.Data;
using IMDB.Data.Cart;
using IMDB.Data.Services;
using IMDB.Data.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace IMDB.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IMoviesService _moviesService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;
        public OrdersController(IMoviesService moviesService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _moviesService = moviesService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }
        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            var orders = await _ordersService.GetOrderByUserIdAndRoleAsync(userId, userRole);
            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var responce = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(responce);
        }
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _moviesService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _moviesService.GetByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction("ShoppingCart");
        }
        public async Task<IActionResult> CompleteOrder(string paymentMethod)
        {
            var items = _shoppingCart.GetShoppingCartItems();

            if (items == null || items.Count == 0)
            {
                return RedirectToAction("ShoppingCart");
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email);

            // تحديد حالة الدفع
            string paymentStatus = paymentMethod == "PayPal" ? "Paid" : "Pending";

            

            await _ordersService.StoreOrderAsync(items, userId, userEmailAddress, paymentMethod, paymentStatus);

            await _shoppingCart.ClearShoppingCartAsync();

            ViewBag.PaymentMethod = paymentMethod;
            ViewBag.PaymentStatus = paymentStatus;

            return View("CompleteOrder");
        }
        public IActionResult EWallet()
        {
            var items = _shoppingCart.GetShoppingCartItems();

            if (items == null || items.Count == 0)
                return RedirectToAction("ShoppingCart");

            var model = new EWalletVM()
            {
                Total = (decimal)_shoppingCart.GetShoppingCartTotal()
            };

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> ConfirmEWallet(EWalletVM model)
        {
            var items = _shoppingCart.GetShoppingCartItems();

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string email = User.FindFirstValue(ClaimTypes.Email);

            await _ordersService.StoreOrderAsync(
                items,
                userId,
                email,
                "EWallet",
                "Pending"
            );

            await _shoppingCart.ClearShoppingCartAsync();

            return View("CompleteOrder");
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);

            if (order == null)
                return NotFound();

            if (order.OrderStatus != "Pending")
                return RedirectToAction(nameof(Index));

            await _ordersService.CancelOrderAsync(id);

            return RedirectToAction(nameof(Index));
        }

    }
}
