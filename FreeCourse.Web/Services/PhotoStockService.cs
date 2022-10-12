using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace FreeCourse.Web.Services
{
    public class PhotoStockService : IPhotoStockService
    {
        private readonly HttpClient _httpClient;

        public PhotoStockService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PhotoViewModel> Upload(IFormFile photo)
        {
            if (photo == null || photo.Length <= 0)
            {
                return null;
            }
            //örn dosya name ssafsf.jpg
            var randomFilename = $"{Guid.NewGuid().ToString()}{Path.GetExtension(photo.FileName)}";

            using var ms = new MemoryStream();

            await photo.CopyToAsync(ms);

            var multipartContent = new MultipartFormDataContent();

            multipartContent.Add(new ByteArrayContent(ms.ToArray()),"photo",randomFilename);

            var response = await _httpClient.PostAsync("photos",multipartContent);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            
            var res = await response.Content.ReadFromJsonAsync<Response<PhotoViewModel>>();

            return res.Data;
        }

        public async Task<bool> DeletePhoto(string pictureUrl)
        {
            var response = await _httpClient.DeleteAsync($"photos?photoUrl={pictureUrl}");

            return response.IsSuccessStatusCode;
        }
    }
}
