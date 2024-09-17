﻿using Final.EFW.Database;
using Final.EFW.Database.EntityActions;
using static Final.EFW.Database.Core;

namespace Final.Models
{
    public class TagsAddModel : BaseModel
    {
        public TagsAddModel(string _sessionId, ApplicationContext _db) : base(_sessionId, _db) 
        {
            Access = false;
        }
        public TagsAddModel(string _sessionId, ApplicationContext _db, RouteData _routes) : base(_sessionId, _db)
        {
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
        }
        public TagsAddModel(string _sessionId, ApplicationContext _db, string _tagName, RouteData _routes) : base(_sessionId, _db) 
        {
            if (AccessScripts.CheckAccess(_db, base.user, _routes))
            {
                TagEntity.Add(base.user, _db, _tagName);
            }
            Access = AccessScripts.CheckAccess(_db, base.user, _routes);
        }
        public bool Access {  get; set; }
    }
}