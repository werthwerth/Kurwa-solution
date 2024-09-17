using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;
using System.Diagnostics;

namespace Final.Models
{
    public class ArticlesModifyModel : BaseModel
    {
        public ArticlesModifyModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public ArticlesModifyModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            TagList = new List<CheckedTag>();
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            List<Tag>? _allTags = TagEntity.GetAllTags(_db);
            Article = ArticleEntity.GetByid(_db, _routes.Values["id"].ToString());
            if (Article != null && _allTags != null && _allTags.Count > 0)
            {
                List<Tag?>? _articleTagList = ArticleTagEntity.GetByArticle(_db, Article);
                if (_articleTagList != null)
                {
                    foreach (Tag _tag in _allTags)
                    {
                        if (_articleTagList.Contains(_tag))
                        {
                            CheckedTag _tempCheckedTag = new CheckedTag(_tag, true);
                            TagList.Add(_tempCheckedTag);
                        }
                        else
                        {
                            CheckedTag _tempCheckedTag = new CheckedTag(_tag, false);
                            TagList.Add(_tempCheckedTag);
                        }
                    }
                }
            }
        }
        public ArticlesModifyModel(string _sessionId, ApplicationContext _db, RouteData _routes, List<Tag> _tagList, string _subject, string _text) : base(_sessionId, _db)
        {
            var _access = AccessScripts.CheckAccess(_db, base.user, _routes);

            Article? _changedArticle = ArticleEntity.GetByid(_db, _routes.Values["id"].ToString()); //.Add(_db, _subject, _text, base.user);
            if (_changedArticle != null)
            {
                if (_tagList != null && _tagList.Count > 0)
                {
                    ArticleTagEntity.DeleteAllByAtricle(_db, _changedArticle);
                    foreach (var _tag in _tagList)
                    {
                        ArticleTagEntity.Add(_db, _tag, _changedArticle);
                    }
                    ArticleEntity.Update(_db, _changedArticle, _subject, _text);
                    var _tags = TagEntity.GetAllTags(_db);
                }
            }
            Access = _access;
        }
        public ArticlesModifyModel(string _sessionId, ApplicationContext _db, RouteData _routes, string _subject, string _text) : base(_sessionId, _db)
        {
            var _access = AccessScripts.CheckAccess(_db, base.user, _routes);
            Article? _changedArticle = ArticleEntity.GetByid(_db, _routes.Values["id"].ToString()); //.Add(_db, _subject, _text, base.user);
            ArticleEntity.Update(_db, _changedArticle, _subject, _text);
            var _tags = TagEntity.GetAllTags(_db);

            Access = _access;
        }
        public class CheckedTag : Tag
        {
            public  CheckedTag(Tag _tag, bool _isCheck)
            {
                Id = _tag.Id;
                CreateDate = _tag.CreateDate;
                Text = _tag.Text;
                Author = _tag.Author;
                isCheck = _isCheck;
            }
            public bool isCheck {  get; set; }
        }
        public bool Access { get; set; }
        public List<CheckedTag> TagList { get; set; }
        public Article? Article {  get; set; }
    }
}
