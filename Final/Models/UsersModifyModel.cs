using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using static Final.EFW.Database.Core;
using Final.EFW.Entities;
using System.Security.Authentication;
using System.Collections;
using Final.Static;
using System.Security.Cryptography;

namespace Final.Models
{
    public class UsersModifyModel : BaseModel
    {
        public UsersModifyModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public UsersModifyModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            List<Role?>?  UserRoles = UserRoleEntity.GetRolesByUserId(_db, _routes.Values["id"].ToString());
            ContextUser = UserEntity.GetById(_routes.Values["id"].ToString(), _db);
            List<Role>  AllRoles = RoleEntity.GetAll(_db);
            ContextRoles = new List<CurentUserRoles>();
            foreach (var _role in AllRoles)
            {
                CurentUserRoles _tempRole = new CurentUserRoles(_role);
                if (UserRoles.Contains(_role))
                {
                    _tempRole.UserInRole = true;
                }
                else
                {
                    _tempRole.UserInRole = false;
                }
                ContextRoles.Add(_tempRole);
            }
        }
        public UsersModifyModel(string _sessionId, ApplicationContext _db, RouteData _routes, string FirstName, string LastName, string Email, string? Password, List<Role?>? _roleList) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (Access && FirstName != null && LastName != null && Email != null && Password != null)
            {
                User? _contextUser = UserEntity.GetById(_routes.Values["id"].ToString(), _db);
                if (_contextUser != null) {
                    if (_roleList != null && _roleList.Count > 0)
                    {
                        UserRoleEntity.DeleteAllUserRoles(_db, _contextUser);
                        foreach (var _role in _roleList)
                        {
                            if (_role != null)
                            {
                                UserRoleEntity.AddRoleToUser(_db, _contextUser, _role);
                            }
                        }
                    }
                    if (Password.ToLower() == ByteToString.Convert(SHA512.Create().ComputeHash(StringToByte.Convert("******"))))
                    {
                        Password = null;
                    }
                    else
                    {
                        Password = Password.ToLower();
                    }
                    UserEntity.UpdateUser(_db, _contextUser, FirstName, LastName, Email, Password);
                }
            }
            List<Role?>? UserRoles = UserRoleEntity.GetRolesByUserId(_db, _routes.Values["id"].ToString());
            ContextUser = UserEntity.GetById(_routes.Values["id"].ToString(), _db);
            List<Role> AllRoles = RoleEntity.GetAll(_db);
            ContextRoles = new List<CurentUserRoles>();
            foreach (var _role in AllRoles)
            {
                CurentUserRoles _tempRole = new CurentUserRoles(_role);
                if (UserRoles.Contains(_role))
                {
                    _tempRole.UserInRole = true;
                }
                else
                {
                    _tempRole.UserInRole = false;
                }
                ContextRoles.Add(_tempRole);
            }
        }
        public bool Access { get; set; }
        public User? ContextUser { get; set; }
        public List<CurentUserRoles> ContextRoles { get; set; }
        public class CurentUserRoles : Role
        {
            public CurentUserRoles(Role obj)
            {
                UserInRole = false;
                Id = obj.Id;
                CreateDate = obj.CreateDate;
                Name =  obj.Name;
                AcessLevel = obj.AcessLevel;
                Description = obj.Description;
            }
            public bool UserInRole;
        }
    }
}