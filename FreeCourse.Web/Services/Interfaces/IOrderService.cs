using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCourse.Web.Models.Orders;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderCreateViewModel> CreateOrder(CheckoutInfoInput checkoutInfoInput);
        
        Task<OrderSuspendViewModel> SuspendOrder(CheckoutInfoInput checkoutInfoInput);

        Task<List<OrderViewModel>> GetOrder();
    }
}
