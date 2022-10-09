using System;
using FreeCourse.Services.Order.Application.Dtos;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using MediatR;

namespace FreeCourse.Services.Order.Domain.Queries
{
    //handle edince geriye ne dönücemiz IRequeste belitiyoz
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public string UserId { get; set; }
    }
}
