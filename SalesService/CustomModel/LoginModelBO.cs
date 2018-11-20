using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class LoginModelBO:Common
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
    }

    public class ChangePasswordBO
    {
        public int? User_ID { get; set; }
        public string Password { get; set; }
        public string NEWPassword { get; set; }
    }
}