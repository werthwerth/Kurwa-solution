using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;
using System.Diagnostics;

namespace Final.Models
{
    public class TagsAllModel : BaseModel
    {
        public TagsAllModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public TagsAllModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Taglist = new List<_viewedTags>();
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (Access)
            {
                var _tempTagList = TagEntity.GetAllTags(_db);
                foreach(var _tag in _tempTagList)
                {
                    Taglist.Add(new _viewedTags(_db, _tag));
                }
            }
        }
        public bool Access { get; set; }
        public class _viewedTags
        {
            public _viewedTags(ApplicationContext _db, Tag _tag)
            {
                Id = _tag.Id;
                Text = _tag.Text;
                ArticleCount = ArticleTagEntity.GetCountByTag(_db, _tag);
            }
            public string? Id {  get; set; }
            public string? Text { get; set; }
            public int ArticleCount { get; set; }
        }
        public List<_viewedTags> Taglist { get; set; }
    }
}
