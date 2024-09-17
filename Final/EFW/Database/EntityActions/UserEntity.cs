using Final.EFW.Database;
using System.Xml.Linq;
using Final;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Identity;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
namespace Final.EFW.Database.EntityActions
{
    public class UserEntity
    {      
        protected internal static bool Check(string _login, ApplicationContext _db)
        {
            User? _user = _db.Users.FirstOrDefault(x => x.Login == _login);
            if (_user == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected internal static void Register(string _login, ApplicationContext _db, string _password, string _firstName, string _lastName, string _email, Role? _role = null)
        {
            if (_role == null)
            {
                _role = _db.Roles.FirstOrDefault(x => x.Name == "Пользователи");
            }
            User _user = new User();
            _user.Var(_login, _email, _password, _firstName, _lastName);
            _db.Users.Add(_user);
            _db.SaveChanges();
            if (_role != null)
            {
                UserRoleEntity.AddRoleToUser(_db, _user, _role);
            }
        }
        protected internal static User? GetByLogin(string _login, ApplicationContext _db)
        {
            User? _user = _db.Users.FirstOrDefault(x => x.Login == _login);
            return _user;
        }
        protected internal static User? GetById(string _id, ApplicationContext _db)
        {
            User? _user = _db.Users.FirstOrDefault(x => x.Id == _id);
            return _user;
        }
        protected internal static User? Authorization(string _login, string _password, ApplicationContext _db)
        {
            User? _user = _db.Users.FirstOrDefault(x => x.Login == _login && x.PasswordHash == _password) ?? null;
            return _user;
        }
        protected internal static void UpdateUser(ApplicationContext _db,User _user, string FirstName, string LastName, string Email, string? Password)
        {
            _user.FirstName = FirstName;
            _user.LastName = LastName; 
            _user.Email = Email;
            if (Password != null) 
            {
                _user.PasswordHash = Password;
            }
            _db.Users.Update(_user);
            _db.SaveChanges();
        }
        protected internal static List<User> GetAll (ApplicationContext _db)
        {
            return _db.Users.ToList();
        }
        protected internal static void Delete(ApplicationContext _db, User _user)
        {
            _db.Users.Where(x => x == _user).ExecuteDelete();
            _db.SaveChanges();
        }
        protected internal static void Delete(ApplicationContext _db, string _userId)
        {
            var _user = GetById(_userId, _db);
            if (_user != null)
            {
                Delete(_db, _user);
            }
        }
    }
}
