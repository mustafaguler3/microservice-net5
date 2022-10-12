using FreeCourse.Services.FakePayment.Models;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentController : CustomControllerBase
    {

        [HttpPost]
        public IActionResult ReceivePayment(PaymentDto paymentDto)
        {
            return CreateActionResultInstance<NoContent>(Response<NoContent>.Success(200));
        }
    }
}
