﻿using Data_access_layer.model;
using Educational_Platform.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Educational_Platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, model.RememberMe);
                    if (result.Succeeded)
                    {
                        List<Claim> claims = new List<Claim>();
                        claims.Add(new Claim("FullName", user.FullName));
                        claims.Add(new Claim("IsActive",user.IsActive? "1" : "0"));
                        claims.Add(new Claim("ProfilePicture", user.ProfilePicture??""));

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User not found.");
                }
            }
            return View(model);
        }
        // Registration for Students
        [HttpGet]

        public IActionResult RegisterStudent()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterStudent(RegisterStudentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign Student role
                    if (!await _roleManager.RoleExistsAsync("Student"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Student"));
                    }
                    await _userManager.AddToRoleAsync(user, "Student");

                    // Create Student entity
                    var student = new Student
                    {

                        Email = model.Email,
                        Name = $"{model.FirstName} {model.LastName}",
                        PhoneNumber = model.PhoneNumber,
                        UserId = user.Id
                    };

                    _context.Students.Add(student);
                    await _context.SaveChangesAsync();

                    // Link ApplicationUser to Student
                    user.StudentId = student.ID;
                    await _userManager.UpdateAsync(user);

                    // Redirect to profile completion
                    return RedirectToAction("EditStudentProfile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // Registration for Instructors
        [HttpGet]
        public IActionResult RegisterInstructor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterInstructor(RegisterInstructorViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign Instructor role
                    if (!await _roleManager.RoleExistsAsync("Instructor"))
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Instructor"));
                    }
                    await _userManager.AddToRoleAsync(user, "Instructor");

                    // Create Instructor entity
                    var instructor = new Instructor
                    {
                        Email = model.Email,
                        Name = $"{model.FirstName} {model.LastName}",
                        UserId = user.Id
                    };

                    _context.Instructors.Add(instructor);
                    await _context.SaveChangesAsync();

                    // Link ApplicationUser to Instructor
                    user.InstructorId = instructor.ID;
                    await _userManager.UpdateAsync(user);

                    // Redirect to profile completion
                    return RedirectToAction("EditInstructorProfile");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // Edit Student Profile
        [HttpGet]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EditStudentProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = _context.Students.FirstOrDefault(s => s.ID == user.StudentId);
            if (student == null) return NotFound();

            var model = new StudentProfileViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                FatherPhone = student.fatherPhone,
                GradeLevel = student.GradeLevel,
                CurrentProfilePicture = user.ProfilePicture
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EditStudentProfile(StudentProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var student = _context.Students.FirstOrDefault(s => s.ID == user.StudentId);
            if (student == null) return NotFound();

            // Update profile picture if uploaded
            if (model.ProfilePictureFile != null)
            {
                string uniqueFileName = await ProcessProfilePictureAsync(model.ProfilePictureFile);
                user.ProfilePicture = uniqueFileName;
                student.ProfilePicture = uniqueFileName;
            }

            // Update user and student details
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.UpdatedAt = DateTime.UtcNow;

            student.Name = $"{model.FirstName} {model.LastName}";
            student.PhoneNumber = model.PhoneNumber;
            student.fatherPhone = model.FatherPhone;
            student.GradeLevel = model.GradeLevel;

            await _userManager.UpdateAsync(user);
            _context.Update(student);
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Profile updated successfully.";
            return RedirectToAction("Index", "Student");
        }

        // Edit Instructor Profile
        [HttpGet]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> EditInstructorProfile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var instructor = _context.Instructors.FirstOrDefault(i => i.ID == user.InstructorId);
            if (instructor == null) return NotFound();

            var model = new InstructorProfileViewModel
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Subjects = instructor.Subjects,
                Qualifications = instructor.Qualifications,
                Bio = instructor.Bio,
                CurrentProfilePicture = user.ProfilePicture
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> EditInstructorProfile(InstructorProfileViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = await _userManager.GetUserAsync(User);
            if (user == null) return NotFound();

            var instructor = _context.Instructors.FirstOrDefault(i => i.ID == user.InstructorId);
            if (instructor == null) return NotFound();

            // Update profile picture if uploaded
            if (model.ProfilePictureFile != null)
            {
                string uniqueFileName = await ProcessProfilePictureAsync(model.ProfilePictureFile);
                user.ProfilePicture = uniqueFileName;
                instructor.Photo = uniqueFileName;
            }

            // Update user and instructor details
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.PhoneNumber = model.PhoneNumber;
            user.UpdatedAt = DateTime.UtcNow;

            instructor.Name = $"{model.FirstName} {model.LastName}";
            instructor.PhoneNumber = model.PhoneNumber;
            instructor.Subjects = model.Subjects;
            instructor.Qualifications = model.Qualifications;
            instructor.Bio = model.Bio;

            await _userManager.UpdateAsync(user);
            _context.Update(instructor);
            await _context.SaveChangesAsync();

            TempData["StatusMessage"] = "Profile updated successfully.";
            return RedirectToAction("Index", "Instructor");
        }

        // Helper method to process profile pictures
        private async Task<string> ProcessProfilePictureAsync(IFormFile profilePicture)
        {
            if (profilePicture == null) return null;

            string uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePicture.FileName;
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/profiles");
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await profilePicture.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            return View("Login");
        }
    }
}