using System.IO;
using System.Threading;
using System.Threading.Tasks;
using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if (photo != null && photo.Length>0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photo", photo.FileName);

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream,cancellationToken);

                var returnPath = "photo" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath };

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }

            return CreateActionResultInstance(Response<PhotoDto>.Fail("photo is empty",400));
        }

        [HttpPost]
        public async Task<IActionResult> PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photo", photoUrl);

            if (!System.IO.File.Exists(path))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("photo not found", 404));
            }
            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }

    
}
