using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalesService.CustomModel
{
    public class OpportunityBO
    {
        
    }
    public class GetOpportunityListBO
    {
        public int? PK_OpportunityID { get; set; }
        public Int64 PK_Opportunity_Id { get; set; }
        public string Opportunity_Name { get; set; }
        public string Cust_Name { get; set; }
        public string Closed_Date { get; set; }
        public string FK_Status { get; set; }
        public string Added_Date { get; set; }
        public int Expected_Value { get; set; }
        public string Sales_Phase { get; set; }
        public string Resource_Name { get; set; }
        public string Status { get; set; }
        public string Customer_Contact_No { get; set; }
        public string Start_Date { get; set; }
        public string Chance_Of_Success { get; set; }
        public bool? Forecast { get; set; }
        public string Enquiry_Source_Name { get; set; }
        public string Enquiry_Type_Name { get; set; }
        public int PK_Cust_Id { get; set; }
        public List<ProductBO> ProductBO { get; set; }
    }

    public class AddOpportunityBO
    {
        public Int64 PK_Opportunity_Id { get; set; }
        public int? FK_Customer_Id { get; set; }
        public string Customer_Contact_No { get; set; }
        public int? FK_Opportunity_Source { get; set; }
        public int? FK_Opportunity_Type { get; set; }
        public string Opportunity_Name { get; set; }
        public DateTime? Start_Date { get; set; }
        public int? Expected_Value { get; set; }
        public string Chance_Of_Success { get; set; }
        public string Sales_Phase { get; set; }
        public DateTime? Closed_Date { get; set; }
        public string FK_Status { get; set; }
        public bool? Forecast { get; set; }
        public int? @Created_By { get; set; }
        
    }

    public class CustomerContactPersonDetailsBO
    {
        public string Cust_CntctPrson_Name { get; set; }
        public string Cust_CntctPrson_Contact_No { get; set; }
        public string Cust_CntctPrson_Email_Id { get; set; }
        public int Cust_CntctPrson_Designation { get; set; }
    }

    public class CustomerListBO
    {
        public int PK_Cust_Id { get; set; }
        public string Cust_Name { get; set; }
    }

    public class OpportunityTypeListBO:Common
    {
        public int Enquiry_Type_Id { get; set; }
        public string Enquiry_Type_Name { get; set; }
        public string Enquiry_Type_Description { get; set; }
    }

    public class OpportunitySourceBO
    {
        public int Enquiry_Source_Id { get; set; }
        public string Enquiry_Source_Name { get; set; }
        public string Enquiry_Source_Desc { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? MODIFIED_DATE { get; set; }
        public int? MODIFIED_BY { get; set; }
        public int ISACTIVE { get; set; }
    }

    public class ChanceOfSuccessBO
    {
        public int W2S_List_Id { get; set; }
        public string List_Name { get; set; }
        public string List_Code { get; set; }
        public string List_Desc { get; set; }
        public int Priority { get; set; }
        public int? CREATED_BY { get; set; }
        public DateTime? CREATED_DATE { get; set; }
        public DateTime? MODIFIED_DATE { get; set; }
        public int? MODIFIED_BY { get; set; }
        public int ISACTIVE { get; set; }
        public string SubRange_Name { get; set; }
        public int? PK_SubRange_Id { get; set; }
    }
    
    public class GetContactNo
    {
        public string Cust_CntctPrson_Contact_No { get; set; }
        public int PK_Cust_Id { get; set; }
    }

    public class OpportunityVisit 
    {
        public Int64? PK_Visit { get; set; }
        public int FK_Opportunity_Id { get; set; }
        public string FK_Visit_Type_Id { get; set; }
        public string List_Visit_Start_Date { get; set; }
        public string List_Visit_Start_Time { get; set; }
        public string List_Visit_End_Date { get; set; }
        public string List_Visit_End_Time { get; set; }
        public string List_Visit_type { get; set; }
    }

    public class OpportunityCompetitorBO
    {
        public Int64? PK_Competitor_Id { get; set; }
        public bool Is_Main { get; set; }
        public string Comp_Product { get; set; }
        public string Comp_Price { get; set; }
        public string List_Competitor_type { get; set; }
        public string Competitor_Name { get; set; }
        public Int64? FK_Opportunity_Id { get; set; }
        public string FK_Competitor_Type_Id { get; set; }
        public int? FK_Competitor_Id { get; set; }
    }

    public class CompetitorBO
    {
        public int? PK_Competitor_Id { get; set; }
        public bool Is_Main { get; set; }
        public string Competitor_Name { get; set; }
        public string FK_Competitor_Type_Id { get; set; }
       
    }

    public class OpporCompetitorListBO
    {
        public int? PK_Competitor_Id { get; set; }
        public string Competitor_Name { get; set; }
    }

    public class QuotationListBO
    {
        public Int64? PK_Quotation_Id { get; set; }
        public Int64? FK_Opportunity_Id { get; set; }
        public string File_Name { get; set; }
        public DateTime? Added_Date { get; set; }
        public int? Added_Time { get; set; }
    }

    public class Document_ListBO
    {
        public Int64? PK_Document_Id { get; set; }
        public Int64 FK_Opportunity_Id { get; set; }
        public string File_Name { get; set; }
        public DateTime? Added_Date { get; set; }
        public int? Added_Time { get; set; }
    }

    public class WonLostBO
    {
        public int? PK_Opportunity_Id { get; set; }
        public Int64? Won_Value { get; set; }
        public string Won_Remarks { get; set; }
        public long? Lost_Value { get; set; }
        public int? FK_Lost_Competitor_Id { get; set; }
        public string Lost_Remarks { get; set; }
        public string FK_Stop_Reason { get; set; }
        public int? FK_Stop_Competitor_Id { get; set; }
        public string Stop_Remarks { get; set; }
    }
   
    public class ProductBO
    {

        public long? PK_Oppor_Product_Id { get; set; }
        public long? FK_Opportunity_Id { get; set; }
        public string FK_Range_Id { get; set; }
        public int? FK_Sub_Range_Id { get; set; }
        public int? Product_Id { get; set; }
        public int? Quantity { get; set; }
        public string Price { get; set; }
        public bool ISCHECKED { get; set; }
        public string Product_Name { get; set; }
    }
}