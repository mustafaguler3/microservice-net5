using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Services.Order.Domain.Core;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    //Ef Core features
    //Owned Types
    //Shadow Property
    //Backing Field
    public class Order : Entity,IAggregateRoot
    {
        public DateTime CreatedTime { get;private set; }
        //ef core order içersinde sütun olarak yaparız yada ayrı bir tablo olarak bunun için address class üzerinde owned kullanırız 
        public Address Address { get;private set; }

        public string BuyerId { get;private set; }
        //ef core içersinde sadece okuma ve yazamayı sadece property üzerinden gerçekleştiriyorsak  - backing field - denir amaç encapsulü arttırma, kimse orderItem a ürün eklemesin, ef core burayı dolduracak 
        private readonly List<OrderItem> _orderItems;
        //dış dünyaya sadece okuma olarak aç
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;

        public Order(string buyerId, Address address)
        {
            _orderItems = new List<OrderItem>();
            CreatedTime = DateTime.Now;
            Address = address;
            BuyerId = buyerId;
        }

        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            var exist = _orderItems.Any(i => i.ProductId == productId);

            if (!exist)
            {
                var newOrderItem = new OrderItem(productId,productName,pictureUrl,price);

                _orderItems.Add(newOrderItem);
            }
        }

        public decimal GetTotalPrice => _orderItems.Sum(i => i.Price);
    }
}
