using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class Common
    {
        public string Action { get; set; }
        public int? CREATED_BY { get; set; }
        public string CREATED_DATE { get; set; }
        public int? MODIFIED_BY { get; set; }
        public string MODIFIED_DATE { get; set; }
        public int ISACTIVE { get; set; }
        public int? State_Id { get; set; }
    }

    public class ZoneListBO
    {
        public int PK_Zone_Id { get; set; }
        public string Zone_Desc { get; set; }
    }


    public class StateListBO
    {
        public int PK_State_Id { get; set; }
        public string State_Desc { get; set; }
    }

    public class CityListBO
    {
        public int PK_City_Id { get; set; }
        public string City_Name { get; set; }
    }

    public class ResponseClass
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object ResponseData { get; set; }
        public object Response { get; set; }
    }
    public class Industry
    {
        public int Industry_Id { get; set; }
        public string Industry_Name { get; set; }
        public string Industry_Desc { get; set; }
        public int IsActive { get; set; }
    }
    public class SegmentType
    {
        public int W2S_List_Id { get; set; }
        public string List_Code { get; set; }
        public string List_Desc { get; set; }

    }
    public class DesignationListBO
    {
        public int PK_Designation_Id { get; set; }
        public string Designation_Desc { get; set; }
    }
    public class User
    {
        public string username { get; set; }
        public string password { get; set; }
    }

    public class MenuListBO
    {
        public int? FK_Designation_Id { get; set; }
        public int? PK_Navigaion_Id { get; set; }
        public int? Navigation_Parent_Id { get; set; }
        public string Navigation_Menu_Name { get; set; }
        public string Navigation_Description { get; set; }
        public string Navigation_URL { get; set; }

    }
}