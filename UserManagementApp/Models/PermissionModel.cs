using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UserManagementApp.Models
{
    public class PermissionModel
    {      
        public int ID { get; set; }

        [DisplayName("Code")]
        [Required]
        public string Code { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }
        public int UserModelID_FK { get; set; }
    }
}