using Final.EFW.Database;
using Final.EFW.Database.EntityActions;
using Final.EFW.Entities;
using Microsoft.Extensions.Logging;
using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class ArticlesViewModel : BaseModel
    {
        public ArticlesViewModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public ArticlesViewModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (isLogged)
            {
                Article = ArticleEntity.GetByid(_db, _routes.Values["id"].ToString());
                if(Article != null)
                {
                    TagList = ArticleTagEntity.GetByArticle(_db, Article);
                    CommentList = CommentEntity.GetByArticle(_db, Article);
                }
            }
        }
        public ArticlesViewModel(string _sessionId, ApplicationContext _db, RouteData _routes, string _commentText) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (isLogged)
            {
                Article = ArticleEntity.GetByid(_db, _routes.Values["id"].ToString());
                if (Article != null)
                {
                    CommentEntity.Add(_db, Article, this.user, _commentText);
                    TagList = ArticleTagEntity.GetByArticle(_db, Article);
                    CommentList = CommentEntity.GetByArticle(_db, Article);
                }
            }
        }
        public bool Access { get; set; }
        public Article? Article { get; set; }
        public List<Tag?>? TagList { get; set; }
        public List<Comment> CommentList { get; set; }
    }
}
