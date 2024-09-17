using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using static Final.EFW.Database.Core;
using Final.EFW.Entities;

namespace Final.Models
{
    public class ArticlesAddModel : BaseModel
    {

        public ArticlesAddModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public ArticlesAddModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            var _tags = TagEntity.GetAllTags(_db);
            if (_tags != null)
            {
                TagList = _tags;
            }
        }
        public ArticlesAddModel(string _sessionId, ApplicationContext _db, RouteData _routes, List<Tag> _tagList, string _subject, string _text) : base(_sessionId, _db)
        {
            var _access = AccessScripts.CheckAccess(_db, base.user, _routes);

            var _newArticle = ArticleEntity.Add(_db, _subject, _text, base.user);
            foreach (var _tag in _tagList)
            {
                ArticleTagEntity.Add(_db, _tag, _newArticle);
            }
            var _tags = TagEntity.GetAllTags(_db);
            if (_tags != null)
            {
                TagList = _tags;
            }

            Access = _access;
        }
        public ArticlesAddModel(string _sessionId, ApplicationContext _db, RouteData _routes, string _subject, string _text) : base(_sessionId, _db)
        {
            var _access = AccessScripts.CheckAccess(_db, base.user, _routes);

            ArticleEntity.Add(_db, _subject, _text, base.user);
            var _tags = TagEntity.GetAllTags(_db);
            if (_tags != null)
            {
                TagList = _tags;
            }

            Access = _access;
        }
        public bool Access { get; set; }
        public List<Tag> TagList { get; set; }
    }
}

