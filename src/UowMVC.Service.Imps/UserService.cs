using UowMVC.Domain;
using UowMVC.Models;
using UowMVC.Repository;
using UowMVC.Service.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Imps
{
    public class UserService : ServiceBase, IUserService
    {
        public UserService(DefaultDataContext dbcontext) : base(dbcontext)
        {
        }

        public bool Add(UserViewModel model)
        {
            model.Id = Guid.NewGuid().ToString();
            ApplicationUser user = new ApplicationUser();
            uow.Set<ApplicationUser>().Add(user);

            user.Id = model.Id;
            user.Index = model.Index;

            user.Name = model.Name;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.UserAvatar = uow.Set<Media>().Find(model.Avatar);
            user.Num = model.Num;
            user.Gender = (GenderEnum)model.Gender;
            user.Type = model.Type;
            user.CreateAt = model.CreateAt;
            user.Deparment = model.Deparment;
            user.Introduce = model.Introduce;

            uow.Commit();
            return true;
        }

        public bool Update(UserViewModel model)
        {
            ApplicationUser user = uow.Set<ApplicationUser>().Where(a => a.IsDelete == false && a.Id == model.Id).FirstOrDefault();

            if (user == null)
                return false;

            user.Index = model.Index;
            user.Id = model.Id;
            user.Name = model.Name;
            user.RealName = model.RealName;
            user.Email = model.Email;
            user.Num = model.Num;
            user.PhoneNumber = model.PhoneNumber;
            user.Gender = (GenderEnum)model.Gender;
            user.Type = model.Type;
            user.Deparment = model.Deparment;
            user.Introduce = model.Introduce;

            uow.Commit();

            return true;
        }

        public bool ChangeAvatar(string userid, MediaViewModel media)
        {
            ApplicationUser user = uow.Set<ApplicationUser>().Where(a => a.IsDelete == false && a.Id == userid).FirstOrDefault();

            if (user == null)
                return false;
            user.UserAvatar = uow.Set<Media>().Find(media.Id);
            uow.Commit();
            return true;

        }
        public UserViewModel GetByUserName(string username)
        {
            var user = uow.Set<ApplicationUser>().Where(a => a.IsDelete == false && a.UserName == username).FirstOrDefault();
            if (user == null)
                return null;
            return new UserViewModel(user);
        }

        public IEnumerable<UserViewModel> Query(string key, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));

        }

        public bool AddUsersToGroup(string groupid, IEnumerable<string> userids)
        {
            if (userids == null || userids.Count() == 0)
            {
                return false;
            }
            var userGroup = uow.Set<UserGroup>().Find(groupid);
            if (userGroup == null)
            {
                return false;
            }
            foreach (var userid in userids)
            {
                var isExist = uow.Set<UserGroupRelationship>().Any(x => x.UserGroup.Id == groupid && x.User.Id == userid);
                if (isExist)
                {
                    continue;
                }
                uow.Set<UserGroupRelationship>().Add(new UserGroupRelationship
                {
                    User = uow.Set<ApplicationUser>().Find(userid),
                    UserGroup = userGroup
                });
            }
            uow.Commit();
            return true;
        }

        public bool AddUserToGroup(string groupid, string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return false;
            }
            var userGroup = uow.Set<UserGroup>().Find(groupid);
            if (userGroup == null)
            {
                return false;
            }
            var isExist = uow.Set<UserGroupRelationship>().Any(x => x.UserGroup.Id == groupid && x.User.Id == userid);
            if (isExist)
            {
                return false;
            }
            var user = uow.Set<ApplicationUser>().Find(userid);
            uow.Set<UserGroupRelationship>().Add(new UserGroupRelationship
            {
                User = user,
                UserGroup = userGroup
            });
            uow.Commit();
            return true;
        }

        public bool RemoveUserFromGroup(string groupid, string userid)
        {
            var relationships = uow.Set<UserGroupRelationship>().Where(x => x.UserGroup.Id == groupid && x.User.Id == userid);
            if (relationships == null || relationships.Count() == 0)
            {
                return false;
            }
            foreach (var relationship in relationships)
            {
                uow.Set<UserGroupRelationship>().Remove(relationship);
            }
            uow.Commit();
            return true;
        }

        public bool RemoveUsersFromGroup(string groupid, IEnumerable<string> userids)
        {
            if (userids == null || userids.Count() == 0)
            {
                return false;
            }
            foreach (var userid in userids)
            {
                var relationships = uow.Set<UserGroupRelationship>().Where(x => x.UserGroup.Id == groupid && x.User.Id == userid);
                if (relationships == null || relationships.Count() == 0)
                {
                    continue;
                }
                foreach (var relationship in relationships)
                {
                    uow.Set<UserGroupRelationship>().Remove(relationship);
                }
            }
            uow.Commit();
            return true;
        }

        public IEnumerable<UserViewModel> QueryByGroup(string key, string groupid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(groupid))
            {
                query = query.Where(x => x.UserGroups.Any(ug => ug.UserGroup.Id == groupid));
            }
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));
        }

        public IEnumerable<UserViewModel> QueryExGroup(string key, string groupid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false && !x.UserGroups.Any(ug => ug.UserGroup.Id == groupid));
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));

        }

        public IEnumerable<UserGroupRelationshipViewModel> QueryRelationshipWithGroup(string key, string groupid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            var userGroup = uow.Set<UserGroup>().Find(groupid);
            count = query.Count();
            var users = query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList();
            var result = new List<UserGroupRelationshipViewModel>();
            foreach (var user in users)
            {
                var isInGroup = user.UserGroups.Any(x => x.UserGroup.Id == groupid);
                result.Add(new UserGroupRelationshipViewModel(new UserViewModel(user), new UserGroupViewModel(userGroup), isInGroup));
            }
            return result;
        }

        public long QueryGroupUserCount(string groupid)
        {
            return uow.Set<UserGroupRelationship>().Where(x => x.UserGroup.Id == groupid).Count();
        }

        public bool AddUsersToDepartment(string departmentid, IEnumerable<string> userids)
        {
            if (userids == null || userids.Count() == 0)
            {
                return false;
            }
            var department = uow.Set<Department>().Find(departmentid);
            if (department == null)
            {
                return false;
            }
            foreach (var userid in userids)
            {
                var isExist = uow.Set<UserGroupRelationship>().Any(x => x.UserGroup.Id == departmentid && x.User.Id == userid);
                if (isExist)
                {
                    continue;
                }
                uow.Set<DepartmentRelationship>().Add(new DepartmentRelationship
                {
                    User = uow.Set<ApplicationUser>().Find(userid),
                    Department = department
                });
            }
            uow.Commit();
            return true;
        }

        public bool AddUserToDepartment(string departmentid, string userid)
        {
            if (string.IsNullOrEmpty(userid))
            {
                return false;
            }
            var department = uow.Set<Department>().Find(departmentid);
            if (department == null)
            {
                return false;
            }
            var isExist = uow.Set<DepartmentRelationship>().Any(x => x.Department.Id == departmentid && x.User.Id == userid);
            if (isExist)
            {
                return false;
            }
            var user = uow.Set<ApplicationUser>().Find(userid);
            uow.Set<DepartmentRelationship>().Add(new DepartmentRelationship
            {
                User = user,
                Department = department
            });
            uow.Commit();
            return true;
        }

        public bool RemoveUserFromDepartment(string departmentid, string userid)
        {
            var relationships = uow.Set<DepartmentRelationship>().Where(x => x.Department.Id == departmentid && x.User.Id == userid);
            if (relationships == null || relationships.Count() == 0)
            {
                return false;
            }
            foreach (var relationship in relationships)
            {
                uow.Set<DepartmentRelationship>().Remove(relationship);
            }
            uow.Commit();
            return true;
        }

        public bool RemoveUsersFromDepartment(string departmentid, IEnumerable<string> userids)
        {
            if (userids == null || userids.Count() == 0)
            {
                return false;
            }
            foreach (var userid in userids)
            {
                var relationships = uow.Set<DepartmentRelationship>().Where(x => x.Department.Id == departmentid && x.User.Id == userid);
                if (relationships == null || relationships.Count() == 0)
                {
                    continue;
                }
                foreach (var relationship in relationships)
                {
                    uow.Set<DepartmentRelationship>().Remove(relationship);
                }
            }
            uow.Commit();
            return true;
        }

        public IEnumerable<UserViewModel> QueryByDepartment(string key, string departmentid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(departmentid))
            {
                query = query.Where(x => x.Departments.Any(ug => ug.Department.Id == departmentid));
            }
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));

        }

        public IEnumerable<UserViewModel> QueryExDepartment(string key, string departmentid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false && !x.Departments.Any(ug => ug.Department.Id == departmentid));
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));
        }

        public IEnumerable<UserDepartmentRelationshipViewModel> QueryRelationshipWithDepartment(string key, string departmentid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            var department = uow.Set<Department>().Find(departmentid);
            count = query.Count();
            var users = query.OrderByDescending(x => x.CreateAt).Skip(offset).Take(limit).ToList();
            var result = new List<UserDepartmentRelationshipViewModel>();
            foreach (var user in users)
            {
                var isInGroup = user.Departments.Any(x => x.Department.Id == departmentid);
                result.Add(new UserDepartmentRelationshipViewModel(new UserViewModel(user), new DepartmentViewModel(department), isInGroup));
            }
            return result;
        }

        public long QueryDepartmentUserCount(string departmentid)
        {
            return uow.Set<DepartmentRelationship>().Where(x => x.Department.Id == departmentid).Count();
        }

        public long QueryRoleUserCount(string roleid)
        {
            return uow.Set<IdentityUserRole>().Where(x => x.RoleId == roleid).Count();
        }

        public IEnumerable<UserViewModel> QueryByRole(string key, string roleid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false);
            if (!string.IsNullOrEmpty(roleid))
            {
                query = query.Where(x => x.Roles.Any(r => r.RoleId == roleid));
            }
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));

        }
        public IEnumerable<UserViewModel> QueryExRole(string key, string roleid, int offset, int limit, out int count)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false && !x.Roles.Any(r => r.RoleId == roleid));
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));

        }

        public UserViewModel GetById(string id)
        {
            var user = uow.Set<ApplicationUser>().Find(id);
            if (user == null || user.IsDelete)
            {
                return new UserViewModel();
            }
            return new UserViewModel(user);
        }

        public ListViewModel<UserViewModel> Query(string start, string end, int? pageNumber, int? pageSize, string key)
        {
            pageNumber = !pageNumber.HasValue || pageNumber < 0 ? 0 : pageNumber;

            pageSize = !pageSize.HasValue || pageSize < 1 ? 10 : pageSize;

            var Items = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false);

            if (Items == null)

                return new ListViewModel<UserViewModel>();

            if (!string.IsNullOrWhiteSpace(key))
            {
                Items = Items.Where(c => c.Name.Contains(key) || c.UserName.Contains(key));
            }

            DateTime? tempDatetimeStart = null;

            DateTime? tempDatetimeEnd = null;

            if (!string.IsNullOrWhiteSpace(start))
            {
                DateTime datetimeStart;
                if (DateTime.TryParse(start, out datetimeStart))
                {
                    tempDatetimeStart = datetimeStart;
                    Items = Items.Where(p => p.CreateAt >= datetimeStart);
                }
            }
            if (!string.IsNullOrWhiteSpace(end))
            {
                DateTime datetimeEnd;
                if (DateTime.TryParse(end, out datetimeEnd))
                {
                    tempDatetimeEnd = datetimeEnd;
                    Items = Items.Where(p => p.CreateAt >= datetimeEnd);
                }
            }
            int TotalNumber = Items.Count();
            if (TotalNumber < pageSize * pageNumber)
            {
                pageNumber = 0;
            }

            Items = Items.OrderBy(r => r.Index).Skip(pageSize.Value * pageNumber.Value).Take(pageSize.Value);

            return new ListViewModel<UserViewModel>
            {
                Items = Items.ToArray().Select(c => new UserViewModel(c)).ToList(),
                PageNumber = pageNumber.Value,
                PageSize = pageSize.Value,
                Keyword = key,
                TotalNumber = TotalNumber,
                StartDate = tempDatetimeStart,
                EndDate = tempDatetimeEnd,
            };
        }

        public IEnumerable<UserViewModel> QueryStaff(string key, int offset, int limit, out int count, string department)
        {
            var query = uow.Set<ApplicationUser>().Where(x => x.IsDelete == false && x.Type == ApplicationUserTypeEnum.User);
            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(x => x.Deparment == department);
            }
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));

        }

        public bool UpdateIntro(UserViewModel model)
        {
            ApplicationUser user = uow.Set<ApplicationUser>().Where(a => a.IsDelete == false && a.Id == model.Id).FirstOrDefault();

            if (user == null)
                return false;

            user.Id = model.Id;
            user.Introduce = model.Introduce;
            user.UserAvatar = uow.Set<Media>().Find(model.AvatarId);
            uow.Commit();

            return true;
        }

        public IEnumerable<UserViewModel> QueryStaff(string key, int offset, int limit, out int count, string department, List<string> allowDepartments)
        {
            var query = uow.Set<ApplicationUser>().Where(x => allowDepartments.Contains(x.Deparment) && x.IsDelete == false && x.Type == ApplicationUserTypeEnum.User);
            if (!string.IsNullOrEmpty(department))
            {
                query = query.Where(x => x.Deparment == department);
            }
            if (!string.IsNullOrEmpty(key))
            {
                query = query.Where(x => x.Name.Contains(key) || x.UserName.Contains(key) || x.PhoneNumber.Contains(key));
            }
            count = query.Count();
            return query.OrderBy(x => x.Index).Skip(offset).Take(limit).ToList().Select(x => new UserViewModel(x));

        }
    }
}
