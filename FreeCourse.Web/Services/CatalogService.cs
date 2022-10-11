using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class CatalogService : ICatalogService
    {
        //delege yazıp bizim yerimize token oluşturacak
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CourseViewModel>> GetAllCourseAsync()
        {
            var response = await _httpClient.GetAsync("courses");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return data.Data;
        }

        public async Task<List<CategoryViewModel>> GetAllCategoryAsync()
        {
            var response = await _httpClient.GetAsync("categories");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();

            return data.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId)
        {
            var response = await _httpClient.GetAsync($"courses/GetAllByUserId/{userId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();

            return data.Data;
        }

        public async Task<bool> DeleteCourseAsync(string courseId)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseId}");

            return response.IsSuccessStatusCode;
        }

        public async Task<CourseViewModel> GetByCourseIdAsync(string courseId)
        {
            var response = await _httpClient.GetAsync($"courses/{courseId}");

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var data = await response.Content.ReadFromJsonAsync<Response<CourseViewModel>>();

            return data.Data;
        }

        public async Task<bool> CreateCourseAsync(CourseCreateInput courseCreate)
        {
            var response = await _httpClient.PostAsJsonAsync<CourseCreateInput>("courses",courseCreate);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdate)
        {
            var response = await _httpClient.PutAsJsonAsync<CourseUpdateInput>("courses", courseUpdate);

            return response.IsSuccessStatusCode;
        }
    }
}
