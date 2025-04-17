using AutoMapper;
using Business_logic_layer.interfaces;
using Data_access_layer.model;
using Microsoft.AspNetCore.Mvc;

namespace Educational_Platform.Controllers
{
    public class LessonController : Controller
    {
        private readonly IunitofWork _unitOfWork;

        public IMapper Mapper { get; }

        public LessonController(IunitofWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            Mapper = mapper;
            this._unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                var Lesson = await _unitOfWork.Lesson.GetAllAsync();

                if (!string.IsNullOrEmpty(searchString))
                {
                    Lesson = _unitOfWork.Lesson.searchCourseBytitle(searchString);
                }

                return View(Lesson);
            }
            catch (Exception ex)
            {

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            try
            {
                return View(new Course { status = "Active" }); // Default status
            }
            catch (Exception ex)
            {
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
                    TempData["SuccessMessage"] = $"Course '{lesson.Title}' created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                return View(lesson);
            }
            catch (Exception ex)
            {
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
                    return RedirectToAction(nameof(Index));
                }
                return View(Lesson);
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
                var Lesson = await _unitOfWork.Lesson.GetByIdAsync(id);
                if (Lesson == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.StatusList = new List<string> { "Active", "Inactive", "Draft" };
                return View(Lesson);
            }
            catch (Exception ex)
            {

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
                    TempData["ErrorMessage"] = "Course ID mismatch.";
                    return RedirectToAction(nameof(Index));
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.Lesson.UpdateAsync(Lesson);
                    await _unitOfWork.Save();
                    TempData["SuccessMessage"] = $"Course '{Lesson.Title}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.StatusList = new List<string> { "Active", "Inactive", "Draft" };
                return View(Lesson);
            }
            catch (Exception ex)
            {

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
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }
                return View(Lesson);
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
                var Lesson = await _unitOfWork.Lesson.GetByIdAsync(id);
                if (Lesson == null)
                {
                    TempData["ErrorMessage"] = "Course not found.";
                    return RedirectToAction(nameof(Index));
                }

                _unitOfWork.Lesson.DeleteAsync(Lesson);
                await _unitOfWork.Save();
                TempData["SuccessMessage"] = $"Course '{Lesson.Title}' deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
