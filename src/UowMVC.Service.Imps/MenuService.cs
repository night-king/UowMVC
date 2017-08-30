using UowMVC.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UowMVC.Models;
using UowMVC.Repository;
using UowMVC.Domain;

namespace UowMVC.Service.Imps
{
    public class MenuService : ServiceBase, IMenuService
    {
        public MenuService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public bool Add(MenuViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            Menu entity = new Menu();
            uow.Set<Menu>().Add(entity);
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Icon = model.Icon;
            entity.Description = model.Description;
            entity.No = model.No;
            entity.OpenStyle = (MenuOpenStyleEnum)model.OpenStyle;
            entity.Parent = uow.Set<Menu>().Find(model.ParentID);
            entity.URL = model.URL;
            entity.IsMustSelected = model.IsMustSelected;
            entity.Width = model.Width;
            entity.Height = model.Height;
            entity.RelevantURL = model.RelevantURL;
            entity.IsControlPanel = model.IsControlPanel;
            entity.IsDisplayOnTable = model.IsDisplayOnTable;

            uow.Commit();
            return true;
        }

        public bool Delete(object id)
        {
            Menu entity = uow.Set<Menu>().Find(id);
            if (entity == null)
                return false;

            var permissions = uow.Set<RolePermission>().Where(x => x.Menu.Id == entity.Id).ToList();
            if (permissions != null && permissions.Count > 0)
            {
                for (int i = 0; i < permissions.Count; i++)
                {
                    uow.Set<RolePermission>().Remove(permissions.ElementAt(i));
                }
            }
            uow.Set<Menu>().Remove(entity);
            uow.Commit();
            return true;
        }

        public IEnumerable<MenuViewModel> GetAll()
        {
            return uow.Set<Menu>().ToList().Select(x => new MenuViewModel(x));
        }

        public MenuViewModel GetById(object id)
        {
            var entity = uow.Set<Menu>().Find(id);
            if (entity == null)
                return null;
            return new MenuViewModel(entity);
        }

        public IEnumerable<MenuViewModel> GetByRole(List<string> roleIds)
        {
            return uow.Set<RolePermission>().Where(x => roleIds.Contains(x.Role.Id)).ToList().Select(x => new MenuViewModel(x.Menu));
        }

        public IEnumerable<MenuViewModel> GetByUrl(string url)
        {
            return uow.Set<Menu>().Where(x => x.URL.ToLower() == url.ToLower()).ToList().Select(x => new MenuViewModel(x));
        }
        public IEnumerable<MenuViewModel> GetChildenByUrl(string url)
        {
            return uow.Set<Menu>().Where(x => x.Parent.URL.ToLower() == url.ToLower()).ToList().Select(x => new MenuViewModel(x));
        }

        public bool Update(MenuViewModel model)
        {
            var entity = uow.Set<Menu>().Find(model.Id);

            if (entity == null)
                return false;

            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Icon = model.Icon;
            entity.Description = model.Description;
            entity.No = model.No;
            entity.OpenStyle = (MenuOpenStyleEnum)model.OpenStyle;
            entity.Parent = uow.Set<Menu>().Find(model.ParentID);
            entity.URL = model.URL;
            entity.IsMustSelected = model.IsMustSelected;
            entity.Width = model.Width;
            entity.Height = model.Height;
            entity.RelevantURL = model.RelevantURL;
            entity.IsControlPanel = model.IsControlPanel;
            entity.IsDisplayOnTable = model.IsDisplayOnTable;

            uow.Commit();

            return true;
        }

        public bool Verify(MenuViewModel model)
        {
            return uow.Set<Menu>().Any(x => x.URL == model.URL && x.Id != model.Id);
        }
    }
}
