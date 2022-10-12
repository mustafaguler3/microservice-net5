using System;
using System.Collections.Generic;

namespace FreeCourse.Web.Models.Orders
{
    public class OrderViewModel
    {
        public int Id { get; set; }

        public DateTime CreatedTime { get; set; }
        //ödeme geçmişinde address alanına ihtiyaç olmadığından alınmadı
        public string BuyerId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
