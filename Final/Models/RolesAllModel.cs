using Final.EFW.Database.EntityActions;
using Final.EFW.Database;
using Final.EFW.Entities;
using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class RolesAllModel : BaseModel
    {
        public RolesAllModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db)
        {
            Access = false;
        }
        public RolesAllModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
            if (Access)
            {
                Rolelist = RoleEntity.GetAll(_db);
            }
        }
        public bool Access { get; set; }

        public List<Role> Rolelist { get; set; }
    }
}
