using Final.EFW.Database;
using Final.Static.EntitiesScripts;
using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class RegisterModel : BaseModel
    {
        public RegisterModel() : base() { }
        public RegisterModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db) { }
        public RegisterModel(string _login, string _password, string _firstName, string _lastName, string _email, ApplicationContext _db, string? _sessionId)
        {
            RegisterButtonName = $"{_firstName} {_lastName}";
            RegisterButtonController = "Profile";
            RegisterButtonAction = "Profile";
            LoginButtonName = "Выйти";
            LoginButtonController = "Home";
            LoginButtonAction = "Exit";
            _registrationResult = UserScripts.Register(_login, _db, _password, _firstName, _lastName, _email, _sessionId);
            isLogged = _registrationResult.success;
            firstName = _registrationResult.firstName;
            lastName = _registrationResult.lastName;
            sessionId = _registrationResult.newSessionId;
        }
        public RegistrationResult? _registrationResult { get; set; }

    }
}
