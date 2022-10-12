using System;
using System.Threading.Tasks;
using FreeCourse.Web.Models;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface IPhotoStockService
    {
        Task<PhotoViewModel> Upload(IFormFile photo);

        Task<Boolean> DeletePhoto(string pictureUrl);
    }
}
