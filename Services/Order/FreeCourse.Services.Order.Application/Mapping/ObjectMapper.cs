using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace FreeCourse.Services.Order.Application.Mapping
{
    public static class ObjectMapper
    {
        //Lazy - istendiği anda initialize edicek,istendiğinde IMapper initiazlze et
        private static readonly Lazy<IMapper> lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CustomMapping>();
            });

            return config.CreateMapper();
        });

        //ben Mapper ı çağırana kadar üsteki kod çalışmiyacak
        public static IMapper Mapper => lazy.Value;
    }
    
}
