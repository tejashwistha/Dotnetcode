using Microsoft.AspNetCore.Mvc;
using MyMvcApp.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace MyMvcApp.Controllers
{
    public class AccountController : Controller
    {
        public static List<Signupinfo> lstSignupinfo = new List<Signupinfo>();

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(Signupinfo objSignupinfo)
        {
            if (ModelState.IsValid)
            {
                lstSignupinfo.Add(objSignupinfo);
                return RedirectToAction("SignupUserList");
            }
            return View(objSignupinfo);
        }

        public IActionResult SignupUserList()
        {
            return View(lstSignupinfo);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(Signupinfo LoginInfo)
        {
            var user = lstSignupinfo.Find(u => u.EmailID == LoginInfo.EmailID && u.Password == LoginInfo.Password);
            if (user != null)
            {
                HttpContext.Session.SetString("EmailID", user.EmailID);
                return RedirectToAction("Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(LoginInfo);
            }
        }

        public IActionResult Dashboard()
        {
            var emailID = HttpContext.Session.GetString("EmailID");
            if (string.IsNullOrEmpty(emailID))
            {
                return RedirectToAction("Login");
            }
            ViewBag.EmailID = emailID;
            return View();
        }
    }
}
