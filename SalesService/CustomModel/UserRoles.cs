using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class Roles
    {
        public int? PK_Role_Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> Created_Date { get; set; }
        public int? Created_By { get; set; }
        public Nullable<DateTime> Modified_Date { get; set; }
        public int? Modified_By { get; set; }
    }


    public class UserRoles
    {
        public int? PK_UserRole_Id { get; set; }
        public int FK_User_Id { get; set; }
        public int FK_Role_Id { get; set; }
        public int FK_Zone_Id { get; set; }
        public int FK_State_Id { get; set; }
        public bool IsActive { get; set; }
        public Nullable<DateTime> Created_Date { get; set; }
        public int? Created_By { get; set; }
        public Nullable<DateTime> Modified_Date { get; set; }
        public int? Modified_By { get; set; }
    }

    public class UserRolesDetails
    {
        public int? PK_UserRole_Id { get; set; }
        public int FK_User_Id { get; set; }
        public string ResourceName { get; set; }
        public int FK_Role_Id { get; set; }
        public string RoleName { get; set; }
        public int FK_Zone_Id { get; set; }
        public string ZoneName { get; set; }
        public int FK_State_Id { get; set; }
        public string StateName { get; set; }
        public bool IsActive { get; set; }
       
    }
}