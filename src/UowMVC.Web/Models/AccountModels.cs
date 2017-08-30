using UowMVC.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UowMVC.Web.Models
{
    public class AccountProfileModel
    {
        public UserViewModel User { set; get; }

        public bool IsAllDataPermission { set; get; }

        public IEnumerable<string> DataPermisionByDepartments { set; get; }
        public IEnumerable<string> DataPermisionByClasses { set; get; }


    }
}
