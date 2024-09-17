using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class Access
    {
        protected internal void Var(Page _page, Role _role)
        {
            this.Page = _page;
            this.Role = _role;
        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public Page? Page { get; set; }
        public Role? Role { get; set; }
    }
}

