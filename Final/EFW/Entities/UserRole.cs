using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class UserRole
    {
        protected internal void Var(User _user, Role _role)
        {
            User = _user;
            Role = _role;
        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}

