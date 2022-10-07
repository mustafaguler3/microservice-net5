using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Shared.ControllerBases
{
    public class CustomControllerBase : ControllerBase
    {
        public IActionResult CreateActionResultInstance<T>(Response<T> response)
        {
            //hepsi için ayrı ayrı return badrequest, return ok dönmemize gerek yok response içinden ok 200, 404 gelecek
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
