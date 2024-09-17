using Final.EFW.Entities;
using Microsoft.EntityFrameworkCore;
using static Final.EFW.Database.Core;

namespace Final.EFW.Database.EntityActions
{
    public class UserRoleEntity
    {
        protected internal static List<UserRole>? GetByUser (ApplicationContext _db, User _user)
        {
            List<UserRole>? _userList = _db.UserRoles.Where(x => x.User == _user).ToList();
            return _userList;
        }
        protected internal static void AddRoleToUser(ApplicationContext _db, User _user, Role _role)
        {
            UserRole _userRole = new UserRole();
            _userRole.Var(_user, _role);
            _db.Add(_userRole);
            _db.SaveChanges();
        }
        protected internal static void DeleteAllUserRoles(ApplicationContext _db, User _user)
        {
            _db.UserRoles.Where(x => x.User == _user).ExecuteDelete();
            _db.SaveChanges();
        }
        protected internal static List<Role?>? GetRolesByUserId(ApplicationContext _db, string? _userId)
        {
            if (_userId != null)
            {
                User? _user = new User();
                _user = _db.Users.FirstOrDefault(x => x.Id == _userId);
                if (_user != null)
                {
                    List<Role?>? _userRoles = _db.UserRoles.Where(x => x.User == _user).Select(x => x.Role).ToList();
                    if (_userRoles != null && _userRoles.Count > 0)
                    {
                        return _userRoles;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
