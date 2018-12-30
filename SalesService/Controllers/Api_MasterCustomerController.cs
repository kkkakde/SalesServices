using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SalesService.Models;
using SalesService.CustomModel;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;


namespace SalesService.Controllers
{
    public class Api_MasterCustomerController : ApiController
    {
        ResponseClass response = new ResponseClass();
        AtlasW2SEntities _db = null;
        [HttpPost]
        [ActionName("Get_Room_Details_List")]
        public HttpResponseMessage Get_Room_Details_List(Common common)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ContactPersonDesigListBO>("W2S_SP_Master_LIST @Action",
                         new SqlParameter("@Action", "CMPRSOR_ROOM_DTL")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("Get_Contact_Designation_List")]
        public HttpResponseMessage Get_Contact_Person_Designation_List(Common common)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ContactPersonDesigListBO>("W2S_SP_Master_LIST @Action",
                         new SqlParameter("@Action", "COTACT_PERSON_DESIGNATION")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [ActionName("Get_working_status_compressor")]
        public HttpResponseMessage Get_working_status_compressor(Common common)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ContactPersonDesigListBO>("W2S_SP_Master_LIST @Action",
                         new SqlParameter("@Action", "CMPRSOR_WRKNG_STATS")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetCompressorRoomDetails")]
        public HttpResponseMessage GetCompressorRoomDetails(int FK_Cust_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    //var data = _db.Database.SqlQuery<MasterCustomerCompressorRoomDetailsBO>("USP_Get_CompressorRoomDetails  @FK_Cust_Id",
                    //   new SqlParameter("@FK_Cust_Id", FK_Cust_Id)
                    //   ).ToList();
                    var data = _db.Database.SqlQuery<MasterCustomerCompressorRoomDetailsBO>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                             new SqlParameter("@Action", "GET_CUST_ROOM_MAPP"),
                             new SqlParameter("@PK_Cust_Id", FK_Cust_Id)
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetContactPersonDetails")]
        public HttpResponseMessage GetContactPersonDetails(int FK_Cust_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCustomerContactPersonDetailsBO>("USP_Get_ContactPersonDetails  @FK_Cust_Id",
                       new SqlParameter("@FK_Cust_Id", FK_Cust_Id)
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetCustomerList")]
        public HttpResponseMessage GetCustomerList(MasterCustomerBO masterCustomerBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCustomerBO>("W2S_SP_Master_Customer @PK_Cust_Id,@Cust_Name,@Cust_Address_Line1,@Cust_Address_Line2,@FK_Zone_Id,@FK_State_Id,@FK_City_Id,@Cust_GSTN_No,@Cust_Cmprsd_Air_App,@Cust_End_Product,@FK_Cust_Id,@FK_Cust_CntctPrson_Dsgntn_Id,@Cust_CntctPrson_Name,@Cust_CntctPrson_Contact_No,@Cust_CntctPrson_Email_Id,@Cust_Cmprsor_RoomDetails,@Cust_Cmprsor_Model,@Cust_Cmprsor_Mfg_Year,@Cust_Cmprsor_Commissioning_Year,@Cust_Cmprsor_Status,@Created_By,@FK_Industry_Type_Id,@PK_Cust_CntctPrson_Mapng_Id,@PK_Cust_Cmprsor_Mapng_Id,@VisitingCardPath,@Action",
                       new SqlParameter("@PK_Cust_Id", ""),
                       new SqlParameter("@Cust_Name", ""),
                       new SqlParameter("@Cust_Address_Line1", ""),
                       new SqlParameter("@Cust_Address_Line2", ""),
                       new SqlParameter("@FK_Zone_Id", ""),
                       new SqlParameter("@FK_State_Id", ""),
                       new SqlParameter("@FK_City_Id", ""),
                       new SqlParameter("@Cust_GSTN_No", ""),
                       new SqlParameter("@Cust_Cmprsd_Air_App", ""),
                       new SqlParameter("@Cust_End_Product", ""),
                       new SqlParameter("@FK_Cust_Id", ""),
                       new SqlParameter("@FK_Cust_CntctPrson_Dsgntn_Id", ""),
                       new SqlParameter("@Cust_CntctPrson_Name", ""),
                       new SqlParameter("@Cust_CntctPrson_Contact_No", ""),
                       new SqlParameter("@Cust_CntctPrson_Email_Id", ""),
                       new SqlParameter("@Cust_Cmprsor_RoomDetails", ""),
                       new SqlParameter("@Cust_Cmprsor_Model", ""),
                       new SqlParameter("@Cust_Cmprsor_Mfg_Year", ""),
                       new SqlParameter("@Cust_Cmprsor_Commissioning_Year", ""),
                       new SqlParameter("@Cust_Cmprsor_Status", ""),
                       new SqlParameter("@Created_By", masterCustomerBO.Created_By),
                       new SqlParameter("@FK_Industry_Type_Id", ""),
                       new SqlParameter("@PK_Cust_CntctPrson_Mapng_Id", ""),
                       new SqlParameter("@PK_Cust_Cmprsor_Mapng_Id", ""),
                       new SqlParameter("@VisitingCardPath", ""),
                       new SqlParameter("@Action", "GET_CUST_LIST")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("SubmitCustomerDetails")]
        public HttpResponseMessage SubmitCustomerDetails(CustomerDetails customerDetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    if (customerDetails.VisitingCardPath == null)
                        customerDetails.VisitingCardPath = "";
                    var data = _db.Database.SqlQuery<CustomerDetails>("USP_AddCustomerDeatils @PK_Cust_Id, @Cust_Name, @Cust_Address_Line1 , @Cust_Address_Line2, @LandlineNo, @VisitType, @FK_Zone_Id, @FK_State_Id, @FK_City_Id, @Cust_GSTN_No, @Cust_Cmprsd_Air_App, @Cust_End_Product, @Created_By,@VisitingCardPath",
                     new SqlParameter("@PK_Cust_Id", customerDetails.PK_Cust_Id),
                     new SqlParameter("@Cust_Name", customerDetails.Cust_Name),
                     new SqlParameter("@Cust_Address_Line1", customerDetails.Cust_Address_Line1),
                     new SqlParameter("@Cust_Address_Line2", customerDetails.Cust_Address_Line2),
                     new SqlParameter("@LandlineNo", customerDetails.LandlineNo),
                     new SqlParameter("@VisitType", customerDetails.VisitType),
                     new SqlParameter("@FK_Zone_Id", customerDetails.FK_Zone_Id),
                     new SqlParameter("@FK_State_Id", customerDetails.FK_State_Id),
                     new SqlParameter("@FK_City_Id", customerDetails.FK_City_Id),
                     new SqlParameter("@Cust_GSTN_No", customerDetails.Cust_GSTN_No),
                     new SqlParameter("@Cust_Cmprsd_Air_App", customerDetails.Cust_Cmprsd_Air_App),
                     new SqlParameter("@Cust_End_Product", customerDetails.Cust_End_Product),
                     new SqlParameter("@Created_By ", customerDetails.Created_By),
                     new SqlParameter("@VisitingCardPath ", customerDetails.VisitingCardPath)
                     ).ToList();

                    var DELETE = _db.Database.SqlQuery<CustomerDetails>("DELETE FROM W2S_Customer_Industry_Mapping WHERE FK_Cust_Id =" + data[0].PK_Cust_Id).ToList();

                    for (int i = 0; i < customerDetails.PK_Industry_Id.Length; i++)
                    {
                        var INDUSTRY = _db.Database.SqlQuery<CustomerDetails>("USP_AddCustomer_Industry @PK_Cust_Id, @FK_Industry_Type_Id , @Created_By",
                       new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id),
                       new SqlParameter("@FK_Industry_Type_Id", customerDetails.PK_Industry_Id[i]),
                       new SqlParameter("@Created_By ", customerDetails.Created_By)
                       ).ToList();
                    }
                    response.IsSuccess = true;
                    response.Message = "Account information save successfully.";
                    response.ResponseData = new { PK_Cust_Id = data[0].PK_Cust_Id };
                    return Request.CreateResponse(HttpStatusCode.OK, data[0].PK_Cust_Id);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpPost]
        [ActionName("SubmitCustCompressorRoom")]
        public HttpResponseMessage SubmitCustCompressorRoom(CompressorRoomDetails compressorroomdetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<CompressorRoomDetails>("USP_Add_Cust_Compressor_Room @PK_Cust_Id, @Cust_Make, @Cust_Cmprsor_RoomDetails,@Cust_Cmprsor_Model,@Cust_Cmprsor_Mfg_Year,@Cust_Cmprsor_Commissioning_Year,@Cust_Cmprsor_Status,@Created_By,@Remark,@Running_Hours",
                     new SqlParameter("@PK_Cust_Id", compressorroomdetails.PK_Cust_Id),
                     new SqlParameter("@Cust_Make", compressorroomdetails.Cust_Make),
                     new SqlParameter("@Cust_Cmprsor_RoomDetails", compressorroomdetails.Cust_Cmprsor_RoomDetails),
                     new SqlParameter("@Cust_Cmprsor_Model", compressorroomdetails.Cust_Cmprsor_Model),
                     new SqlParameter("@Cust_Cmprsor_Mfg_Year", compressorroomdetails.Cust_Cmprsor_Mfg_Year),
                     new SqlParameter("@Cust_Cmprsor_Commissioning_Year", compressorroomdetails.Cust_Cmprsor_Commissioning_Year),
                     new SqlParameter("@Cust_Cmprsor_Status", compressorroomdetails.Cust_Cmprsor_Status),
                     new SqlParameter("@Created_By", compressorroomdetails.Created_By),
                     new SqlParameter("@Remark", compressorroomdetails.Remark),
                     new SqlParameter("@Running_Hours", compressorroomdetails.Running_Hours)
                     ).ToList();
                    response.IsSuccess = true;
                    response.Message = "Customer information save successfully.";
                    response.ResponseData = new { data = data };
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpPost]
        [ActionName("SubmitCustContactPerson")]
        public HttpResponseMessage SubmitCustContactPerson(MasterCustomerContactPersonDetailsBO1 contactpersonsdetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCustomerContactPersonDetailsBO1>("USP_AddContactPersonDetails  @Action,@PK_Cust_Id ,@FK_Cust_CntctPrson_Dsgntn_Id,@Cust_CntctPrson_Name,@Cust_CntctPrson_Contact_No,@Cust_CntctPrson_Email_Id,@Created_By,@PK_Cust_CntctPrson_Mapng_Id",
                     new SqlParameter("@Action", "Add_Customer_Contct_Person"),
                     new SqlParameter("@PK_Cust_Id", contactpersonsdetails.FK_Cust_Id),
                     new SqlParameter("@FK_Cust_CntctPrson_Dsgntn_Id", contactpersonsdetails.PK_Cust_CntctPrson_Mapng_Id),
                     new SqlParameter("@Cust_CntctPrson_Name", contactpersonsdetails.Cust_CntctPrson_Name),
                     new SqlParameter("@Cust_CntctPrson_Contact_No", contactpersonsdetails.Cust_CntctPrson_Contact_No),
                     new SqlParameter("@Cust_CntctPrson_Email_Id", contactpersonsdetails.Cust_CntctPrson_Email_Id),
                     new SqlParameter("@Created_By", contactpersonsdetails.Created_By),
                     new SqlParameter("@PK_Cust_CntctPrson_Mapng_Id", "")
                     ).ToList();

                    response.IsSuccess = true;
                    response.Message = "Customer contact person details save successfully.";
                    response.ResponseData = new { data = data };
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }

        [HttpPost]
        [ActionName("DeleteCustomerContactPersonDetail")]
        public HttpResponseMessage DeleteCustomerContactPersonDetail(MasterCustomerContactPersonDetailsBO customerBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCustomerBO>("W2S_SP_Master_Customer @PK_Cust_Id,@Cust_Name,@Cust_Address_Line1,@Cust_Address_Line2,@FK_Zone_Id,@FK_State_Id,@FK_City_Id,@Cust_GSTN_No,@Cust_Cmprsd_Air_App,@Cust_End_Product,@FK_Cust_Id,@FK_Cust_CntctPrson_Dsgntn_Id,@Cust_CntctPrson_Name,@Cust_CntctPrson_Contact_No,@Cust_CntctPrson_Email_Id,@Cust_Cmprsor_RoomDetails,@Cust_Cmprsor_Model,@Cust_Cmprsor_Mfg_Year,@Cust_Cmprsor_Commissioning_Year,@Cust_Cmprsor_Status,@Created_By,@FK_Industry_Type_Id,@PK_Cust_CntctPrson_Mapng_Id,@PK_Cust_Cmprsor_Mapng_Id,@VisitingCardPath,@Action",
                       new SqlParameter("@PK_Cust_Id", ""),
                       new SqlParameter("@Cust_Name", ""),
                       new SqlParameter("@Cust_Address_Line1", ""),
                       new SqlParameter("@Cust_Address_Line2", ""),
                       new SqlParameter("@FK_Zone_Id", ""),
                       new SqlParameter("@FK_State_Id", ""),
                       new SqlParameter("@FK_City_Id", ""),
                       new SqlParameter("@Cust_GSTN_No", ""),
                       new SqlParameter("@Cust_Cmprsd_Air_App", ""),
                       new SqlParameter("@Cust_End_Product", ""),
                       new SqlParameter("@FK_Cust_Id", ""),
                       new SqlParameter("@FK_Cust_CntctPrson_Dsgntn_Id", ""),
                       new SqlParameter("@Cust_CntctPrson_Name", ""),
                       new SqlParameter("@Cust_CntctPrson_Contact_No", ""),
                       new SqlParameter("@Cust_CntctPrson_Email_Id", ""),
                       new SqlParameter("@Cust_Cmprsor_RoomDetails", ""),
                       new SqlParameter("@Cust_Cmprsor_Model", ""),
                       new SqlParameter("@Cust_Cmprsor_Mfg_Year", ""),
                       new SqlParameter("@Cust_Cmprsor_Commissioning_Year", ""),
                       new SqlParameter("@Cust_Cmprsor_Status", ""),
                       new SqlParameter("@Created_By", ""),
                       new SqlParameter("@FK_Industry_Type_Id", ""),
                       new SqlParameter("@PK_Cust_CntctPrson_Mapng_Id", customerBO.PK_Cust_CntctPrson_Mapng_Id),
                       new SqlParameter("@PK_Cust_Cmprsor_Mapng_Id", ""),
                       new SqlParameter("@VisitingCardPath", ""),
                       new SqlParameter("@Action", "DELETE_CUST_CONTACT_PERSON_DETAILS")
                       ).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, Msg = "", Response = "Customer contact person deleted successfully." });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("DeleteCustomerCompressorDetail")]
        public HttpResponseMessage DeleteCustomerCompressorDetail(MasterCustomerCompressorRoomDetailsBO customerBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCustomerBO>("W2S_SP_Master_Customer @PK_Cust_Id,@Cust_Name,@Cust_Address_Line1,@Cust_Address_Line2,@FK_Zone_Id,@FK_State_Id,@FK_City_Id,@Cust_GSTN_No,@Cust_Cmprsd_Air_App,@Cust_End_Product,@FK_Cust_Id,@FK_Cust_CntctPrson_Dsgntn_Id,@Cust_CntctPrson_Name,@Cust_CntctPrson_Contact_No,@Cust_CntctPrson_Email_Id,@Cust_Cmprsor_RoomDetails,@Cust_Cmprsor_Model,@Cust_Cmprsor_Mfg_Year,@Cust_Cmprsor_Commissioning_Year,@Cust_Cmprsor_Status,@Created_By,@FK_Industry_Type_Id,@PK_Cust_CntctPrson_Mapng_Id,@PK_Cust_Cmprsor_Mapng_Id,@VisitingCardPath,@Action",
                       new SqlParameter("@PK_Cust_Id", ""),
                       new SqlParameter("@Cust_Name", ""),
                       new SqlParameter("@Cust_Address_Line1", ""),
                       new SqlParameter("@Cust_Address_Line2", ""),
                       new SqlParameter("@FK_Zone_Id", ""),
                       new SqlParameter("@FK_State_Id", ""),
                       new SqlParameter("@FK_City_Id", ""),
                       new SqlParameter("@Cust_GSTN_No", ""),
                       new SqlParameter("@Cust_Cmprsd_Air_App", ""),
                       new SqlParameter("@Cust_End_Product", ""),
                       new SqlParameter("@FK_Cust_Id", ""),
                       new SqlParameter("@FK_Cust_CntctPrson_Dsgntn_Id", ""),
                       new SqlParameter("@Cust_CntctPrson_Name", ""),
                       new SqlParameter("@Cust_CntctPrson_Contact_No", ""),
                       new SqlParameter("@Cust_CntctPrson_Email_Id", ""),
                       new SqlParameter("@Cust_Cmprsor_RoomDetails", ""),
                       new SqlParameter("@Cust_Cmprsor_Model", ""),
                       new SqlParameter("@Cust_Cmprsor_Mfg_Year", ""),
                       new SqlParameter("@Cust_Cmprsor_Commissioning_Year", ""),
                       new SqlParameter("@Cust_Cmprsor_Status", ""),
                       new SqlParameter("@Created_By", ""),
                       new SqlParameter("@FK_Industry_Type_Id", ""),
                       new SqlParameter("@PK_Cust_CntctPrson_Mapng_Id", ""),
                       new SqlParameter("@PK_Cust_Cmprsor_Mapng_Id", customerBO.PK_Cust_Cmprsor_Mapng_Id),
                       new SqlParameter("@VisitingCardPath", ""),
                       new SqlParameter("@Action", "DELETE_CUST_COMPRESSOR_ROOM_DETAILS")
                       ).ToList();

                    return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, Msg = "", Response = "Customer compressor room details deleted successfully." });
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetCustomerDetailsList")]
        public HttpResponseMessage GetCustomerDetailsList(MasterCustomerBO masterCustomerBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var cust_master = _db.Database.SqlQuery<MasterCustomerBO>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                        new SqlParameter("@Action", "GET_CUST_MASTER"),
                        new SqlParameter("@PK_Cust_Id", masterCustomerBO.PK_Cust_Id)
                       ).FirstOrDefault();
                    if (cust_master != null)
                    {

                        var contactperson_mapp = _db.Database.SqlQuery<MasterCustomerContactPersonDetailsBO>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                          new SqlParameter("@Action", "GET_CUST_CPERSON_MAPP"),
                          new SqlParameter("@PK_Cust_Id", masterCustomerBO.PK_Cust_Id)
                         ).ToList();


                        var Compressor_Room_Details = _db.Database.SqlQuery<MasterCustomerCompressorRoomDetailsBO>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                            new SqlParameter("@Action", "GET_CUST_ROOM_MAPP"),
                            new SqlParameter("@PK_Cust_Id", masterCustomerBO.PK_Cust_Id)
                           ).ToList();

                        var Industry_Type = _db.Database.SqlQuery<IndustryTypeDropDownForamt>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                          new SqlParameter("@Action", "GET_CUST_INDUSTRY_MAPP"),
                          new SqlParameter("@PK_Cust_Id", masterCustomerBO.PK_Cust_Id)
                         ).ToList();


