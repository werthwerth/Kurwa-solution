using Final.EFW.Entities;
using Final.EFW.Database;
using static Final.EFW.Database.Core;
using Microsoft.EntityFrameworkCore;

namespace Final.EFW.Database.EntityActions
{
    public class ArticleTagEntity
    {
        protected internal static void Add(ApplicationContext _db, Tag _tag, Article _article)
        {
            ArticleTag articleTag = new ArticleTag();
            articleTag.Var(_article, _tag);
            _db.ArticleTags.Add(articleTag);
            _db.SaveChanges();
        }
        protected internal static List<Tag?>? GetByArticle(ApplicationContext _db, Article _article)
        {
            List<Tag?>? _return = _db.ArticleTags.Where(x => x.Article == _article).Select(x => x.Tag).ToList();
            return _return;
        }
        protected internal static void DeleteAllByAtricle (ApplicationContext _db, Article _article)
        {
            _db.ArticleTags.Where(x => x.Article == _article).ExecuteDelete();
            _db.SaveChanges();
        }
        protected internal static int GetCountByTag(ApplicationContext _db, Tag _tag)
        {
            return _db.ArticleTags.Where(x => x.Tag == _tag).Count();
        }
    }
}
