using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;
using static Final.Models.UsersAllModel;

namespace Final.Models
{
    public class UsersAllModel : BaseModel
    {
        public UsersAllModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public UsersAllModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            _viewedUsers = new List<Viewedusers>();
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (Access)
            {
                List<User> _allUsers = UserEntity.GetAll(_db);
                foreach (var _user in _allUsers)
                {
                    _viewedUsers.Add(new Viewedusers(_db, _user));
                }
            }
        }
        public bool Access { get; set; }
        public void ChangeRole(ApplicationContext _db, RouteData _routes, string _roleName, string _roleDescription)
        {
            RoleEntity.ChangeRole(_db, _routes.Values["id"].ToString(), _roleName, _roleDescription);
        }
        public class Viewedusers
        {
            public Viewedusers(ApplicationContext _db, User _user)
            {
                FullName = _user.FirstName + " " + _user.LastName;
                Email = _user.Email;
                Roles = UserRoleEntity.GetRolesByUserId(_db, _user.Id);
                Id = _user.Id;
            }
            public string FullName { get; set; }
            public string? Email { get; set; }
            public List<Role?>? Roles { get; set; }
            public string? Id { get; set; }
        }
        public List<Viewedusers> _viewedUsers { get; set; }
    }
}
