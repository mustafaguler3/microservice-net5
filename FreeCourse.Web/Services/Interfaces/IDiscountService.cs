using System.Threading.Tasks;
using FreeCourse.Web.Models.Discount;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IDiscountService
    {
        Task<DiscountViewModel> GetDiscount(string discountCode);
    }
}
