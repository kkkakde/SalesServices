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
    
    public partial class W2S_SP_Master_Navigation_Result
    {
        public int PK_Navigaion_Id { get; set; }
        public string Navigation_Menu_Name { get; set; }
        public string Navigation_Description { get; set; }
        public Nullable<int> Navigation_Parent_Id { get; set; }
        public string Navigation_URL { get; set; }
        public Nullable<int> IsActive { get; set; }
    }
}