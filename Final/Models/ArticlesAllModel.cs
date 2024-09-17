using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class ArticlesAllModel : BaseModel
    {
        public ArticlesAllModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public ArticlesAllModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (isLogged)
            {
                Articlelist = new List<ArticleWithTags>();
                var _tempArticleList = ArticleEntity.GetAll(_db);
                foreach (var _tempArticle in _tempArticleList)
                {
                    Articlelist.Add(new ArticleWithTags(_tempArticle, ArticleTagEntity.GetByArticle(_db, _tempArticle)));
                }
            }
        }
        public bool Access { get; set; }
        public class ArticleWithTags : Article
        {
            public ArticleWithTags(Article _article, List<Tag?>? _articleTags)
            {
                Id = _article.Id;
                CreateDate = _article.CreateDate;
                Subject = _article.Subject;
                Text = _article.Text;
                Author = _article.Author;
                ArticleTagList = _articleTags;
            }
            public List<Tag?>? ArticleTagList { get; set; }
        }
        public List<ArticleWithTags> Articlelist { get; set; }
    }
}
