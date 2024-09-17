using Final.EFW.Database;
using Final.EFW.Entities;
using Final.Static.EntitiesScripts;
using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            RegisterButtonName = "Регистрация";
            RegisterButtonController = "Register";
            RegisterButtonAction = "Register";
            LoginButtonName = "Войти";
            LoginButtonController = "Login";
            LoginButtonAction = "Login";
            sessionId = null;
        }
        public BaseModel(string _sessionId, ApplicationContext _db)
        {
            if (SessionScripts.CheckById(_sessionId, _db))
            {
                User? _user = SessionScripts.GetUserBySessionId(_sessionId, _db);
                if (_user != null)
                {
                    RegisterButtonName = $"{_user.FirstName} {_user.LastName}";
                    RegisterButtonController = "Profile";
                    RegisterButtonAction = "Profile";
                    LoginButtonName = "Выйти";
                    LoginButtonController = "Home";
                    LoginButtonAction = "Exit";
                    isLogged = true;
                    firstName = _user.FirstName;
                    lastName = _user.LastName;
                    sessionId = _sessionId;
                    user = _user;
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
        public string? RegisterButtonName { get; set; }
        public string? RegisterButtonController { get; set; }
        public string? RegisterButtonAction { get; set; }
        public string? LoginButtonName { get; set; }
        public string? LoginButtonController { get; set; }
        public string? LoginButtonAction { get; set; }
        public bool isLogged { get; set; }
        public string? Login { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? sessionId { get; set; }
        public User? user { get; set; }
    }
}
