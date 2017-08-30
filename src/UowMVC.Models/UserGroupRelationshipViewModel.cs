using UowMVC.Domain;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UowMVC.Models
{
    public class UserGroupRelationshipViewModel
    {
        /// <summary>
        /// 用户
        /// </summary>
        public UserViewModel UserViewModel { get; set; }
        public UserGroupViewModel UserGroupViewModel { get; set; }
        /// <summary>
        /// 是否在此分组内
        /// </summary>
        public bool IsInGroup { get; set; }

        public UserGroupRelationshipViewModel()
        {
        }
        public UserGroupRelationshipViewModel(UserViewModel userViewModel, UserGroupViewModel userGroupViewModel, bool isInGroup)
        {
            UserViewModel = userViewModel;
            UserGroupViewModel = userGroupViewModel;
            IsInGroup = isInGroup;
        }
    }
}
