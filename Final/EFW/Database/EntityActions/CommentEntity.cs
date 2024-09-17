

using Final.EFW.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using static Final.EFW.Database.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Final.EFW.Database.EntityActions
{
    public class CommentEntity
    {
        protected internal static List<Comment> GetByArticle(ApplicationContext _db, Article _article)
        {
            return _db.Comments.Where(x => x.Article == _article).ToList();
        }
        protected internal static void Add(ApplicationContext _db, Article _article, User _author, string _text)
        {
            _db.Comments.Add(new Comment {  Article = _article, Author = _author , Text = _text});
            _db.SaveChanges();
        }
        protected internal static Comment? GetById(ApplicationContext _db, string _id)
        {
            return _db.Comments.FirstOrDefault(x => x.Id == _id);
        }
        protected internal static void Update(ApplicationContext _db, Comment? _comment, string _text)
        {
            if (_comment != null)
            {
                _comment.Text = _text;
                _db.Comments.Update(_comment);
                _db.SaveChanges();
            }
        }
        protected internal static void Update(ApplicationContext _db, string _commentid, string _text)
        {
            Update(_db, GetById(_db, _commentid), _text); 
        }
        protected internal static void Delete(ApplicationContext _db, Comment _comment)
        {
            _db.Comments.Where(x => x.Id == _comment.Id).ExecuteDelete();
            _db.SaveChanges();
        }
        protected internal static void Delete(ApplicationContext _db, string _commentId)
        {
            _db.Comments.Where(x => x.Id == _commentId).ExecuteDelete();
            _db.SaveChanges();
        }
    }
}