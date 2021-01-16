using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using testingproject.Models;

namespace testingproject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult AboutBot()
        {
            return View();
        }

        public ActionResult RegisterTeacher()
        {
            return View();
        }

        public ActionResult RegisterStudent()
        {
            return View();
        }

        public ActionResult StudentMain()
        {
            return View();
        }

        public ActionResult TeacherMain()
        {
            return View();
        }

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}


        [HttpPost]
        public ActionResult Login(IFormCollection formData)
        {
            string email = formData["txtLoginID"].ToString().ToLower();
            string password = formData["txtPassword"].ToString();

            if (email == "student@gmail.com" && password == "password")
            {
                return RedirectToAction("StudentMain");
            }

            else if (email == "teacher@gmail.com" && password == "password")
            {
                return RedirectToAction("TeacherMain");
            }

            else
            {
                // Store an error message in TempData for display at the index view
                TempData["Message"] = "Invalid Login Credentials!";
                // Redirect user back to the index view through an action
                return RedirectToAction("Index");
            }

        }
    }
}