                        masterCustomerBO.PK_Cust_Id = cust_master.PK_Cust_Id;
                        masterCustomerBO.Cust_Name = cust_master.Cust_Name;
                        masterCustomerBO.VisitType = cust_master.VisitType;
                        masterCustomerBO.LandlineNo = cust_master.LandlineNo;
                        masterCustomerBO.Cust_Address_Line1 = Convert.ToString(cust_master.Cust_Address_Line1);
                        masterCustomerBO.Cust_Address_Line2 = Convert.ToString(cust_master.Cust_Address_Line2);
                        masterCustomerBO.FK_Zone_Id = new ZoneDropDownForamt { PK_Zone_Id = Convert.ToString(cust_master.FK_Zone), Zone_Name = Convert.ToString(cust_master.zonename) };
                        masterCustomerBO.FK_State_Id = new StateDropDownForamt { PK_State_Id = Convert.ToString(cust_master.FK_State), State_Name = Convert.ToString(cust_master.statename) };
                        masterCustomerBO.FK_City_Id = new CityDropDownForamt { PK_City_Id = Convert.ToString(cust_master.FK_City), City_Name = Convert.ToString(cust_master.cityname) };
                        masterCustomerBO.Cust_GSTN_No = Convert.ToString(cust_master.Cust_GSTN_No);
                        masterCustomerBO.Cust_Cmprsd_Air_App = Convert.ToString(cust_master.Cust_Cmprsd_Air_App);
                        masterCustomerBO.Cust_End_Product = Convert.ToString(cust_master.Cust_End_Product);
                        masterCustomerBO.Url = ConfigurationManager.AppSettings["VisitCardAttachmentPath"] + cust_master.VisitingCardPath;
                        if(cust_master.VisitingCardPath != null && cust_master.VisitingCardPath != "") { masterCustomerBO.VisitingCardPath = Convert.ToString(cust_master.VisitingCardPath).Substring(18); }
                        for (int i = 0; i < contactperson_mapp.Count; i++)
                        {

                            masterCustomerBO.Cust_Contact_Person_List.Add(
                                new MasterCustomerContactPersonDetailsBO
                                {
                                    Cust_CntctPrson_Name = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Name),
                                    Cust_CntctPrson_Contact_No = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Contact_No),
                                    Cust_CntctPrson_Designation = new CommonDropDownForamt { W2S_List_Id = Convert.ToString(contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id) == "" ? "0" : Convert.ToString(contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id) },
                                    Cust_CntctPrson_Email_Id = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Email_Id),
                                    PK_Cust_CntctPrson_Mapng_Id = Convert.ToString(contactperson_mapp[i].PK_Cust_CntctPrson_Mapng_Id) == "" ? 0 : Convert.ToInt32(contactperson_mapp[i].PK_Cust_CntctPrson_Mapng_Id),
                                    Set_DefaultNo = contactperson_mapp[i].Set_DefaultNo,
                                    IsActive=contactperson_mapp[i].IsActive
                                });
                        }
                        for (int i = 0; i < Compressor_Room_Details.Count; i++)
                        {
                            masterCustomerBO.Cust_CompressorRoom_List.Add(
                                new MasterCustomerCompressorRoomDetailsBO
                                {
                                    Cust_Make= Convert.ToString(Compressor_Room_Details[i].Cust_Make),
                                    Cust_Cmprsor_Model = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Model),
                                    Cust_Cmprsor_Mfg_Year = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Mfg_Year),
                                    Cust_Cmprsor_RoomDetailsName = Compressor_Room_Details[i].Cust_Cmprsor_RoomDetailsName,
                                    //Cust_Cmprsor_RoomDetails = new CommonDropDownForamt { W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetails) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetails) },
                                    Cust_Cmprsor_StatusDetails = new CommonDropDownForamt { W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status) },
                                    Cust_Cmprsor_Commissioning_Year = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Commissioning_Year),
                                    PK_Cust_Cmprsor_Mapng_Id = Convert.ToString(Compressor_Room_Details[i].PK_Cust_Cmprsor_Mapng_Id) == "" ? 0 : Convert.ToInt32(Compressor_Room_Details[i].PK_Cust_Cmprsor_Mapng_Id),
                                    Running_Hours = Compressor_Room_Details[i].Running_Hours,
                                    Remark= Compressor_Room_Details[i].Remark
                                });
                        }
                        for (int i = 0; i < Industry_Type.Count; i++)
                        {
                            masterCustomerBO.CustomerIndustryTypeList.Add(
                                new IndustryTypeDropDownForamt
                                {
                                    PK_Industry_Id = Industry_Type[i].PK_Industry_Id,
                                    Industry_Name = Convert.ToString(Industry_Type[i].Industry_Name),
                                    IsSelected = Convert.ToString(Industry_Type[i].IsSelected) == "" ? 0 : Convert.ToInt32(Industry_Type[i].IsSelected)
                                });
                        }
                        if (masterCustomerBO != null)
                        {
                            return Request.CreateResponse(HttpStatusCode.OK, masterCustomerBO);
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, "False");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [ActionName("UpdateCustContactPerson")]
        public HttpResponseMessage UpdateCustContactPerson(MasterCustomerContactPersonDetailsBO1 contactpersonsdetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterCustomerContactPersonDetailsBO1>("USP_AddContactPersonDetails @Action,@PK_Cust_Id ,@FK_Cust_CntctPrson_Dsgntn_Id,@Cust_CntctPrson_Name,@Cust_CntctPrson_Contact_No,@Cust_CntctPrson_Email_Id,@Created_By,@PK_Cust_CntctPrson_Mapng_Id",
                     new SqlParameter("@Action", "Update_Customer_Contct_Person"),
                     new SqlParameter("@PK_Cust_Id", contactpersonsdetails.FK_Cust_Id),
                     new SqlParameter("@FK_Cust_CntctPrson_Dsgntn_Id", ""),
                     new SqlParameter("@Cust_CntctPrson_Name", ""),
                     new SqlParameter("@Cust_CntctPrson_Contact_No", ""),
                     new SqlParameter("@Cust_CntctPrson_Email_Id", ""),
                     new SqlParameter("@Created_By", contactpersonsdetails.Created_By),
                     new SqlParameter("@PK_Cust_CntctPrson_Mapng_Id", contactpersonsdetails.PK_Cust_CntctPrson_Mapng_Id)
                     ).ToList();

                    response.IsSuccess = true;
                    response.Message = "Customer contact person details save successfully.";
                    response.ResponseData = new { data = data };
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "error";
                response.ResponseData = new { ex };
                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
            }
        }
        [HttpPost]
        [ActionName("VisitInOut")]
        public HttpResponseMessage VisitOut(MasterCustomerBO masterCustomerBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    if (masterCustomerBO.Visit_Tracking_Id == null)
                        masterCustomerBO.Visit_Tracking_Id = 0;
                    if (masterCustomerBO.FK_Opportunity_Id == null)
                        masterCustomerBO.FK_Opportunity_Id = 0;

                    var data = _db.Database.SqlQuery<Document_ListBO>("USP_OutVisit @Action, @PK_Cust_Id,@FK_Opportunity_Id,@Visit_Tracking_Id,@CreatedBy,@Latitude,@Longitude",
                           new SqlParameter("@Action", masterCustomerBO.Action),
                           new SqlParameter("@PK_Cust_Id", masterCustomerBO.PK_Cust_Id),
                           new SqlParameter("@FK_Opportunity_Id", masterCustomerBO.FK_Opportunity_Id),
                           new SqlParameter("@Visit_Tracking_Id", masterCustomerBO.Visit_Tracking_Id),
                           new SqlParameter("@CreatedBy", masterCustomerBO.Created_By),
                           new SqlParameter("@Latitude", masterCustomerBO.Latitude == null ? "" : masterCustomerBO.Latitude),
                           new SqlParameter("@Longitude", masterCustomerBO.Longitude == null ? "" : masterCustomerBO.Longitude)
                          ).ToList();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, new { IsSuccess = true, Msg = "Visit Out successfully", Response = "" });
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "False");
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

    }
}
