using Final.EFW.Database.EntityActions;
using Final.EFW.Entities;
using System.Runtime.CompilerServices;
using static Final.EFW.Database.Core;

namespace Final.EFW.Database.EntityActions
{
    public class AccessScripts
    {
        public static bool CheckAccess(ApplicationContext _db, User? _user, RouteData _routes)
        {
            Page? _page = GetPage(_db, _routes);
            var _access = false;
            if (_page != null && _user != null)
            {
                List<UserRole>? _userRoles = UserRoleEntity.GetByUser(_db, _user);
                if (_userRoles != null)
                {
                    List<Access> _pageAccesses = AccessEntity.GetByPage(_db, _page);
                    foreach (var _pageAccess in _pageAccesses)
                    {
                        if (_pageAccess.Role != null)
                        {
                            foreach (var _userRole in _userRoles)
                            {
                                if (_userRole.Role.Id == _pageAccess.Role.Id)
                                {
                                    _access = true;
                                }
                            }
                        }
                    }
                    return _access;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public static Page? GetPage(ApplicationContext _db, RouteData _routes)
        {
            Page? _page = PageEntity.Get(_db, _routes.Values["controller"].ToString(), _routes.Values["action"].ToString());
            return _page;
        }
    }
}
