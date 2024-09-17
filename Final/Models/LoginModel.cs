using Final.EFW.Database;
using System.Net;
using Final.Static.EntitiesScripts;

using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class LoginModel : BaseModel
    {
        public LoginModel() : base() { }
        public LoginModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db) { }
        public LoginModel(string _login, string _password, ApplicationContext _db)
        {
            _LoginResult = UserScripts.Authorization(_login, _password, _db);
            if(_LoginResult.success)
            {
                RegisterButtonController = "Profile";
                RegisterButtonAction = "Profile";
                LoginButtonName = "Выйти";
                LoginButtonController = "Home";
                LoginButtonAction = "Exit";
                RegisterButtonName = $"{_LoginResult.firstName} {_LoginResult.lastName}";
                isLogged = _LoginResult.success;
                firstName = _LoginResult.firstName;
                lastName = _LoginResult.lastName;
                sessionId = _LoginResult.newSessionId;
            }
            else
            {
                RegisterButtonName = "Регистрация";
                RegisterButtonController = "Register";
                RegisterButtonAction = "Register";
                LoginButtonName = "Войти";
                LoginButtonController = "Login";
                LoginButtonAction = "Login";
                sessionId = null;
            }
        }
        public LoginResult? _LoginResult { get; set; }
    }
}
