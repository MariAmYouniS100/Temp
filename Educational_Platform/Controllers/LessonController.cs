using Business_logic_layer.interfaces;
using Data_access_layer.model;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Platform.Controllers
{
    public class LessonController : Controller
    {
        private readonly IunitofWork _unitOfWork;

        public LessonController(IunitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                var lessons = await _unitOfWork.Lesson.GetAllAsync();
                var lessonsWithCourses = lessons.Select(lesson =>
                {
                    lesson.Course = _unitOfWork.Course.GetByIdAsync(lesson.CourseID).Result;
                    return lesson;
                }).ToList();

                if (!string.IsNullOrEmpty(searchString))
                {
                    lessonsWithCourses = _unitOfWork.Lesson.searchCourseBytitle(searchString).ToList();
                }

                return View(lessonsWithCourses);
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            try
            {
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the create form.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lesson lesson)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Lesson.AddAsync(lesson);
                    await _unitOfWork.Save();
                    TempData["SuccessMessage"] = $"Lesson '{lesson.Title}' created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(lesson);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the Lesson.";
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(lesson);
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var Lesson = await _unitOfWork.Lesson.GetByIdAsync(id);
                if (Lesson == null)
                {
                    TempData["ErrorMessage"] = "Lesson not found.";
                    return RedirectToAction(nameof(Index));
                }
                return View(Lesson);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving Lesson details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var Lesson = await _unitOfWork.Lesson.GetByIdAsync(id);

                if (Lesson == null)
                {
                    TempData["ErrorMessage"] = "Lesson not found.";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.StatusList = new List<string> { "Active", "Inactive", "Draft" };
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(Lesson);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the edit form.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lesson Lesson)
        {
            try
            {
                if (id != Lesson.ID)
                {
                    TempData["ErrorMessage"] = "Lesson ID mismatch.";
                    return RedirectToAction(nameof(Index));
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.Lesson.UpdateAsync(Lesson);
                    await _unitOfWork.Save();
                    TempData["SuccessMessage"] = $"Lesson '{Lesson.Title}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.StatusList = new List<string> { "Active", "Inactive", "Draft" };
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(Lesson);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the Lesson.";
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(Lesson);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var Lesson = await _unitOfWork.Lesson.GetByIdAsync(id);
                if (Lesson == null)
                {
                    TempData["ErrorMessage"] = "Lesson not found.";
                    return RedirectToAction(nameof(Index));
                }
                return View(Lesson);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the delete confirmation.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var Lesson = await _unitOfWork.Lesson.GetByIdAsync(id);
                if (Lesson == null)
                {
                    TempData["ErrorMessage"] = "Lesson not found.";
                    return RedirectToAction(nameof(Index));
                }

                _unitOfWork.Lesson.DeleteAsync(Lesson);
                await _unitOfWork.Save();
                TempData["SuccessMessage"] = $"Lesson '{Lesson.Title}' deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
