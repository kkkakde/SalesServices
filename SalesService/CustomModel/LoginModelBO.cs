using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class LoginModelBO
    {
        public int PK_Resource_Id { get; set; }
        public Nullable<int> FK_Designation_Id { get; set; }
        public Nullable<int> FK_Zone_Id { get; set; }
        public Nullable<int> FK_State_Id { get; set; }
        public string Resource_Name { get; set; }
        public string Resource_Login_Id { get; set; }
        public string Resource_Password { get; set; }
        public string Resource_Mobile_No { get; set; }
        public string Resource_Email_Id { get; set; }
        public string Reset_PasswordCode { get; set; }
        public string Designation_Desc { get; set; }
        public int? CREATED_BY { get; set; }
        public Nullable<DateTime> CREATED_DATE { get; set; }
        public int? MODIFIED_BY { get; set; }
        public Nullable<DateTime> MODIFIED_DATE { get; set; }
        public int ISACTIVE { get; set; }
    }
    public class LoginModelBO1 
    {
        public int PK_Resource_Id { get; set; }
        public Nullable<int> FK_Designation_Id { get; set; }
        public Nullable<int> FK_Zone_Id { get; set; }
        public Nullable<int> FK_State_Id { get; set; }
        public string Resource_Name { get; set; }
        public string Resource_Login_Id { get; set; }
        public string Resource_Password { get; set; }
        public string Resource_Mobile_No { get; set; }
        public string Resource_Email_Id { get; set; }
        public string Reset_PasswordCode { get; set; }
        public string Action { get; set; }
        public int? CREATED_BY { get; set; }
        public Nullable<DateTime> CREATED_DATE { get; set; }
        public int? MODIFIED_BY { get; set; }
        public Nullable<DateTime> MODIFIED_DATE { get; set; }
        public int ISACTIVE { get; set; }
        public int? State_Id { get; set; }
    }

    public class ChangePasswordBO
    {
        public int? User_ID { get; set; }
        public string Password { get; set; }
        public string NEWPassword { get; set; }
    }
}