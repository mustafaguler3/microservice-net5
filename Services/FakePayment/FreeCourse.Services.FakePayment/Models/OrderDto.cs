﻿using System;
using System.Collections.Generic;

namespace FreeCourse.Services.FakePayment.Models
{
    public class OrderDto
    {
        public OrderDto()
        {
            OrderItems = new List<OrderItem>();
        }

        public string BuyerId { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public AddressDto Address { get; set; }
    }

    public class OrderItem
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public Decimal Price { get; set; }
    }

    public class AddressDto
    {
        public string Province { get; set; }
        public string District { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string Line { get; set; }
    }
}
