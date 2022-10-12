using System;

namespace FreeCourse.Web.Models.Orders
{
    public class OrderItemViewModel
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
        public Decimal Price { get; set; }
    }
}
