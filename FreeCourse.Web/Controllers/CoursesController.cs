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
    }
}
