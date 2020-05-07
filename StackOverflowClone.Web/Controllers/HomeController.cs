using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackOverflowClone.Data;
using StackOverflowClone.Web.Models;

namespace StackOverflowClone.Web.Controllers
{
    public class HomeController : Controller
    {
        private Repository _repository;
        public HomeController(IConfiguration configuration)
        {
            _repository = new Repository(configuration.GetConnectionString("ConnectionString"));
        }

        public IActionResult Index()
        {
            return View(_repository.GetQuestions());
        }
        public IActionResult QuestionPage(int id)
        {
            return View(new QuestionPageViewModel
            {
                UserID = _repository.GetUserID(User.Identity.Name),
                Question = _repository.GetQuestion(id),
                Answers = _repository.GetAnswers(id)
            });
        }
        public IActionResult AskQuestion()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View(_repository.GetUserID(User.Identity.Name));
            }
            return RedirectToAction("Login");
        }
        [HttpPost]
        public IActionResult AddQuestion(Question question, List<string> tags)
        {
            question.DatePosted = DateTime.Now;
            _repository.AddQuestion(question, tags);
            return Redirect("/");
        }
        [HttpPost]
        public IActionResult LikeQuestion(int questionID)
        {
            _repository.LikeQuestion(new LikedQuestions
            {
                QuestionID = questionID,
                UserID = _repository.GetUserID(User.Identity.Name)
            });
            return Json(true);
        }
        [HttpPost]
        public IActionResult LikeAnswer(int answerID)
        {
            _repository.LikeAnswer(new LikedAnswers
            {
                AnswerID = answerID,
                UserID = _repository.GetUserID(User.Identity.Name)
            });
            return Json(true);
        }
        [HttpPost]
        public IActionResult AddAnswer(Answer answer)
        {
            _repository.AddAnswer(answer);
            return Json(true);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            if (_repository.Login(email, password))
            {
                var claims = new List<Claim>
            {
                new Claim("user", email)
            };
                HttpContext.SignInAsync(new ClaimsPrincipal(
                    new ClaimsIdentity(claims, "Cookies", "user", "role"))).Wait();
            }
            return Redirect("/");
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync().Wait();
            return Redirect("/");
        }
        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SignUp(User user, string password)
        {
            _repository.AddUser(user, password);
            return Redirect("/");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
