using Final.EFW.Database;
using Final.Models;
using Final.Static;
using Final.Static.EntitiesScripts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NLog;
using System.Diagnostics;
using static Final.EFW.Database.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Final.Controllers
{
    public class HomeController(ILogger<ArticlesController> _logger, ApplicationContext _db) : Controller
    {
        private Logger logger = LogManager.GetCurrentClassLogger();
        public IActionResult Index()
        {
            logger.Debug(string.Format("method: {0} | controller: {1} | action: {2} | sessionId: {3}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.Request.Cookies["sessionId"]));
            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                this.Response.Cookies.Append("sessionId", _sessionId);
            }
            IndexModel _IndexModel = new IndexModel(_sessionId, _db);
            return View("/Views/Home/Index.cshtml", _IndexModel);
        }
        public IActionResult Exit()
        {

            logger.Debug(string.Format("method: {0} | controller: {1} | action: {2} | sessionId: {3}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.Request.Cookies["sessionId"]));
            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                SessionScripts.End(_sessionId, _db);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
