using Business_logic_layer.interfaces;
using Data_access_layer.model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Educational_Platform.Controllers
{
    public class RevisionController : Controller
    {
        private readonly IunitofWork _unitOfWork;

        public RevisionController(IunitofWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<IActionResult> Index(string searchString)
        {
            try
            {
                var revisions = await _unitOfWork.Revision.GetAllAsync();

                if (!string.IsNullOrEmpty(searchString))
                {
                    revisions = _unitOfWork.Revision.searchCourseBytitle(searchString);
                }

                return View(revisions);
            }
            catch (Exception ex)
            {
                // Log the exception
                TempData["ErrorMessage"] = "An error occurred while retrieving revisions.";
                return RedirectToAction("Error", "Home");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var revision = await _unitOfWork.Revision.GetByIdAsync(id);

                if (revision == null)
                {
                    TempData["ErrorMessage"] = "Revision not found.";
                    return RedirectToAction(nameof(Index));
                }

                return View(revision);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while retrieving revision details.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
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
        public async Task<IActionResult> Create(Revision revision)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _unitOfWork.Revision.AddAsync(revision);
                    await _unitOfWork.Save();
                    TempData["SuccessMessage"] = $"Revision '{revision.Title}' created successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(revision);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while creating the revision.";
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(revision);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var revision = await _unitOfWork.Revision.GetByIdAsync(id);

                if (revision == null)
                {
                    TempData["ErrorMessage"] = "Revision not found.";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(revision);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while loading the edit form.";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Revision revision)
        {
            try
            {
                if (id != revision.ID)
                {
                    TempData["ErrorMessage"] = "Revision ID mismatch.";
                    return RedirectToAction(nameof(Index));
                }

                if (ModelState.IsValid)
                {
                    _unitOfWork.Revision.UpdateAsync(revision);
                    await _unitOfWork.Save();
                    TempData["SuccessMessage"] = $"Revision '{revision.Title}' updated successfully!";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(revision);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while updating the revision.";
                ViewBag.Courses = await _unitOfWork.Course.GetAllAsync();
                return View(revision);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var revision = await _unitOfWork.Revision.GetByIdAsync(id);

                if (revision == null)
                {
                    TempData["ErrorMessage"] = "Revision not found.";
                    return RedirectToAction(nameof(Index));
                }

                return View(revision);
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
                var revision = await _unitOfWork.Revision.GetByIdAsync(id);

                if (revision == null)
                {
                    TempData["ErrorMessage"] = "Revision not found.";
                    return RedirectToAction(nameof(Index));
                }

                _unitOfWork.Revision.DeleteAsync(revision);
                await _unitOfWork.Save();
                TempData["SuccessMessage"] = $"Revision '{revision.Title}' deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the revision.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}