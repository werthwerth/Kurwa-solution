using Final.EFW.Entities;
using static Final.EFW.Database.Core;

namespace Final.EFW.Database.EntityActions
{
    public class SessionEntity
    {
        protected internal static bool Check(string _sessionId, ApplicationContext _db, User? _user)
        {
            Session? _session = _db.Sessions.FirstOrDefault(x => x.SessionId == _sessionId && x.ExpirationDate > DateTime.UtcNow && x.User == _user) ?? null;
            if (_session == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected internal static bool CheckBySessionId(string _sessionId, ApplicationContext _db)
        {
            Session? _session = _db.Sessions.FirstOrDefault(x => x.SessionId == _sessionId && x.ExpirationDate > DateTime.UtcNow);
            if (_session == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        protected internal static User? GetUserBySessionId(string _sessionId, ApplicationContext _db)
        {
            Session? _session = _db.Sessions.FirstOrDefault(x => x.SessionId == _sessionId && x.ExpirationDate > DateTime.UtcNow) ?? null;
            if (_session == null || _session.User == null)
            {
                return null;
            }
            else
            {
                return _session.User;
            }
        }
        protected internal static void Start(string _sessionId, ApplicationContext _db, User? _user)
        {
            Session _session = new Session();
            _session.Var(DateTime.UtcNow.AddHours(2), _sessionId, _user);
            _db.Sessions.Add(_session);
            _db.SaveChanges();
        }
        protected internal static void End(string _sessionId, ApplicationContext _db)
        {
            Session? _session = _db.Sessions.FirstOrDefault(x => x.SessionId == _sessionId) ?? null;
            if (_session != null)
            {
                _session.ExpirationDate = DateTime.UtcNow;
                _db.Sessions.Update(_session);
                _db.SaveChanges();
            }
        }
    }
}
