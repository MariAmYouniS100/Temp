﻿using Microsoft.AspNetCore.Mvc;

namespace Educational_Platform.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Charts()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
