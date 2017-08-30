using UowMVC.Domain;
using UowMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Service.Interfaces
{
    public interface IUserService
    {

        bool Add(UserViewModel model);


        bool Update(UserViewModel model);
        bool UpdateIntro(UserViewModel model);

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="avaster"></param>
        /// <returns></returns>
        bool ChangeAvatar(string userid, MediaViewModel avaster);

        UserViewModel GetByUserName(string username);


        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> Query(string key, int offset, int limit, out int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <param name="key"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        ListViewModel<UserViewModel> Query(string start, string end, int? pageNumber, int? pageSize, string keyword);


        /// <summary>
        /// 查询某角色有多少用户数
        /// </summary>
        /// <param name="roleid"></param>
        /// <returns></returns>
        long QueryRoleUserCount(string roleid);


        /// <summary>
        /// 查询某个角色下的用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> QueryByRole(string key, string roleid, int offset, int limit, out int count);

        /// <summary>
        /// 查询不在某个角色下的用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="roleid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> QueryExRole(string key, string roleid, int offset, int limit, out int count);

        IEnumerable<UserViewModel> QueryStaff(string key, int offset, int limit, out int count,string department);
        IEnumerable<UserViewModel> QueryStaff(string key, int offset, int limit, out int count, string department,List<string> allowDepartments);


        /// <summary>
        /// 批量添加用户到分组
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="userids"></param>
        /// <returns></returns>
        bool AddUsersToGroup(string groupid, IEnumerable<string> userids);

        /// <summary>
        /// 将用户添加到分组
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool AddUserToGroup(string groupid, string userid);

        /// <summary>
        /// 用户移除分组
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool RemoveUserFromGroup(string groupid, string userid);

        /// <summary>
        /// 批量将用户移除分组
        /// </summary>
        /// <param name="groupid"></param>
        /// <param name="userids"></param>
        /// <returns></returns>
        bool RemoveUsersFromGroup(string groupid, IEnumerable<string> userids);

        /// <summary>
        /// 查询某个分组下的用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> QueryByGroup(string key, string groupid, int offset, int limit, out int count);

        /// <summary>
        /// 查询不在此分组内的所有用户
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> QueryExGroup(string key, string groupid, int offset, int limit, out int count);

        UserViewModel GetById(string id);


        /// <summary>
        /// 查询所有用户与分组关联关系
        /// </summary>
        /// <param name="key"></param>
        /// <param name="groupid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserGroupRelationshipViewModel> QueryRelationshipWithGroup(string key, string groupid, int offset, int limit, out int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns></returns>
        long QueryGroupUserCount(string groupid);

        /// <summary>
        /// 批量添加用户到部门
        /// </summary>
        /// <param name="departmentid"></param>
        /// <param name="userids"></param>
        /// <returns></returns>
        bool AddUsersToDepartment(string departmentid, IEnumerable<string> userids);


        /// <summary>
        /// 将用户添加到部门
        /// </summary>
        /// <param name="departmentid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool AddUserToDepartment(string departmentid, string userid);

        /// <summary>
        /// 用户移除部门
        /// </summary>
        /// <param name="departmentid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        bool RemoveUserFromDepartment(string departmentid, string userid);

        /// <summary>
        /// 批量将用户移除部门
        /// </summary>
        /// <param name="departmentid"></param>
        /// <param name="userids"></param>
        /// <returns></returns>
        bool RemoveUsersFromDepartment(string departmentid, IEnumerable<string> userids);

        /// <summary>
        /// 查询某个部门下的用户信息
        /// </summary>
        /// <param name="key"></param>
        /// <param name="departmentid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> QueryByDepartment(string key, string departmentid, int offset, int limit, out int count);

        /// <summary>
        /// 查询不在此部门内的所有用户
        /// </summary>
        /// <param name="key"></param>
        /// <param name="departmentid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserViewModel> QueryExDepartment(string key, string departmentid, int offset, int limit, out int count);


        /// <summary>
        /// 查询所有用户与部门关联关系
        /// </summary>
        /// <param name="key"></param>
        /// <param name="departmentid"></param>
        /// <param name="offset"></param>
        /// <param name="limit"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        IEnumerable<UserDepartmentRelationshipViewModel> QueryRelationshipWithDepartment(string key, string departmentid, int offset, int limit, out int count);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="departmentid"></param>
        /// <returns></returns>
        long QueryDepartmentUserCount(string departmentid);
    }
}
