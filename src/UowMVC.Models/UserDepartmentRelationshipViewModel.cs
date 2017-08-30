using UowMVC.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class UserDepartmentRelationshipViewModel
    {
        public string Id { get; set; }

        public UserViewModel UserViewModel { get; set; }
        public DepartmentViewModel DepartmentViewModel { get; set; }
        /// <summary>
        /// 是否在此分组内
        /// </summary>
        public bool IsInGroup { get; set; }

        public UserDepartmentRelationshipViewModel()
        {
        }

        public UserDepartmentRelationshipViewModel(DepartmentRelationship entity)
        {
            Id = entity.Id;
            UserViewModel = entity.User == null ? null : new UserViewModel(entity.User);
            DepartmentViewModel = entity.Department == null ? null : new DepartmentViewModel(entity.Department);
            IsInGroup = true;
        }

        public UserDepartmentRelationshipViewModel(UserViewModel userViewModel, DepartmentViewModel departmentViewModel, bool isInGroup)
        {
            UserViewModel = userViewModel;
            DepartmentViewModel = departmentViewModel;
            IsInGroup = isInGroup;
        }
    }
}
