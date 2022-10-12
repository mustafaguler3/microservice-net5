using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Web.Models.FakePayment;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly HttpClient _httpClient;

        public PaymentService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> ReceivePayment(PaymentInfoInput paymentInfoInput)
        {
            var response = await _httpClient.PostAsJsonAsync<PaymentInfoInput>("fakepayments",paymentInfoInput);

            return response.IsSuccessStatusCode;
        }
    }
}
