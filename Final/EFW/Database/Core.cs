using Microsoft.EntityFrameworkCore;
using Final.EFW.Entities;
using Final.EFW.Database.EntityActions;
using Final.Static;

namespace Final.EFW.Database
{
    public class Core
    {
        public class ApplicationContext : DbContext
        {
            public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
            {
                Database.EnsureCreated();   // создаем базу данных при первом обращении
                Users.Load();
                Sessions.Load();
                Roles.Load();
                Articles.Load();
                Tags.Load();
                Comments.Load();
                ArticleTags.Load();
                UserRoles.Load();
                Pages.Load();
                Accesses.Load();
            }
            // Объекты таблицы Users
            public DbSet<User> Users { get; set; }
            public DbSet<Session> Sessions { get; set; }
            public DbSet<Role> Roles { get; set; }
            public DbSet<Article> Articles { get; set; }
            public DbSet<Tag> Tags { get; set; }
            public DbSet<Comment> Comments { get; set; }
            public DbSet<ArticleTag> ArticleTags { get; set; }
            public DbSet<UserRole> UserRoles { get; set; }
            public DbSet<Page> Pages { get; set; }
            public DbSet<Access> Accesses { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=../Database/Final.db");
            }
            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<User>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<Session>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<Role>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<Article>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<Tag>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).ValueGeneratedOnAdd().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<Comment>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<ArticleTag>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<UserRole>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<Page>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
                modelBuilder.Entity<Access>(eb =>
                {
                    eb.HasKey(x => x.Id);
                    eb.Property(x => x.Id).IsRequired().HasDefaultValue(Guid.NewGuid().ToString());
                    eb.Property(x => x.CreateDate).IsRequired().HasDefaultValue(DateTime.UtcNow);
                });
            }
        }

        public class StaticUserRole
        {
            public StaticUserRole(string _type, string _name)
            {
                this.type = _type;
                this.name = _name;
            }

            public string type { get; set; }
            public string name { get; set; }
        }
        public class StaticPages
        {
            public StaticPages(string _controller, string _action)
            {
                this.controller = _controller;
                this.action = _action;
            }

            public string controller { get; set; }
            public string action { get; set; }
        }
        public class StaticAccess
        {
            public StaticAccess(Page _page, Role _role)
            {
                this.Page = _page;
                this.Role = _role;
            }

            public Page Page { get; set; }
            public Role Role { get; set; }
        }

        public static void CheckDBStaticValues(ApplicationContext _db)
        {

            List<string> _names = new List<string>() { "Администраторы", "Модераторы", "Пользователи" };
            foreach (string _name in _names)
            {
                RoleEntity.Add(_name, _db);
            }

            List<StaticUserRole> _userTypes = new List<StaticUserRole> { new StaticUserRole("Regular", "Пользователи"), new StaticUserRole("Moderator", "Модераторы"), new StaticUserRole("Admin", "Администраторы") };
            foreach (StaticUserRole _StaticUserRole in _userTypes)
            {
                if (!UserEntity.Check($"{_StaticUserRole.type}User", _db))
                {
                    Role? _role = _db.Roles.FirstOrDefault(x => x.Name == _StaticUserRole.name) ?? null;
                    UserEntity.Register($"{_StaticUserRole.type}User", _db, SHA512Hash.Calculate($"{_StaticUserRole.type}Password"), _StaticUserRole.type, "User", $"{_StaticUserRole.type}User@mail.ru", _role);
                }
            }
            List<StaticPages> _staticPages = new List<StaticPages> { new StaticPages("Roles", "Add"), new StaticPages("Tags", "Add") };
            foreach (var _staticPage in _staticPages)
            {
                PageEntity.Add(_db, _staticPage.controller, _staticPage.action);
            }
            List<StaticAccess> _accessList = new List<StaticAccess> { new StaticAccess(PageEntity.Get(_db, "Roles", "Add"), RoleEntity.GetByName(_db, "Администраторы")), new StaticAccess(PageEntity.Get(_db, "Tags", "Add"), RoleEntity.GetByName(_db, "Модераторы")), new StaticAccess(PageEntity.Get(_db, "Tags", "Add"), RoleEntity.GetByName(_db, "Администраторы")) };
            foreach (var _access in _accessList)
            {
                AccessEntity.Add(_db, _access.Page, _access.Role);
            }
        }
    }
}

