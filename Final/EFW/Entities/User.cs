using System.ComponentModel.DataAnnotations;
using Final.Static.EntitiesScripts;

namespace Final.EFW.Entities
{
    public class User
    {
        protected internal void Var(string _Login, string _Email, string _PasswordHash, string _FirstName, string _LastName)
        {
            Login = _Login;
            Email = _Email;
            PasswordHash = _PasswordHash;
            FirstName = _FirstName;
            LastName = _LastName;
            
        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Login { get; set; }
        public string? PasswordHash { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set;}
        public string? Email { get; set; }
    }
}