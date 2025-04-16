using Business_logic_layer.interfaces;
using Business_logic_layer.Repository;
using Data_access_layer.model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Educational_Platform.Controllers
{
    public class CourseController : Controller
    {
        private readonly IunitofWork _unitOfWork;

        public CourseController(IunitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                var courses = await _unitOfWork.Course.GetAllAsync();

                if (!string.IsNullOrEmpty(searchString))
                {
                    courses = _unitOfWork.Course.searchCourseBytitle(searchString);
                }

                return View(courses);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Course course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Course.AddAsync(course);
                    await _unitOfWork.Save();
                    TempData["SuccessMessage"] = $"Course '{course.Title}' created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View(course);
            }
            catch (Exception ex)
            {
                return View(course);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var course = await _unitOfWork.Course.GetByIdAsync(id);
                if (course == null)
                {
                    return RedirectToAction(nameof(Index));
                }
                return View(course);
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var course = await _unitOfWork.Course.GetByIdAsync(id);
                if (course == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.StatusList = new List<string> { "Active", "Inactive", "Draft" };
                return View(course);
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Course course)
        {
            try
            {
                if (id != course.ID)
                {
                    TempData["ErrorMessage"] = "Course ID mismatch.";
                    return RedirectToAction(nameof(Index));
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.Course.UpdateAsync(course);
                    await _unitOfWork.Save();
                    TempData["SuccessMessage"] = $"Course '{course.Title}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.StatusList = new List<string> { "Active", "Inactive", "Draft" };
                return View(course);
            }
            catch (Exception ex)
            {

                return View(course);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var course = await _unitOfWork.Course.GetByIdAsync(id);
                if (course == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }
                return View(course);
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var course = await _unitOfWork.Course.GetByIdAsync(id);
                if (course == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }

                _unitOfWork.Course.DeleteAsync(course);
                await _unitOfWork.Save();
                TempData["SuccessMessage"] = $"Course '{course.Title}' deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }
    }
}