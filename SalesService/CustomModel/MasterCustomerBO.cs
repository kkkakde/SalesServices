using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class MasterCustomerBO
    {
        public MasterCustomerBO()
        {
            Cust_Contact_Person_List = new List<MasterCustomerContactPersonDetailsBO>();
            Cust_CompressorRoom_List = new List<MasterCustomerCompressorRoomDetailsBO>();
            CustomerIndustryTypeList = new List<IndustryTypeDropDownForamt>();
        }
        public string Url { get; set; }
        public int? PK_Cust_Id { get; set; }
        public string Cust_Name { get; set; }
        public string Cust_Address_Line1 { get; set; }
        public string Cust_Address_Line2 { get; set; }
        public string LandlineNo { get; set; }
        public string VisitType { get; set; }
        public ZoneDropDownForamt PK_Zone_Id { get; set; }
        public StateDropDownForamt PK_State_Id { get; set; }
        public int? FK_Zone { get; set; }
        public int? FK_State { get; set; }
        public int? FK_City { get; set; }
        public string zonename { get; set; }
        public string statename { get; set; }
        public string cityname { get; set; }
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
        //public string OutTime { get; set; }
        //public string InTime { get; set; }
        public bool? TrackInTime { get; set; }
        public bool? TrackOutTime { get; set; }
        public bool InFlag { get; set; }
        public string Action { get; set; }
        public int Created_By { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public long? Visit_Tracking_Id { get; set; }
        public int? FK_Opportunity_Id { get; set; }
        public string Cust_CntctPrson_Contact_No { get; set; }
        public List<MasterCustomerContactPersonDetailsBO> Cust_Contact_Person_List { get; set; }
        public List<MasterCustomerCompressorRoomDetailsBO> Cust_CompressorRoom_List { get; set; }
        public List<IndustryTypeDropDownForamt> CustomerIndustryTypeList { get; set; }
    }
    public class MasterCustomerContactPersonDetailsBO
    {
        public int? PK_Cust_CntctPrson_Mapng_Id { get; set; }
        public int? FK_Cust_Id { get; set; }
        public CommonDropDownForamt Cust_CntctPrson_Designation { get; set; }
        public string Cust_CntctPrson_Name { get; set; }
        public string Cust_CntctPrson_Contact_No { get; set; }
        public string Cust_CntctPrson_Email_Id { get; set; }
        public int? FK_Cust_CntctPrson_Dsgntn_Id { get; set; }
        public int? Set_DefaultNo { get; set; }
        public string List_Desc { get; set; }
        public int IsActive { get; set; }
        

    }
    public class MasterCustomerCompressorRoomDetailsBO
    {
        public int? PK_Cust_Cmprsor_Mapng_Id { get; set; }
        public string Cust_Make { get; set; }
        public int? FK_Cust_Id { get; set; }
        public CommonDropDownForamt Cust_Cmprsor_RoomDetails { get; set; }
        public string Cust_Cmprsor_Model { get; set; }
        public string Cust_Cmprsor_Mfg_Year { get; set; }
        public string Cust_Cmprsor_Commissioning_Year { get; set; }
        public CommonDropDownForamt Cust_Cmprsor_StatusDetails { get; set; }
        public string Cust_Cmprsor_RoomDetailsName { get; set; }
        public string List_Desc { get; set; }
        public int? Cust_Cmprsor_Status { get; set; }
        public string Remark { get; set; }
        public int? Running_Hours { get; set; }
    }
    public class CommonDropDownForamt
    {
        public string W2S_List_Id { get; set; }
        public string List_Desc { get; set; }
        public bool IsSelected { get; set; }
    }
    public class ZoneDropDownForamt
    {
        public string PK_Zone_Id { get; set; }
        public string Zone_Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class StateDropDownForamt
    {
        public string PK_State_Id { get; set; }
        public string State_Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class CityDropDownForamt
    {
        public string PK_City_Id { get; set; }
        public string City_Name { get; set; }
        public bool IsSelected { get; set; }
    }
    public class IndustryTypeDropDownForamt
    {
        public int? PK_Industry_Id { get; set; }
        public string Industry_Name { get; set; }
        public int? IsSelected { get; set; }
    }

}