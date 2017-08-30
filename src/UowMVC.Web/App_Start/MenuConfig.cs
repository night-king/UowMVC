using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UowMVC.Repository;
using UowMVC.Domain;
using UowMVC.Models;
using System.Security.Principal;
using System.Security.Claims;
using System.Runtime.Caching;

namespace UowMVC.Web
{
    public static class MenuConfig
    {
        private static object _lock = new object();
        private static ObjectCache _cache = MemoryCache.Default;
        private const int cache_mintues = 10;
        public static void Clear()
        {
            lock (_lock)
            {
                var cs = _cache.AsEnumerable().ToList();
                foreach (var c in cs)
                {
                    _cache.Remove(c.Key);
                }
            }
        }
        public static void Init(IPrincipal User)
        {
            lock (_lock)
            {
                var usr = User.Identity.Name;
                using (var _uow = new UnitOfWork(new DefaultDataContext()))
                {
                    var userDistinctMenus = new List<MenuViewModel>();
                    var isSuperAdmin = _uow.Set<ApplicationUser>().Any(x => x.UserName == usr && x.IsSuperAdmin == true);
                    if (!isSuperAdmin)
                    {
                        var identity = User.Identity as ClaimsIdentity;
                        var userRoles = identity.FindAll(ClaimTypes.Role).Select(x => x.Value).ToList();
                        var permissions = _uow.Set<RolePermission>().Where(x => userRoles.Contains(x.Role.Name)).Distinct().ToList();
                        var userMenus = permissions.Select(x => new MenuViewModel(x.Menu)).Distinct().ToList();

                        foreach (var m in userMenus)
                        {
                            if (userDistinctMenus.Any(x => x.Id == m.Id))
                            {
                                continue;
                            }
                            userDistinctMenus.Add(m);
                        }
                    }
                    else
                    {
                        userDistinctMenus = _uow.Set<Menu>().ToList().Select(x => new MenuViewModel(x)).ToList();
                    }
                    if (_cache.Any(x => x.Key == usr))
                    {
                        _cache.Remove(usr);
                    }
                    _cache.Set(usr, userDistinctMenus, new CacheItemPolicy() { AbsoluteExpiration = DateTime.Now.AddMinutes(cache_mintues) });
                }
            }
        }

        public static IEnumerable<MenuViewModel> Get(IPrincipal User)
        {
            var usr = User.Identity.Name;
            if (!_cache.Any(x => x.Key == usr))
            {
                Init(User);
            }
            lock (_lock)
            {
                return _cache.Get(usr) as IEnumerable<MenuViewModel>;
            }
        }
    }

}