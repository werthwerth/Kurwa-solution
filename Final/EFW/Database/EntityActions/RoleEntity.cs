using Final.EFW.Entities;
using System.Security.Cryptography;
using static Final.EFW.Database.Core;

namespace Final.EFW.Database.EntityActions
{
    public class RoleEntity
    {
        protected internal static void Add(string _name, ApplicationContext _db)
        {
            Add(_name, _db, null);
        }
        protected internal static void Add(string _name, ApplicationContext _context, string? _description)
        {
            Role? _role = _context.Roles.FirstOrDefault(x => x.Name == _name) ?? null;
            if (_role == null)
            {
                int? _maxAccessLevel = _context.Roles.Select(x => x.AcessLevel).Max().GetValueOrDefault(0);

                _role = new Role();
                _role.Var(_name, _maxAccessLevel+1, _description);
                _context.Roles.Add(_role);
                _context.SaveChanges();
            }
        }
        protected internal static Role? GetByName(ApplicationContext _db, string _roleName)
        {
            return _db.Roles.FirstOrDefault(x => x.Name == _roleName);
        }
        protected internal static List<Role> GetAll(ApplicationContext _db)
        {
            return _db.Roles.ToList();
        }
        protected internal static Role? GetById(ApplicationContext _db, string _id)
        {
            return _db.Roles.FirstOrDefault(x => x.Id == _id);
        }
        protected internal static void ChangeRole(ApplicationContext _db, string _roleId, string _roleName, string _roleDescription)
        {
            Role? _role = _db.Roles.FirstOrDefault(x => x.Id == _roleId);
            if (_role != null)
            {
                _role.Name = _roleName;
                _role.Description = _roleDescription;
                _db.Roles.Update(_role);
                _db.SaveChanges();
            }
        }
    }
}
