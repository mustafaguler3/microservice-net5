using System.Threading.Tasks;
using FreeCourse.Shared.Services;
using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FreeCourse.Web.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICatalogService _catalogService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public CoursesController(ICatalogService catalogService, ISharedIdentityService sharedIdentityService)
        {
            _catalogService = catalogService;
            _sharedIdentityService = sharedIdentityService;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _catalogService.GetAllCourseByUserIdAsync(_sharedIdentityService.GetUserId));
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CourseCreateInput courseCreate)
        {
            var categories = await _catalogService.GetAllCategoryAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }

            courseCreate.UserId = _sharedIdentityService.GetUserId;

            await _catalogService.CreateCourseAsync(courseCreate);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(string id)
        {
            var course = await _catalogService.GetByCourseIdAsync(id);
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name",course.Id);

            if (course == null)
            {
                RedirectToAction(nameof(Index));
            }

            CourseUpdateInput courseUpdate = new()
            {
                Id = course.Id,
                Name = course.Name,
                Feature = new FeatureViewModel { Duration = course.Feature.Duration },
                Description = course.Description,
                Price = course.Price,
                CategoryId = course.CategoryId,
                UserId = course.UserId,
                Picture = course.Picture
            };

            return View(courseUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CourseUpdateInput courseUpdate)
        {
            var categories = await _catalogService.GetAllCategoryAsync();
            ViewBag.categoryList = new SelectList(categories, "Id", "Name", courseUpdate.Id);

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _catalogService.UpdateCourseAsync(courseUpdate);

            return RedirectToAction(nameof(Index));
        }
    }
}
