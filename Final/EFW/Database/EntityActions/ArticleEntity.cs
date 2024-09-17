using Final.EFW.Entities;
using Microsoft.EntityFrameworkCore;
using static Final.EFW.Database.Core;

namespace Final.EFW.Database.EntityActions
{
    public class ArticleEntity
    {
        protected internal static Article? Add(ApplicationContext _db, string _subject, string _text, User _user)
        {
            Article _article = new Article();
            _article.Var(_subject, _text, _user);
            _db.Articles.Add(_article);
            _db.SaveChanges();
            return _article;
        }
        protected internal static Article? Add(ApplicationContext _db, string _subject, string _text, string _userId)
        {
            var _user = UserEntity.GetById(_userId, _db);
            if (_user != null)
            {
                return Add(_db, _subject, _text, _userId);
            }
            else
            {
                return null;
            }
        }
        protected internal static Article? GetByid(ApplicationContext _db, string? _id)
        {
            if (_id == null)
            {
                return null;
            }
            else
            {
                Article? _article = _db.Articles.FirstOrDefault(a => a.Id == _id);
                return _article;
            }
        }
        protected internal static void Update(ApplicationContext _db, Article? _article, string _subject, string _text)
        {
            if (_article != null)
            {
                _article.Text = _text;
                _article.Subject = _subject;
                _db.Articles.Update(_article);
                _db.SaveChanges();
            }

        }
        protected internal static List<Article> GetAll(ApplicationContext _db)
        {
            return _db.Articles.ToList();
        }
        protected internal static void DeleteById(ApplicationContext _db, string? _id)
        {
            if (_id != null)
            {
                _db.Articles.Where(x => x.Id == _id).ExecuteDelete();
                _db.SaveChanges();
            }
        }
        protected internal static void Update(ApplicationContext _db, string _articleId, string _subject, string _text)
        {
            Update(_db, ArticleEntity.GetByid(_db, _articleId), _subject, _text);
        }
        protected internal static void Delete(ApplicationContext _db, Article _article)
        {
            _db.Articles.Where(x => x == _article).ExecuteDelete();
            _db.SaveChanges();
        }
        protected internal static void Delete(ApplicationContext _db, string _articleId)
        {
            Delete(_db, GetByid(_db, _articleId));
        }
    }
}