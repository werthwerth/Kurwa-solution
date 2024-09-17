using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;
using System.Xml.Linq;

namespace Final.Models
{
    public class RolesModifyModel : BaseModel
    {
        public RolesModifyModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public RolesModifyModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (Access)
            {
                Role = RoleEntity.GetById(_db, _routes.Values["id"].ToString());
            }
        }
        public bool Access { get; set; }
        public Role? Role { get; set; }
        public void ChangeRole(ApplicationContext _db, RouteData _routes, string _roleName, string _roleDescription)
        {
            RoleEntity.ChangeRole(_db, _routes.Values["id"].ToString(), _roleName, _roleDescription);
        }
    }
}
