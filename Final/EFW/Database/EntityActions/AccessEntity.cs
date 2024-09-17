using Final.EFW.Entities;
using static Final.EFW.Database.Core;

namespace Final.EFW.Database.EntityActions
{
    public class AccessEntity
    {
        protected internal static List<Access> GetByPage(ApplicationContext _db, Page _page)
        {
            List<Access> _pageAccesses = _db.Accesses.Where(x => x.Page == _page).ToList();
            return _pageAccesses;
        }
        protected internal static bool CheckRoleByPageAndRole(ApplicationContext _db, Page _page, Role _role)
        {
            if (_db.Accesses.Where(x => x.Page == _page && x.Role == _role).Count() < 1)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected internal static void Add(ApplicationContext _db, Page _page, Role _role)
        {
            if (!CheckRoleByPageAndRole(_db, _page, _role))
            {
                Access _access = new Access();
                _access.Var(_page, _role);
                _db.Accesses.Add(_access);
                _db.SaveChanges();
            }
        }
    }
}
