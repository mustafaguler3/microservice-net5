using System;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Orders;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly IOrderService _orderService;

        public OrderController(IBasketService basketService, IOrderService orderService)
        {
            _basketService = basketService;
            _orderService = orderService;
        }

        public async Task<IActionResult> Checkout()
        {
            var basket = await _basketService.Get();
            ViewBag.basket = basket;

            return View(new CheckoutInfoInput());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutInfoInput checkoutInfo)
        {
            ///1 yol senkron iletişim
            /// var orderStatus = await _orderService.CreateOrder(checkoutInfo);
            /// 2.yol asenkron iletişim
            var orderSuspend = await _orderService.SuspendOrder(checkoutInfo);
            
            if (!orderSuspend.IsSuccessfull)
            {
                var basket = await _basketService.Get();

                ViewBag.basket = basket;

                TempData["error"] = orderSuspend.Error;
                return RedirectToAction(nameof(Checkout));
            }

            return RedirectToAction(nameof(SuccessfullCheckout), new { orderId = new Random().Next(1,1000) });
        }

        public IActionResult SuccessfullCheckout(int orderId)
        {
            ViewBag.orderId = orderId;
            return View();
        }
    }
}
