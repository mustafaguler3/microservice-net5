using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models.Basket;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class BasketService : IBasketService
    {
        private readonly HttpClient _httpClient;
        private readonly IDiscountService _discountService;

        public BasketService(HttpClient httpClient, IDiscountService discountService)
        {
            _httpClient = httpClient;
            _discountService = discountService;
        }

        public async Task<bool> SaveOrUpdate(BasketViewModel basketView)
        {
            var response = await _httpClient.PostAsJsonAsync<BasketViewModel>("baskets",basketView);

            return response.IsSuccessStatusCode;
        }

        public async Task<BasketViewModel> Get()
        {
            var response = await _httpClient.GetAsync("baskets");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var basketViewModel = await response.Content.ReadFromJsonAsync<Response<BasketViewModel>>();
            
            return basketViewModel.Data;
        }

        public async Task<bool> Delete()
        {
            var result = await _httpClient.DeleteAsync("baskets");

            return result.IsSuccessStatusCode;
        }

        public async Task AddBasketItem(BasketItemViewModel basketItemViewModel)
        {
            var basket = await Get();

            if (basket != null)
            {
                if (basket.BasketItems.Any(i=>i.CourseId==basketItemViewModel.CourseId))
                {
                    basket.BasketItems.Add(basketItemViewModel);
                }
            }
            else
            {
                basket = new BasketViewModel();
                basket.BasketItems.Add(basketItemViewModel);
            }

            await SaveOrUpdate(basket);
        }

        public async Task<bool> RemoveBasketItem(string courseId)
        {
            var basket = await Get();

            if (basket == null)
            {
                return false;
            }

            var deleteResult = basket.BasketItems.Remove(basket.BasketItems.First(i => i.CourseId == courseId));

            if (!deleteResult)
            {
                return false;
            }

            if (!basket.BasketItems.Any())
            {
                basket.DiscountCode = null;
            }

            return await SaveOrUpdate(basket);
        }

        public async Task<bool> ApplyDiscount(string discountCode)
        {
            await CancelApplyDiscount();

            var basket = await Get();

            if (basket == null || basket.DiscountCode == null)
            {
                return false;
            }

            var hasDiscount = await _discountService.GetDiscount(discountCode);

            if (hasDiscount == null)
            {
                return false;
            }

            basket.DiscountRate = hasDiscount.Rate;
            basket.DiscountCode = hasDiscount.Code;

            await SaveOrUpdate(basket);

            return true;
        }

        public async Task<bool> CancelApplyDiscount()
        {
            var basket = await Get();

            if (basket == null)
            {
                return false;
            }

            basket.DiscountCode = null;

            await SaveOrUpdate(basket);

            return true;
        }
    }
}
