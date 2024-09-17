using Final.EFW.Entities;
using Microsoft.EntityFrameworkCore;
using static Final.EFW.Database.Core;
namespace Final.EFW.Database.EntityActions
{
    public class TagEntity
    {
        protected internal static void Add(User? _user, ApplicationContext _db, string _tagName)
        {
            if (_user != null)
            {
                int _count = _db.Tags.Where(x => x.Text == _tagName).Count();
                if (_count < 1)
                {
                    Tag _tag = new Tag();
                    _tag.Var(_tagName, _user);
                    _db.Tags.Add(_tag);
                    _db.SaveChanges();
                }
            }
        }
        protected internal static Tag? GetById(ApplicationContext _db, string _id)
        {
            return _db.Tags.FirstOrDefault(x => x.Id == _id);
        }
        protected internal static List<Tag>? GetAllTags(ApplicationContext _db)
        {
            return _db.Tags.ToList();
        }

        protected internal static void UpdateById(ApplicationContext _db, string _id, string _tagText)
        {
            Tag? _tempTag = _db.Tags.FirstOrDefault(x => x.Id == _id);
            if (_tempTag != null)
            {
                _tempTag.Text = _tagText;
                _db.Tags.Update(_tempTag);
                _db.SaveChanges();
            }
        }
        protected internal static void DeleteById(ApplicationContext _db, string _id)
        {
            _db.Tags.Where(x => x.Id == _id).ExecuteDelete();
            _db.SaveChanges();
        }
    }
}