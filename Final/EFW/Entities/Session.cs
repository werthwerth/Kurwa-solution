using System.ComponentModel.DataAnnotations;

namespace Final.EFW.Entities
{
    public class Session
    {
        protected internal void Var(DateTime _ExpirationDate, string? _SessionId, User? _User)
        {
            ExpirationDate = _ExpirationDate;
            SessionId = _SessionId;
            User = _User;
        }
        [Key]
        public string? Id { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string? SessionId { get; set; }
        public User? User { get; set; }
    }
}
