//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SalesService.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class USERDETAIL
    {
        public int User_ID { get; set; }
        public Nullable<int> TYP_ID { get; set; }
        public string UserName { get; set; }
        public string loginID { get; set; }
        public string Password { get; set; }
        public string EmailID { get; set; }
        public string IMEINo { get; set; }
        public Nullable<bool> Status { get; set; }
        public string ResetPasswordCode { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
