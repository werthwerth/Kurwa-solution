using Final.EFW.Database;
using Final.Models;
using Final.Static;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using static Final.EFW.Database.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Final.Controllers
{
    public class LoginController(ILogger<ArticlesController> _logger, ApplicationContext _db) : Controller
    {

        [HttpGet]
        public IActionResult Login()
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | sessionId: {3}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.Request.Cookies["sessionId"]));

            var _LoginModel = new LoginModel();
            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                _LoginModel = new LoginModel(_sessionId, _db);
            }
            if (!System.String.IsNullOrEmpty(_LoginModel.sessionId))
            {
                this.Response.Cookies.Append("sessionId", _LoginModel.sessionId);
            }
            return View("Login", _LoginModel);
        }
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | sessionId: {3}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.Request.Cookies["sessionId"]));
            string? _sessionId = this.Request.Cookies["sessionId"];
            LoginModel _LoginModel = new LoginModel(login, password, _db);
            if (!System.String.IsNullOrEmpty(_LoginModel.sessionId))
            {
                this.Response.Cookies.Append("sessionId", _LoginModel.sessionId);
            }
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
