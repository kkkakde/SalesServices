using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.Models;
using SalesService.CustomModel;
using System.Data.SqlClient;

namespace SalesService.Controllers
{
    public class Api_OpportunityController : Api_CommonController
    {
        AtlasW2SEntities _db = null;
        ResponseClass response = new ResponseClass();
        [HttpPost]
        [ActionName("GetOpportunityList")]
        public HttpResponseMessage GetOpportunityList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<GetOpportunityListBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                       new SqlParameter("@Action", "Get_Opportunity_List"),
                       new SqlParameter("@PK_Opportunity_Id", ""),
                       new SqlParameter("@FK_Customer_Id", ""),
                       new SqlParameter("@Opportunity_Name", ""),
                       new SqlParameter("@FK_Opportunity_Id", ""),
                       new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "GetOpportunityList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("AddOpportunity")]
        public HttpResponseMessage AddOpportunity(AddOpportunityBO addOpportunityBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {

                    var data = _db.Database.SqlQuery<AddOpportunityBO>("W2S_SP_AddOpportunity " +
                        "@FK_Customer_Id," +
                        "@Customer_Contact_No," +
                        "@FK_Opportunity_Source," +
                        "@FK_Opportunity_Type," +
                        "@Opportunity_Name," +
                        "@Start_Date," +
                        "@Expected_Value," +
                        "@Chance_Of_Success," +
                        "@Sales_Phase," +
                        "@Closed_Date," +
                        "@FK_Status," +
                        "@Forecast," +
                        "@Created_By",
                       new SqlParameter("@FK_Customer_Id", addOpportunityBO.FK_Customer_Id),
                       new SqlParameter("@Customer_Contact_No", addOpportunityBO.Customer_Contact_No),
                       new SqlParameter("FK_Opportunity_Source", addOpportunityBO.FK_Opportunity_Source),
                       new SqlParameter("FK_Opportunity_Type", addOpportunityBO.FK_Opportunity_Type),
                       new SqlParameter("Opportunity_Name", addOpportunityBO.Opportunity_Name),
                       new SqlParameter("Start_Date", addOpportunityBO.Start_Date),
                       new SqlParameter("Expected_Value", addOpportunityBO.Expected_Value),
                       new SqlParameter("Chance_Of_Success", addOpportunityBO.Chance_Of_Success),
                       new SqlParameter("Sales_Phase", addOpportunityBO.Sales_Phase),
                       new SqlParameter("Closed_Date", addOpportunityBO.Closed_Date),
                       new SqlParameter("FK_Status", addOpportunityBO.FK_Status),
                       new SqlParameter("Forecast", addOpportunityBO.Forecast == null ? Convert.ToInt16(addOpportunityBO.Forecast) : 0),
                       new SqlParameter("Created_By", addOpportunityBO.Created_By)
                       ).ToList();

                    if (data[0].PK_Opportunity_Id > 0)
                    {

                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "AddOpportunity", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Opportunity_Details")]
        public HttpResponseMessage Get_Opportunity_Details(int PK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<GetOpportunityListBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                         new SqlParameter("@Action", "Get_Opportunity_Details"),
                         new SqlParameter("@PK_Opportunity_Id", PK_Opportunity_Id),
                         new SqlParameter("@FK_Customer_Id", ""),
                         new SqlParameter("@Opportunity_Name", ""),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Opportunity_Details", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_ContactPerson_Details")]
        public HttpResponseMessage Get_ContactPerson_Details(int FK_Customer_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<CustomerContactPersonDetailsBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                         new SqlParameter("@Action", "Get_ContactPerson_Details"),
                         new SqlParameter("@PK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Customer_Id", FK_Customer_Id),
                         new SqlParameter("@Opportunity_Name", ""),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_ContactPerson_Details", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Customer_List")]
        public HttpResponseMessage Get_Customer_List()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<CustomerListBO>("W2S_SP_Get_Opportunity_List 'Get_Customer_List','',''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Customer_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("GetOpportunityTypeList")]
        public HttpResponseMessage GetOpportunityTypeList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityTypeListBO>("W2S_SP_Get_Opportunity_List 'Get_Opportunity_Type_List','','','','',''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "GetOpportunityTypeList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("GetOpportunitySourceList")]
        public HttpResponseMessage GetOpportunitySourceList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunitySourceBO>("W2S_SP_Get_Opportunity_List 'Get_Opportunity_Source_List','','','','',''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "GetOpportunitySourceList", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Chance_Of_Success")]
        public HttpResponseMessage Get_Chance_Of_Success()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Opportunity_List 'Get_Chance_Of_Success','','','','',''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Chance_Of_Success", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Sales_Phase")]
        public HttpResponseMessage Get_Sales_Phase()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Opportunity_List 'Get_Sales_Phase','','','','',''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Sales_Phase", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Status")]
        public HttpResponseMessage Get_Status()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Opportunity_List 'Get_Status','','','','',''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Status", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("GetContactNo")]
        public HttpResponseMessage GetContactNo(string Opportunity_Name)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<GetContactNo>("W2S_SP_Get_Opportunity_List 'Get_Contact_No','',''," + Opportunity_Name
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "GetContactNo", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Visit_Type_List")]
        public HttpResponseMessage Get_Visit_Type_List()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Opportunity_List 'Get_Visit_Type_List','','','','',''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Visit_Type_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Get_OpportunityVisitDetails")]
        public HttpResponseMessage Get_OpportunityVisitDetails(int FK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityVisit>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                         new SqlParameter("@Action", "Get_OpportunityVisitDetails"),
                         new SqlParameter("@PK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Customer_Id", ""),
                         new SqlParameter("@Opportunity_Name", ""),
                         new SqlParameter("@FK_Opportunity_Id", FK_Opportunity_Id),
                         new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_OpportunityVisitDetails", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("AddVisits")]
        public HttpResponseMessage AddVisits(OpportunityVisit opportunityVisit)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityVisit>("W2S_SP_AddVisits @Action,@PK_Visit,@FK_Opportunity_Id,@FK_Visit_Type_Id,@List_Visit_Start_Date,@List_Visit_Start_Time,@List_Visit_End_Date,@List_Visit_End_Time",
                         new SqlParameter("@Action", "Add_Visits"),
                         new SqlParameter("@PK_Visit", ""),
                         new SqlParameter("@FK_Opportunity_Id", opportunityVisit.FK_Opportunity_Id),
                         new SqlParameter("@FK_Visit_Type_Id", opportunityVisit.FK_Visit_Type_Id),
                         new SqlParameter("@List_Visit_Start_Date", opportunityVisit.List_Visit_Start_Date),
                         new SqlParameter("@List_Visit_Start_Time", opportunityVisit.List_Visit_Start_Time),
                         new SqlParameter("@List_Visit_End_Date", opportunityVisit.List_Visit_End_Date),
                         new SqlParameter("@List_Visit_End_Time", opportunityVisit.List_Visit_End_Time)
                        ).ToList();
                    if (data[0].PK_Visit > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "AddVisits", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("DeleteVisits")]
        public HttpResponseMessage DeleteVisits(int PK_Visit)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityVisit>("W2S_SP_AddVisits @Action,@PK_Visit,@FK_Opportunity_Id,@FK_Visit_Type_Id,@List_Visit_Start_Date,@List_Visit_Start_Time,@List_Visit_End_Date,@List_Visit_End_Time",
                         new SqlParameter("@Action", "Delete_Visits"),
                         new SqlParameter("@PK_Visit", PK_Visit),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Visit_Type_Id", ""),
                         new SqlParameter("@List_Visit_Start_Date", ""),
                         new SqlParameter("@List_Visit_Start_Time", ""),
                         new SqlParameter("@List_Visit_End_Date", ""),
                         new SqlParameter("@List_Visit_End_Time", "")
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record deleted successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "AddVisits", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Competitor_Type_List")]
        public HttpResponseMessage Get_Competitor_Type_List()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Opportunity_List 'Get_Competitor_Type_List','','','',null,''"
                       ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Competitor_Type_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Get_OpportunityCompetitorDetails")]
        public HttpResponseMessage Get_OpportunityCompetitorDetails(int FK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityCompetitorBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                          new SqlParameter("@Action", "Get_OpportunityCompetitorDetails"),
                           new SqlParameter("@PK_Opportunity_Id", ""),
                          new SqlParameter("@FK_Customer_Id", ""),
                          new SqlParameter("@Opportunity_Name", ""),
                          new SqlParameter("@FK_Opportunity_Id", FK_Opportunity_Id),
                          new SqlParameter("@FK_Competitor_Type_Id", "")
                        ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_OpportunityCompetitorDetails", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("AddCompetitor")]
        public HttpResponseMessage AddCompetitor(OpportunityCompetitorBO opportunityCompetitorBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityCompetitorBO>("W2S_SP_AddCompetitor @Action,@FK_Opportunity_Id,@FK_Competitor_Type_Id,@FK_Competitor_Id,@Is_Main,@Comp_Product,@Comp_Price,@PK_Competitor_Id",
                         new SqlParameter("@Action", "Add_Competitor"),
                         new SqlParameter("@FK_Opportunity_Id", opportunityCompetitorBO.FK_Opportunity_Id),
                         new SqlParameter("@FK_Competitor_Type_Id", opportunityCompetitorBO.FK_Competitor_Type_Id),
                         new SqlParameter("@FK_Competitor_Id", opportunityCompetitorBO.FK_Competitor_Id),
                         new SqlParameter("@Is_Main", opportunityCompetitorBO.Is_Main),
                         new SqlParameter("@Comp_Product", opportunityCompetitorBO.Comp_Product),
                         new SqlParameter("@Comp_Price", opportunityCompetitorBO.Comp_Price),
                         new SqlParameter("@PK_Competitor_Id", "")
                        ).ToList();
                    if (data[0].PK_Competitor_Id > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "AddCompetitor", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Delete_Competitor")]
        public HttpResponseMessage Delete_Competitor(int PK_Competitor_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityCompetitorBO>("W2S_SP_AddCompetitor @Action,@FK_Opportunity_Id,@FK_Competitor_Type_Id,@FK_Competitor_Id,@Is_Main,@Comp_Product,@Comp_Price,@PK_Competitor_Id",
                         new SqlParameter("@Action", "Delete_Competitor"),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Competitor_Type_Id", ""),
                         new SqlParameter("@FK_Competitor_Id", ""),
                         new SqlParameter("@Is_Main", ""),
                         new SqlParameter("@Comp_Product", ""),
                         new SqlParameter("@Comp_Price", ""),
                         new SqlParameter("@PK_Competitor_Id", PK_Competitor_Id)
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record deleted successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "AddCompetitor", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Get_Quotation_List")]
        public HttpResponseMessage Get_Quotation_List(int FK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<QuotationListBO>("W2S_SP_AddCompetitor @Action,@FK_Opportunity_Id,@FK_Competitor_Type_Id,@FK_Competitor_Id,@Is_Main,@Comp_Product,@Comp_Price,@PK_Competitor_Id",
                         new SqlParameter("@Action", "Get_Quotation_List"),
                         new SqlParameter("@FK_Opportunity_Id", FK_Opportunity_Id),
                         new SqlParameter("@FK_Competitor_Type_Id", ""),
                         new SqlParameter("@FK_Competitor_Id", ""),
                         new SqlParameter("@Is_Main", ""),
                         new SqlParameter("@Comp_Product", ""),
                         new SqlParameter("@Comp_Price", ""),
                         new SqlParameter("@PK_Competitor_Id", "")
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "AddCompetitor", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Document_List")]
        public HttpResponseMessage Get_Document_List(int FK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Document_ListBO>("W2S_SP_AddCompetitor @Action,@FK_Opportunity_Id,@FK_Competitor_Type_Id,@FK_Competitor_Id,@Is_Main,@Comp_Product,@Comp_Price,@PK_Competitor_Id",
                         new SqlParameter("@Action", "Get_Document_List"),
                         new SqlParameter("@FK_Opportunity_Id", FK_Opportunity_Id),
                         new SqlParameter("@FK_Competitor_Type_Id", ""),
                         new SqlParameter("@FK_Competitor_Id", ""),
                         new SqlParameter("@Is_Main", ""),
                         new SqlParameter("@Comp_Product", ""),
                         new SqlParameter("@Comp_Price", ""),
                         new SqlParameter("@PK_Competitor_Id", "")
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Document_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Range_List")]
        public HttpResponseMessage Get_Range_List()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Range_List @Action,@FK_Range_Id",
                         new SqlParameter("@Action", "Get_Range_List"),
                         new SqlParameter("@FK_Range_Id", "")
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Range_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Sub_Range_List")]
        public HttpResponseMessage Get_Sub_Range_List(string FK_Range_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Range_List @Action,@FK_Range_Id",
                         new SqlParameter("@Action", "Get_Sub_Range_List"),
                         new SqlParameter("@FK_Range_Id", FK_Range_Id)
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Range_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Product_List")]
        public HttpResponseMessage Get_Product_List(int FK_Sub_Range_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ProductBO>("W2S_SP_Product @Action,@FK_Sub_Range_Id,@FK_Opportunity_Id,@FK_Range_Id,@Quantity,@Price,@FK_Product_Id",
                         new SqlParameter("@Action", "Get_Product_List"),
                         new SqlParameter("@FK_Sub_Range_Id", FK_Sub_Range_Id),
                         new SqlParameter("@FK_Opportunity_Id", FK_Sub_Range_Id),
                         new SqlParameter("@FK_Range_Id", FK_Sub_Range_Id),
                         new SqlParameter("@Quantity", FK_Sub_Range_Id),
                         new SqlParameter("@Price", FK_Sub_Range_Id),
                         new SqlParameter("@FK_Product_Id", FK_Sub_Range_Id)
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Product_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Oppor_Prod")]
        public HttpResponseMessage Get_Oppor_Prod(int PK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityCompetitorBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                          new SqlParameter("@Action", "Get_Oppor_Prod_List"),
                          new SqlParameter("@PK_Opportunity_Id", PK_Opportunity_Id),
                          new SqlParameter("@FK_Customer_Id", ""),
                          new SqlParameter("@Opportunity_Name", ""),
                          new SqlParameter("@FK_Opportunity_Id", ""),
                          new SqlParameter("@FK_Competitor_Type_Id", "")
                        ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_OpportunityCompetitorDetails", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Reason_List")]
        public HttpResponseMessage Get_Reason_List()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                          new SqlParameter("@Action", "Get_Reason_List"),
                          new SqlParameter("@PK_Opportunity_Id", ""),
                          new SqlParameter("@FK_Customer_Id", ""),
                          new SqlParameter("@Opportunity_Name", ""),
                          new SqlParameter("@FK_Opportunity_Id", ""),
                          new SqlParameter("@FK_Competitor_Type_Id", "")
                        ).ToList();

                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Reason_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Add_Won")]
        public HttpResponseMessage Add_Won(WonLostBO wonLostBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<WonLostBO>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Won_Value,@Won_Remarks,@Lost_Value,@FK_Lost_Competitor_Id,@Lost_Remarks,@Stop_Remarks,@FK_Stop_Competitor_Id,@FK_Stop_Reason",
                         new SqlParameter("@Action", "Add_Won"),
                         new SqlParameter("@PK_Opportunity_Id", wonLostBO.PK_Opportunity_Id),
                         new SqlParameter("@Won_Value", wonLostBO.Won_Value),
                         new SqlParameter("@Won_Remarks", wonLostBO.Won_Remarks),
                         new SqlParameter("@Lost_Value", ""),
                         new SqlParameter("@FK_Lost_Competitor_Id", ""),
                         new SqlParameter("@Lost_Remarks", ""),
                         new SqlParameter("@Stop_Remarks", ""),
                         new SqlParameter("@FK_Stop_Competitor_Id", ""),
                         new SqlParameter("@FK_Stop_Reason", "")
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Add_Won", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Add_Lost")]
        public HttpResponseMessage Add_Lost(WonLostBO wonLostBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<WonLostBO>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Won_Value,@Won_Remarks,@Lost_Value,@FK_Lost_Competitor_Id,@Lost_Remarks,@Stop_Remarks,@FK_Stop_Competitor_Id,@FK_Stop_Reason",
                         new SqlParameter("@Action", "Add_Lost"),
                         new SqlParameter("@PK_Opportunity_Id", wonLostBO.PK_Opportunity_Id),
                         new SqlParameter("@Won_Value", ""),
                         new SqlParameter("@Won_Remarks", ""),
                         new SqlParameter("@Lost_Value", wonLostBO.Lost_Value),
                         new SqlParameter("@FK_Lost_Competitor_Id", wonLostBO.FK_Lost_Competitor_Id),
                         new SqlParameter("@Lost_Remarks", wonLostBO.Lost_Remarks),
                         new SqlParameter("@Stop_Remarks", ""),
                         new SqlParameter("@FK_Stop_Competitor_Id", ""),
                         new SqlParameter("@FK_Stop_Reason", "")
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Add_Lost", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Add_Stop")]
        public HttpResponseMessage Add_Stop(WonLostBO wonLostBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<WonLostBO>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Won_Value,@Won_Remarks,@Lost_Value,@FK_Lost_Competitor_Id,@Lost_Remarks,@Stop_Remarks,@FK_Stop_Competitor_Id,@FK_Stop_Reason",
                         new SqlParameter("@Action", "Add_Stop"),
                         new SqlParameter("@PK_Opportunity_Id", wonLostBO.PK_Opportunity_Id),
                         new SqlParameter("@Won_Value", ""),
                         new SqlParameter("@Won_Remarks", ""),
                         new SqlParameter("@Lost_Value", ""),
                         new SqlParameter("@FK_Lost_Competitor_Id", ""),
                         new SqlParameter("@Lost_Remarks", ""),
                         new SqlParameter("@Stop_Remarks", wonLostBO.Stop_Remarks),
                         new SqlParameter("@FK_Stop_Competitor_Id", wonLostBO.FK_Stop_Competitor_Id),
                         new SqlParameter("@FK_Stop_Reason", wonLostBO.FK_Stop_Reason)
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Add_Stop", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("SetAsInProc")]
        public HttpResponseMessage SetAsInProc(int PK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<WonLostBO>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Won_Value,@Won_Remarks,@Lost_Value,@FK_Lost_Competitor_Id,@Lost_Remarks,@Stop_Remarks,@FK_Stop_Competitor_Id,@FK_Stop_Reason",
                         new SqlParameter("@Action", "SetAsInProc"),
                         new SqlParameter("@PK_Opportunity_Id", PK_Opportunity_Id),
                         new SqlParameter("@Won_Value", ""),
                         new SqlParameter("@Won_Remarks", ""),
                         new SqlParameter("@Lost_Value", ""),
                         new SqlParameter("@FK_Lost_Competitor_Id", ""),
                         new SqlParameter("@Lost_Remarks", ""),
                         new SqlParameter("@Stop_Remarks", ""),
                         new SqlParameter("@FK_Stop_Competitor_Id", ""),
                         new SqlParameter("@FK_Stop_Reason", "")
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "SetAsInProc", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Oppor_Competitor_List")]
        public HttpResponseMessage Get_Oppor_Competitor_List(int PK_Opportunity_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpporCompetitorListBO>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Won_Value,@Won_Remarks,@Lost_Value,@FK_Lost_Competitor_Id,@Lost_Remarks,@Stop_Remarks,@FK_Stop_Competitor_Id,@FK_Stop_Reason",
                         new SqlParameter("@Action", "Get_Oppor_Competitor_List"),
                         new SqlParameter("@PK_Opportunity_Id", PK_Opportunity_Id),
                         new SqlParameter("@Won_Value", ""),
                         new SqlParameter("@Won_Remarks", ""),
                         new SqlParameter("@Lost_Value", ""),
                         new SqlParameter("@FK_Lost_Competitor_Id", ""),
                         new SqlParameter("@Lost_Remarks", ""),
                         new SqlParameter("@Stop_Remarks", ""),
                         new SqlParameter("@FK_Stop_Competitor_Id", ""),
                         new SqlParameter("@FK_Stop_Reason", "")
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Record saved successfully.";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Get_Oppor_Competitor_List", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Delete_Document")]
        public HttpResponseMessage Delete_Document(int PK_Document_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Document_ListBO>("W2S_SP_Add_Document_Quotation  @Action,@FK_Opportunity_Id,@File_Name,@PK_Document_Id,@PK_Quotation_Id,@PK_Quotation_Id",
                         new SqlParameter("@Action", "Delete_Document"),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@File_Name", ""),
                         new SqlParameter("@PK_Document_Id", PK_Document_Id),
                         new SqlParameter("@PK_Quotation_Id", "")
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, Msg = "Record deleted successfully", Response = "" });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Delete_Document", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Add_Document")]
        public HttpResponseMessage Add_Document(Document_ListBO document_ListBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Document_ListBO>("W2S_SP_Add_Document_Quotation @Action,@FK_Opportunity_Id,@File_Name,@PK_Document_Id,@PK_Quotation_Id",
                         new SqlParameter("@Action", "Add_Document"),
                         new SqlParameter("@FK_Opportunity_Id", document_ListBO.FK_Opportunity_Id),
                         new SqlParameter("@File_Name", document_ListBO.File_Name),
                         new SqlParameter("@PK_Document_Id", ""),
                         new SqlParameter("@PK_Quotation_Id", "")
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Delete_Document", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Get_Competitor_List")]
        public HttpResponseMessage Get_Competitor_List(string FK_Competitor_Type_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<CompetitorBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                           new SqlParameter("@Action", "Get_Competitor_List"),
                           new SqlParameter("@PK_Opportunity_Id", ""),
                           new SqlParameter("@FK_Customer_Id", ""),
                           new SqlParameter("@Opportunity_Name", ""),
                           new SqlParameter("@FK_Opportunity_Id", ""),
                           new SqlParameter("@FK_Competitor_Type_Id", FK_Competitor_Type_Id)
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Delete_Document", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }


        [HttpPost]
        [ActionName("Add_Quotation")]
        public HttpResponseMessage Add_Quotation(Document_ListBO document_ListBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Document_ListBO>("W2S_SP_Add_Document_Quotation @Action,@FK_Opportunity_Id,@File_Name,@PK_Document_Id,@PK_Quotation_Id",
                         new SqlParameter("@Action", "Add_Quotation"),
                         new SqlParameter("@FK_Opportunity_Id", document_ListBO.FK_Opportunity_Id),
                         new SqlParameter("@File_Name", document_ListBO.File_Name),
                         new SqlParameter("@PK_Document_Id", ""),
                         new SqlParameter("@PK_Quotation_Id", "")
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Delete_Document", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Delete_Quotation")]
        public HttpResponseMessage Delete_Quotation(int PK_Quotation_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Document_ListBO>("W2S_SP_Add_Document_Quotation @Action,@FK_Opportunity_Id,@File_Name,@PK_Document_Id,@PK_Quotation_Id",
                         new SqlParameter("@Action", "Delete_Quotation"),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@File_Name", ""),
                         new SqlParameter("@PK_Document_Id", ""),
                         new SqlParameter("@PK_Quotation_Id", PK_Quotation_Id)
                        ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, Msg = "Record deleted successfully", Response = "" });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Delete_Document", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("Add_opporProduct")]
        public HttpResponseMessage Add_opporProduct(GetOpportunityListBO getOpportunityListBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    //if (productBO.PK_Oppor_Product_Id != null)
                    //{
                    //    var data1 = _db.Database.SqlQuery<ProductBO>("W2S_SP_Product @Action,@FK_Sub_Range_Id,@FK_Opportunity_Id,@FK_Range_Id,@Quantity,@Price,@FK_Product_Id",
                    //           new SqlParameter("@Action", "Delete_opporProduct"),
                    //           new SqlParameter("@FK_Sub_Range_Id", ""),
                    //           new SqlParameter("@FK_Opportunity_Id", productBO.PK_Oppor_Product_Id),
                    //           new SqlParameter("@FK_Range_Id", ""),
                    //           new SqlParameter("@Quantity", ""),
                    //           new SqlParameter("@Price", ""),
                    //           new SqlParameter("@FK_Product_Id", "")
                    //           ).ToList();
                    //}
                    //var data = _db.Database.SqlQuery<ProductBO>("W2S_SP_Product @Action,@FK_Sub_Range_Id,@FK_Opportunity_Id,@FK_Range_Id,@Quantity,@Price,@FK_Product_Id",
                    //  new SqlParameter("@Action", "Add_opporProduct"),
                    //  new SqlParameter("@FK_Sub_Range_Id", productBO.FK_Sub_Range_Id),
                    //  new SqlParameter("@FK_Opportunity_Id", productBO.FK_Opportunity_Id),
                    //  new SqlParameter("@FK_Range_Id", productBO.FK_Range_Id),
                    //  new SqlParameter("@Quantity", productBO.Quantity),
                    //  new SqlParameter("@Price", productBO.Price),
                    //  new SqlParameter("@FK_Product_Id", productBO.Product_Id)
                    // ).ToList();
                    if (getOpportunityListBO.ProductBO.Count > 0)
                    {
                        for (int i = 0; i < getOpportunityListBO.ProductBO.Count; i++)
                        {
                            if (getOpportunityListBO.ProductBO[i].ISCHECKED == true)
                            {

                                var data = _db.Database.SqlQuery<ProductBO>("W2S_SP_Product @Action,@FK_Sub_Range_Id,@FK_Opportunity_Id,@FK_Range_Id,@Quantity,@Price,@FK_Product_Id",
                                new SqlParameter("@Action", "Add_opporProduct"),
                                new SqlParameter("@FK_Sub_Range_Id", getOpportunityListBO.ProductBO[i].FK_Sub_Range_Id),
                                new SqlParameter("@FK_Opportunity_Id", getOpportunityListBO.ProductBO[i].FK_Opportunity_Id),
                                //new SqlParameter("@FK_Range_Id", productBO[i].FK_Range_Id),
                                new SqlParameter("@Quantity", getOpportunityListBO.ProductBO[i].Quantity)
                               //new SqlParameter("@Price", productBO.Price),
                               //new SqlParameter("@FK_Product_Id", productBO.Product_Id)
                               ).ToList();
                                if (data != null)
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, data);
                                }
                                else
                                {
                                    return Request.CreateResponse(HttpStatusCode.OK, "False");
                                }
                            }
                        }
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, "");

                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Delete_Document", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }
    }
}
