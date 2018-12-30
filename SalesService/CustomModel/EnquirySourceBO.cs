using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class EnquirySourceBO
    {
        public int? Enquiry_Source_Id { get; set; }
        public string Enquiry_Source_Name { get; set; }
        public string Enquiry_Source_Desc { get; set; }
        public int? IsActive { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Created_Date { get; set; }
        public int? Modified_By { get; set; }
        public DateTime? Modified_Date { get; set; }
    }
    public class MasterEnquiryTypeBO 
    {
        public int? Enquiry_Type_Id { get; set; }
        public string Enquiry_Type_Name { get; set; }
        public string Enquiry_Type_Description { get; set; }
        public int? IsActive { get; set; }
        public DateTime? Created_Date { get; set; }
        public int? Created_By { get; set; }
        public DateTime? Modified_Date { get; set; }
        public int? Modified_By { get; set; }
    }
}