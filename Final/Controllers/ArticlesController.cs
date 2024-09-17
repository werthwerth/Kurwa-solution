using Final.EFW.Database;
using Final.EFW.Database.EntityActions;
using Final.EFW.Entities;
using Final.Models;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System.Diagnostics;
using static Final.EFW.Database.Core;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Final.Controllers
{
    public class ArticlesController(ILogger<ArticlesController> _logger, ApplicationContext _db) : Controller
    {
        [HttpGet]
        public IActionResult Add()
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | sessionId: {3}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.Request.Cookies["sessionId"]));
            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                // Core.DB _db = new Core.DB();
                var _ArticlesAddModel = new ArticlesAddModel(_sessionId, _db, this.RouteData);
                return View("/Views/Articles/Add.cshtml", _ArticlesAddModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpGet]
        public IActionResult Modify()
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | id: {3} | sessionId: {4}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.RouteData.Values["id"].ToString(), this.Request.Cookies["sessionId"]));

            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                var _ArticlesModifyModel = new ArticlesModifyModel(_sessionId, _db, this.RouteData);
                if (_ArticlesModifyModel.Access)
                {
                    return View("/Views/Articles/Modify.cshtml", _ArticlesModifyModel);
                }
                else
                {
                    BaseModel _baseModel = new BaseModel(_sessionId, _db);
                    return View("/Views/Shared/Deny.cshtml", _baseModel);
                }
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public IActionResult Add(string ArticleSubject, string ArticleText)
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | sessionId: {3}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.Request.Cookies["sessionId"]));

            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                List<Tag> _tagList = new List<Tag>();
                foreach (var _tag in this.Request.Form)
                {
                    if (Guid.TryParse(_tag.Key, out var _out) && _tag.Value[0] == "true")
                    {
                        var _tempTag = TagEntity.GetById(_db, _tag.Key);
                        if (_tempTag != null)
                        {
                            _tagList.Add(_tempTag);
                        }
                    }
                }
                ArticlesAddModel _ArticlesAddModel;
                if (ArticleSubject != null && ArticleText != null)
                {
                    if (_tagList.Count > 0)
                    {
                        _ArticlesAddModel = new ArticlesAddModel(_sessionId, _db, this.RouteData, _tagList, ArticleSubject, ArticleText);
                    }
                    else
                    {
                        _ArticlesAddModel = new ArticlesAddModel(_sessionId, _db, this.RouteData, ArticleSubject, ArticleText);
                    }
                }
                else
                {
                    _ArticlesAddModel = new ArticlesAddModel(_sessionId, _db, this.RouteData);
                }
                return View("/Views/Articles/Add.cshtml", _ArticlesAddModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpPost]
        public IActionResult Modify(string ArticleSubject, string ArticleText)
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | id: {3} | sessionId: {4}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.RouteData.Values["id"].ToString(), this.Request.Cookies["sessionId"]));

            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                List<Tag> _tagList = new List<Tag>();
                foreach (var _tag in this.Request.Form)
                {
                    if (Guid.TryParse(_tag.Key, out var _out) && _tag.Value[0] == "true")
                    {
                        var _tempTag = TagEntity.GetById(_db, _tag.Key);
                        if (_tempTag != null)
                        {
                            _tagList.Add(_tempTag);
                        }
                    }
                }
                ArticlesAddModel _ArticlesAddModel;
                if (ArticleSubject != null && ArticleText != null)
                {
                    if (_tagList.Count > 0)
                    {
                        _ArticlesAddModel = new ArticlesAddModel(_sessionId, _db, this.RouteData, _tagList, ArticleSubject, ArticleText);
                    }
                    else
                    {
                        _ArticlesAddModel = new ArticlesAddModel(_sessionId, _db, this.RouteData, ArticleSubject, ArticleText);
                    }
                }
                else
                {
                    _ArticlesAddModel = new ArticlesAddModel(_sessionId, _db, this.RouteData);
                }
                return View("/Views/Articles/Add.cshtml", _ArticlesAddModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public IActionResult All()
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | sessionId: {3}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.Request.Cookies["sessionId"]));

            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                var _ArticlesAllModel = new ArticlesAllModel(_sessionId, _db, this.RouteData);
                return View("/Views/Articles/All.cshtml", _ArticlesAllModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public IActionResult Delete()
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | id: {3} | sessionId: {4}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.RouteData.Values["id"].ToString(), this.Request.Cookies["sessionId"]));

            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                var _ArticlesDeleteModel = new ArticlesDeleteModel(_sessionId, _db, this.RouteData);
                return RedirectToAction("All", "Articles");
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [HttpGet]
        public IActionResult View()
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | id: {3} | sessionId: {4}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.RouteData.Values["id"].ToString(), this.Request.Cookies["sessionId"]));

            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                var _ArticlesViewModel = new ArticlesViewModel(_sessionId, _db, this.RouteData);
                return View("/Views/Articles/View.cshtml", _ArticlesViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }
        [HttpPost]
        public IActionResult View(string CommentText)
        {
            _logger.LogTrace(string.Format("method: {0} | controller: {1} | action: {2} | id: {3} | sessionId: {4}", this.Request.Method, this.RouteData.Values["controller"].ToString(), this.RouteData.Values["action"].ToString(), this.RouteData.Values["id"].ToString(), this.Request.Cookies["sessionId"]));

            string? _sessionId = this.Request.Cookies["sessionId"];
            if (!System.String.IsNullOrEmpty(_sessionId))
            {
                var _ArticlesViewModel = new ArticlesViewModel(_sessionId, _db, this.RouteData, CommentText);
                return View("/Views/Articles/View.cshtml", _ArticlesViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
