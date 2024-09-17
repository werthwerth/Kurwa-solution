using Final.Static.EntitiesScripts;
using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class ExitModel : BaseModel
    {
        public ExitModel() : base() { }
        public ExitModel(string _sessionId, ApplicationContext _db)
        {
            SessionScripts.End(_sessionId, _db);
        }
    }
}
