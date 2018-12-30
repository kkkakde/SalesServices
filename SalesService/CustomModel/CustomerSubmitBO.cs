using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{

    public class CustomerDetails {
        public int? PK_Cust_Id { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_Address_Line1 { get; set; }
        public string Cust_Address_Line2 { get; set; }
        public string LandlineNo { get; set; }
        public string VisitType { get; set; }
        public int? FK_Zone_Id { get; set; }
        public int? FK_State_Id { get; set; }
        public int? FK_City_Id { get; set; }
        public string Cust_GSTN_No { get; set; }
        public string Cust_Cmprsd_Air_App { get; set; }
        public string Cust_End_Product { get; set; }
        public int? Created_By { get; set; }
        public string[] PK_Industry_Id { get; set; }
        public string VisitingCardPath { get; set; }
    }
    public class CompressorRoomDetails
    {
        public int? PK_Cust_Id { get; set; }
        public string Cust_Make { get; set; }
        public int? Cust_Cmprsor_RoomDetails { get; set; }
        public string Cust_Cmprsor_Model { get; set; }
        public string Cust_Cmprsor_Mfg_Year { get; set; }
        public string Cust_Cmprsor_Commissioning_Year { get; set; }
        public int? Cust_Cmprsor_Status { get; set; }
        public int? Created_By { get; set; }
        public string Remark { get; set; }
        public int Running_Hours { get; set; }
    }
    public class CustomerSubmitBO
    {
        public  CustomerSubmitBO()
        {
            Cust_Contact_Person_List = new List<MasterCustomerContactPersonDetailsBO>();
            Cust_CompressorRoom_List = new List<MasterCustomerCompressorRoomDetailsBO>();
            CustomerIndustryTypeList = new List<IndustryTypeDropDownForamt>();
        }
        public int? PK_Cust_Id { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_Address_Line1 { get; set; }
        public string Cust_Address_Line2 { get; set; }
        public ZoneDropDownForamt PK_Zone_Id { get; set; }
        public StateDropDownForamt PK_State_Id { get; set; }
        public ZoneDropDownForamt FK_Zone_Id { get; set; }
        public StateDropDownForamt FK_State_Id { get; set; }
        public CityDropDownForamt FK_City_Id { get; set; }
        public string Cust_Longitude { get; set; }
        public string Cust_Latitude { get; set; }
        public string Cust_GSTN_No { get; set; }
        public string Cust_Cmprsd_Air_App { get; set; }
        public string VisitingCardPath { get; set; }
        public string Cust_End_Product { get; set; }
        public int? IsActive { get; set; }
        public List<MasterCustomerContactPersonDetailsBO> Cust_Contact_Person_List { get; set; }
        public List<MasterCustomerCompressorRoomDetailsBO> Cust_CompressorRoom_List { get; set; }
        public List<IndustryTypeDropDownForamt> CustomerIndustryTypeList { get; set; }
    }
    public class MasterCustomerContactPersonDetailsBO1
    {
        public int? PK_Cust_CntctPrson_Mapng_Id { get; set; }
        public int? FK_Cust_Id { get; set; }
        public CommonDropDownForamt Cust_CntctPrson_Designation { get; set; }
        public string Cust_CntctPrson_Name { get; set; }
        public string Cust_CntctPrson_Contact_No { get; set; }
        public string Cust_CntctPrson_Email_Id { get; set; }
        public int? Created_By { get; set; }
    }
    public class MasterCustomerCompressorRoomDetailsBO1
    {
        public int? PK_Cust_Cmprsor_Mapng_Id { get; set; }
        public int? FK_Cust_Id { get; set; }
        public CommonDropDownForamt Cust_Cmprsor_RoomDetails { get; set; }
        public string Cust_Cmprsor_Model { get; set; }
        public string Cust_Cmprsor_Mfg_Year { get; set; }
        public string Cust_Cmprsor_Commissioning_Year { get; set; }
        public CommonDropDownForamt Cust_Cmprsor_Status { get; set; }
    }
    public class CommonDropDownForamt1
    {
        public string W2S_List_Id { get; set; }
        public string List_Desc { get; set; }
        public bool IsSelected { get; set; }
    }
    public class ZoneDropDownForamt1
    {
        public string PK_Zone_Id { get; set; }
        public string Zone_Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class StateDropDownForamt1
    {
        public string PK_State_Id { get; set; }
        public string State_Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class CityDropDownForamt1
    {
        public string PK_City_Id { get; set; }
        public string City_Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class IndustryTypeDropDownForamt1
    {
        public string PK_Industry_Id { get; set; }
        public string Industry_Name { get; set; }
        public int? IsSelected { get; set; }
    }
    public class ContactPersonDesigListBO
    {
        public int W2S_List_Id { get; set; }
        public string List_Desc { get; set; }
        public int Priority { get; set; }
    }
}