using IMDB.Data.Cart;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace IMDB.Data.ViewComponents
{
    public class ShoppingCartSummary: ViewComponent
    {
        private readonly ShoppingCart _shoppingCart;
        public ShoppingCartSummary(ShoppingCart shoppingCart)
        {
            _shoppingCart = shoppingCart;
        }

        public IViewComponentResult Invoke() 
        {
            var items = _shoppingCart.GetShoppingCartItems();
            var totalAmount = items.Sum(items => items.Amount);
            return View(totalAmount);
        }
    }
}
