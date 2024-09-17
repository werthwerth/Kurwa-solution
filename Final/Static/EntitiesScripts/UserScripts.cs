using Final.EFW.Database;
using Final.EFW.Database.EntityActions;
using Microsoft.AspNetCore.Identity;
using Final.EFW.Entities;
using System.Runtime.CompilerServices;
using static Final.EFW.Database.Core;

namespace Final.Static.EntitiesScripts
{
    public class UserScripts
    {
        public static RegistrationResult Register(string _login, ApplicationContext _db, string _password, string _firstName, string _lastName, string _email, string? _sessionId)
        {
            if (!UserEntity.Check(_login, _db))
            {
                UserEntity.Register(_login, _db, _password, _firstName, _lastName, _email);
                RegistrationResult _RegistrationResult = new RegistrationResult(_firstName, _lastName);
                User? _user = UserEntity.GetByLogin(_login, _db);
                if (_sessionId == null)
                {
                    _sessionId = RandomString.Generate();
                }
                if (!SessionScripts.Check(_sessionId, _db, _user))
                {
                    
                    string _newSessionId = SessionScripts.Start(_db, _user);
                    _RegistrationResult.newSessionId = _newSessionId;
                }
                return _RegistrationResult;
            }
            else
            {
                RegistrationResult _RegistrationResult = new RegistrationResult();
                return _RegistrationResult;
            }
        }
        public static LoginResult Authorization(string _login, string _password, ApplicationContext _db)
        {
            User? _user = UserEntity.Authorization(_login, _password, _db);
            LoginResult _LoginResult = new LoginResult();
            if (_user != null)
            {
                string _newSessionId = SessionScripts.Start(_db, _user);
                _LoginResult = new LoginResult(_user.FirstName, _user.LastName, _newSessionId);
            }
            return _LoginResult;
        }
    }
    public class RegistrationResult
    {
        public RegistrationResult()
        {
            success = false;
            firstName = "";
            lastName = "";
        }
        public RegistrationResult(string _firstName, string _lastName)
        {
            success = true;
            firstName = _firstName;
            lastName = _lastName;
        }
        public bool success { get; set; }
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string? newSessionId { get; set; }
    }
    public class LoginResult : RegistrationResult 
    {
        public LoginResult() : base() { }
        public LoginResult(string _firstName, string _lastName) :  base(_firstName, _lastName) { }
        public LoginResult(string? _firstName, string? _lastName, string? _sessionId)
        {
            success = true;
            firstName = _firstName;
            lastName = _lastName;
            newSessionId = _sessionId;
        }
    }
}
