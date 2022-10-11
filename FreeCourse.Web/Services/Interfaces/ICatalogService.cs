using System.Collections.Generic;
using System.Threading.Tasks;
using FreeCourse.Web.Models;

namespace FreeCourse.Web.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<List<CourseViewModel>> GetAllCourseAsync();

        Task<List<CategoryViewModel>> GetAllCategoryAsync();

        Task<List<CourseViewModel>> GetAllCourseByUserIdAsync(string userId);

        Task<bool> DeleteCourseAsync(string courseId);

        Task<CourseViewModel> GetByCourseIdAsync(string courseId);

        Task<bool> CreateCourseAsync(CourseCreateInput courseCreate);

        Task<bool> UpdateCourseAsync(CourseUpdateInput courseUpdate);
    }
}
