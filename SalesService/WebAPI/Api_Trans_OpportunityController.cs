using SalesService.Controllers;
using SalesService.CustomModel;
using SalesService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace SalesService.WebAPI
{
    public class Api_Trans_OpportunityController : Api_CommonController
    {
        AtlasW2SEntities _db = null;
        ResponseClass response = new ResponseClass();

        //Aaccount Api (Customer Mastere)
        #region Add Account

        [HttpPost]
        [ActionName("SubmitCustomerDetails")]
        public HttpResponseMessage SubmitCustomerDetails(MasterCustomerBO customerDetails)
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
                     new SqlParameter("@FK_Zone_Id", customerDetails.FK_Zone_Id.PK_Zone_Id == null ? "" : customerDetails.FK_Zone_Id.PK_Zone_Id),
                     new SqlParameter("@FK_State_Id", customerDetails.FK_State_Id.PK_State_Id == null ? "" : customerDetails.FK_State_Id.PK_State_Id),
                     new SqlParameter("@FK_City_Id", customerDetails.FK_City_Id.PK_City_Id == null ? "" : customerDetails.FK_City_Id.PK_City_Id),
                     new SqlParameter("@Cust_GSTN_No", customerDetails.Cust_GSTN_No),
                     new SqlParameter("@Cust_Cmprsd_Air_App", customerDetails.Cust_Cmprsd_Air_App),
                     new SqlParameter("@Cust_End_Product", customerDetails.Cust_End_Product),
                     new SqlParameter("@Created_By ", customerDetails.Created_By),
                     new SqlParameter("@VisitingCardPath ", customerDetails.VisitingCardPath)
                     ).ToList();


                    var DELETE = _db.Database.SqlQuery<CustomerDetails>("DELETE FROM W2S_Customer_Industry_Mapping WHERE FK_Cust_Id =" + data[0].PK_Cust_Id).ToList();

                    foreach (var item in customerDetails.CustomerIndustryTypeList)
                    {
                        if (item.IsSelected == 1)
                        {
                            var INDUSTRY = _db.Database.SqlQuery<CustomerDetails>("USP_AddCustomer_Industry @PK_Cust_Id, @FK_Industry_Type_Id , @Created_By",
                               new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id),
                               new SqlParameter("@FK_Industry_Type_Id", item.PK_Industry_Id),
                               new SqlParameter("@Created_By ", customerDetails.Created_By)
                               ).ToList();
                        }
                    }

                    foreach (var item in customerDetails.Cust_Contact_Person_List)
                    {
                        var contactperson = _db.Database.SqlQuery<MasterCustomerContactPersonDetailsBO1>("USP_AddContactPersonDetails  @Action,@PK_Cust_Id ,@FK_Cust_CntctPrson_Dsgntn_Id,@Cust_CntctPrson_Name,@Cust_CntctPrson_Contact_No,@Cust_CntctPrson_Email_Id,@Created_By,@PK_Cust_CntctPrson_Mapng_Id",
                     new SqlParameter("@Action", "Add_Customer_Contct_Person"),
                     new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id),
                     new SqlParameter("@FK_Cust_CntctPrson_Dsgntn_Id", item.PK_Cust_CntctPrson_Mapng_Id),
                     new SqlParameter("@Cust_CntctPrson_Name", item.Cust_CntctPrson_Name),
                     new SqlParameter("@Cust_CntctPrson_Contact_No", item.Cust_CntctPrson_Contact_No),
                     new SqlParameter("@Cust_CntctPrson_Email_Id", item.Cust_CntctPrson_Email_Id),
                     new SqlParameter("@Created_By", customerDetails.Created_By),
                     new SqlParameter("@PK_Cust_CntctPrson_Mapng_Id", "")
                     ).ToList();
                    }

                    foreach (var item in customerDetails.Cust_CompressorRoom_List)
                    {
                        var Compressor = _db.Database.SqlQuery<CompressorRoomDetails>("USP_Add_Cust_Compressor_Room @PK_Cust_Id,@Cust_Make, @Cust_Cmprsor_RoomDetails,@Cust_Cmprsor_Model,@Cust_Cmprsor_Mfg_Year,@Cust_Cmprsor_Commissioning_Year,@Cust_Cmprsor_Status,@Created_By",
                        new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id),
                        new SqlParameter("@Cust_Make", item.Cust_Make),
                        new SqlParameter("@Cust_Cmprsor_RoomDetails", item.Cust_Cmprsor_RoomDetails.W2S_List_Id),
                        new SqlParameter("@Cust_Cmprsor_Model", item.Cust_Cmprsor_Model),
                        new SqlParameter("@Cust_Cmprsor_Mfg_Year", item.Cust_Cmprsor_Mfg_Year),
                        new SqlParameter("@Cust_Cmprsor_Commissioning_Year", item.Cust_Cmprsor_Commissioning_Year),
                        new SqlParameter("@Cust_Cmprsor_Status", item.Cust_Cmprsor_StatusDetails.W2S_List_Id),
                        new SqlParameter("@Created_By", customerDetails.Created_By)
                        ).ToList();
                    }

                    MasterCustomerBO masterCustomerBO = new MasterCustomerBO();
                    using (_db = new AtlasW2SEntities())
                    {
                        var cust_master = _db.Database.SqlQuery<MasterCustomerBO>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                            new SqlParameter("@Action", "GET_CUST_MASTER"),
                            new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id)
                           ).FirstOrDefault();
                        if (cust_master != null)
                        {

                            var contactperson_mapp = _db.Database.SqlQuery<MasterCustomerContactPersonDetailsBO>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                              new SqlParameter("@Action", "GET_CUST_CPERSON_MAPP"),
                              new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id)
                             ).ToList();


                            var Compressor_Room_Details = _db.Database.SqlQuery<MasterCustomerCompressorRoomDetailsBO>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                                new SqlParameter("@Action", "GET_CUST_ROOM_MAPP"),
                                new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id)
                               ).ToList();

                            var Industry_Type = _db.Database.SqlQuery<IndustryTypeDropDownForamt>("W2S_SP_Get_Customer_Details @Action,@PK_Cust_Id",
                              new SqlParameter("@Action", "GET_CUST_INDUSTRY_MAPP"),
                              new SqlParameter("@PK_Cust_Id", data[0].PK_Cust_Id)
                             ).ToList();


                            masterCustomerBO.PK_Cust_Id = cust_master.PK_Cust_Id;
                            masterCustomerBO.Cust_Name = cust_master.Cust_Name;
                            masterCustomerBO.Cust_Address_Line1 = Convert.ToString(cust_master.Cust_Address_Line1);
                            masterCustomerBO.Cust_Address_Line2 = Convert.ToString(cust_master.Cust_Address_Line2);
                            masterCustomerBO.VisitType = Convert.ToString(cust_master.VisitType);
                            masterCustomerBO.LandlineNo = Convert.ToString(cust_master.LandlineNo);
                            masterCustomerBO.FK_Zone_Id = new ZoneDropDownForamt { PK_Zone_Id = Convert.ToString(cust_master.FK_Zone), Zone_Name = Convert.ToString(cust_master.zonename) };
                            masterCustomerBO.FK_State_Id = new StateDropDownForamt { PK_State_Id = Convert.ToString(cust_master.FK_State), State_Name = Convert.ToString(cust_master.statename) };
                            masterCustomerBO.FK_City_Id = new CityDropDownForamt { PK_City_Id = Convert.ToString(cust_master.FK_City), City_Name = Convert.ToString(cust_master.cityname) };
                            masterCustomerBO.Cust_GSTN_No = Convert.ToString(cust_master.Cust_GSTN_No);
                            masterCustomerBO.Cust_Cmprsd_Air_App = Convert.ToString(cust_master.Cust_Cmprsd_Air_App);
                            masterCustomerBO.Cust_End_Product = Convert.ToString(cust_master.Cust_End_Product);
                            masterCustomerBO.VisitingCardPath = Convert.ToString(cust_master.VisitingCardPath);
                            for (int i = 0; i < contactperson_mapp.Count; i++)
                            {

                                masterCustomerBO.Cust_Contact_Person_List.Add(
                                    new MasterCustomerContactPersonDetailsBO
                                    {
                                        Cust_CntctPrson_Name = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Name),
                                        Cust_CntctPrson_Contact_No = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Contact_No),
                                        List_Desc = Convert.ToString(contactperson_mapp[i].List_Desc),
                                        Cust_CntctPrson_Designation =
                                        new CommonDropDownForamt
                                        {
                                            W2S_List_Id = Convert.ToString(contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id) == "" ? "0" : Convert.ToString(contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id),
                                            List_Desc = Convert.ToString(contactperson_mapp[i].List_Desc) == "" ? "0" : Convert.ToString(contactperson_mapp[i].List_Desc),
                                            IsSelected = true
                                        },
                                        Cust_CntctPrson_Email_Id = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Email_Id),
                                        PK_Cust_CntctPrson_Mapng_Id = Convert.ToString(contactperson_mapp[i].PK_Cust_CntctPrson_Mapng_Id) == "" ? 0 : Convert.ToInt32(contactperson_mapp[i].PK_Cust_CntctPrson_Mapng_Id),
                                        Set_DefaultNo = contactperson_mapp[i].Set_DefaultNo,
                                        FK_Cust_CntctPrson_Dsgntn_Id = contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id
                                    });
                            }
                            for (int i = 0; i < Compressor_Room_Details.Count; i++)
                            {
                                masterCustomerBO.Cust_CompressorRoom_List.Add(
                                    new MasterCustomerCompressorRoomDetailsBO
                                    {
                                        Cust_Cmprsor_Model = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Model),
                                        Cust_Cmprsor_Mfg_Year = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Mfg_Year),
                                        Cust_Cmprsor_RoomDetailsName = Compressor_Room_Details[i].Cust_Cmprsor_RoomDetailsName,
                                        //Cust_Cmprsor_RoomDetails = new CommonDropDownForamt { W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetails) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetails) },
                                        Cust_Cmprsor_StatusDetails = new CommonDropDownForamt
                                        {
                                            W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status),
                                            List_Desc = Convert.ToString(Compressor_Room_Details[i].List_Desc),
                                            IsSelected = true
                                        },
                                        Cust_Cmprsor_RoomDetails = new CommonDropDownForamt
                                        {
                                            W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status),
                                            List_Desc = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetailsName),
                                            IsSelected = true
                                        },
                                        Cust_Cmprsor_Commissioning_Year = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Commissioning_Year),
                                        PK_Cust_Cmprsor_Mapng_Id = Convert.ToString(Compressor_Room_Details[i].PK_Cust_Cmprsor_Mapng_Id) == "" ? 0 : Convert.ToInt32(Compressor_Room_Details[i].PK_Cust_Cmprsor_Mapng_Id)
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
                                response.IsSuccess = true;
                                response.Message = "";
                                response.ResponseData = "";
                                response.Response = masterCustomerBO;
                                return Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = "";
                                response.ResponseData = "";
                                response.Response = masterCustomerBO;
                                return Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }

                        response.IsSuccess = false;
                        response.Message = "Customer Not Found";
                        response.ResponseData = "";
                        response.Response = masterCustomerBO;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    response.IsSuccess = true;
                    response.Message = "Customer information save successfully.";
                    response.ResponseData = "";
                    response.Response = masterCustomerBO;
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
        [ActionName("GetCustomerList")]
        public HttpResponseMessage GetCustomerList(MasterCustomerBO masterCustomerBO)
        {
            try
            {
                if (masterCustomerBO.PK_Cust_Id == null || masterCustomerBO.PK_Cust_Id == 0)
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
                            response.IsSuccess = true;
                            response.Message = "";
                            response.ResponseData = "";
                            response.Response = data;
                            return Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                        else
                        {
                            response.IsSuccess = true;
                            response.Message = "";
                            response.ResponseData = "";
                            response.Response = data;
                            return Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                else
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
                            masterCustomerBO.Cust_Address_Line1 = Convert.ToString(cust_master.Cust_Address_Line1);
                            masterCustomerBO.Cust_Address_Line2 = Convert.ToString(cust_master.Cust_Address_Line2);
                            masterCustomerBO.VisitType = Convert.ToString(cust_master.VisitType);
                            masterCustomerBO.LandlineNo = Convert.ToString(cust_master.LandlineNo);
                            masterCustomerBO.FK_Zone_Id = new ZoneDropDownForamt { PK_Zone_Id = Convert.ToString(cust_master.FK_Zone), Zone_Name = Convert.ToString(cust_master.zonename) };
                            masterCustomerBO.FK_State_Id = new StateDropDownForamt { PK_State_Id = Convert.ToString(cust_master.FK_State), State_Name = Convert.ToString(cust_master.statename) };
                            masterCustomerBO.FK_City_Id = new CityDropDownForamt { PK_City_Id = Convert.ToString(cust_master.FK_City), City_Name = Convert.ToString(cust_master.cityname) };
                            masterCustomerBO.Cust_GSTN_No = Convert.ToString(cust_master.Cust_GSTN_No);
                            masterCustomerBO.Cust_Cmprsd_Air_App = Convert.ToString(cust_master.Cust_Cmprsd_Air_App);
                            masterCustomerBO.Cust_End_Product = Convert.ToString(cust_master.Cust_End_Product);
                            masterCustomerBO.Url = ConfigurationManager.AppSettings["VisitCardAttachmentPath"] + cust_master.VisitingCardPath;
                            masterCustomerBO.VisitingCardPath = Convert.ToString(cust_master.VisitingCardPath).Substring(18);
                            for (int i = 0; i < contactperson_mapp.Count; i++)
                            {

                                masterCustomerBO.Cust_Contact_Person_List.Add(
                                    new MasterCustomerContactPersonDetailsBO
                                    {
                                        Cust_CntctPrson_Name = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Name),
                                        Cust_CntctPrson_Contact_No = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Contact_No),
                                        List_Desc = Convert.ToString(contactperson_mapp[i].List_Desc),
                                        Cust_CntctPrson_Designation =
                                        new CommonDropDownForamt
                                        {
                                            W2S_List_Id = Convert.ToString(contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id) == "" ? "0" : Convert.ToString(contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id),
                                            List_Desc = Convert.ToString(contactperson_mapp[i].List_Desc) == "" ? "0" : Convert.ToString(contactperson_mapp[i].List_Desc),
                                            IsSelected = true
                                        },
                                        Cust_CntctPrson_Email_Id = Convert.ToString(contactperson_mapp[i].Cust_CntctPrson_Email_Id),
                                        PK_Cust_CntctPrson_Mapng_Id = Convert.ToString(contactperson_mapp[i].PK_Cust_CntctPrson_Mapng_Id) == "" ? 0 : Convert.ToInt32(contactperson_mapp[i].PK_Cust_CntctPrson_Mapng_Id),
                                        Set_DefaultNo = contactperson_mapp[i].Set_DefaultNo,
                                        FK_Cust_CntctPrson_Dsgntn_Id = contactperson_mapp[i].FK_Cust_CntctPrson_Dsgntn_Id
                                    });
                            }
                            for (int i = 0; i < Compressor_Room_Details.Count; i++)
                            {
                                masterCustomerBO.Cust_CompressorRoom_List.Add(
                                    new MasterCustomerCompressorRoomDetailsBO
                                    {
                                        Cust_Cmprsor_Model = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Model),
                                        Cust_Cmprsor_Mfg_Year = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Mfg_Year),
                                        Cust_Cmprsor_RoomDetailsName = Compressor_Room_Details[i].Cust_Cmprsor_RoomDetailsName,
                                        //Cust_Cmprsor_RoomDetails = new CommonDropDownForamt { W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetails) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetails) },
                                        Cust_Cmprsor_StatusDetails = new CommonDropDownForamt
                                        {
                                            W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status),
                                            List_Desc = Convert.ToString(Compressor_Room_Details[i].List_Desc),
                                            IsSelected = true
                                        },
                                        Cust_Cmprsor_RoomDetails = new CommonDropDownForamt
                                        {
                                            W2S_List_Id = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status) == "" ? "0" : Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Status),
                                            List_Desc = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_RoomDetailsName),
                                            IsSelected = true
                                        },
                                        Cust_Cmprsor_Commissioning_Year = Convert.ToString(Compressor_Room_Details[i].Cust_Cmprsor_Commissioning_Year),
                                        PK_Cust_Cmprsor_Mapng_Id = Convert.ToString(Compressor_Room_Details[i].PK_Cust_Cmprsor_Mapng_Id) == "" ? 0 : Convert.ToInt32(Compressor_Room_Details[i].PK_Cust_Cmprsor_Mapng_Id)
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
                                response.IsSuccess = true;
                                response.Message = "";
                                response.ResponseData = "";
                                response.Response = masterCustomerBO;
                                return Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = "";
                                response.ResponseData = "";
                                response.Response = masterCustomerBO;
                                return Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                        }

                        response.IsSuccess = false;
                        response.Message = "Customer Not Found";
                        response.ResponseData = "";
                        response.Response = masterCustomerBO;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

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
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = true;
                response.Message = "";
                response.ResponseData = "";
                response.Response = ex;
                return Request.CreateResponse(HttpStatusCode.OK, response);
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
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
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
                    response.Response = data;
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

                    response.IsSuccess = true;
                    response.Message = "Delete Successfully";
                    response.ResponseData = "";
                    response.Response = data;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
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
                    response.IsSuccess = true;
                    response.Message = "Delete Successfully";
                    response.ResponseData = "";
                    response.Response = data;
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        #endregion
        //Add Opportunity Api

        #region Opportunity Api
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
                        "@Expected_Value," +
                        "@Chance_Of_Success," +
                        "@Sales_Phase," +
                        "@Closed_Date," +
                        "@Forecast," +
                        "@Created_By",
                       new SqlParameter("@FK_Customer_Id", addOpportunityBO.FK_Customer_Id),
                       new SqlParameter("@Customer_Contact_No", addOpportunityBO.Customer_Contact_No),
                       new SqlParameter("FK_Opportunity_Source", addOpportunityBO.FK_Opportunity_Source),
                       new SqlParameter("FK_Opportunity_Type", addOpportunityBO.FK_Opportunity_Type),
                       new SqlParameter("Opportunity_Name", addOpportunityBO.Opportunity_Name),
                       new SqlParameter("Expected_Value", addOpportunityBO.Expected_Value),
                       new SqlParameter("Chance_Of_Success", addOpportunityBO.Chance_Of_Success),
                       new SqlParameter("Sales_Phase", addOpportunityBO.Sales_Phase),
                       new SqlParameter("Closed_Date", addOpportunityBO.Closed_Date),
                       new SqlParameter("Forecast", addOpportunityBO.Forecast),
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
        [ActionName("GetOpportunityList")]
        public HttpResponseMessage GetOpportunityList(GetOpportunityListBO getOpportunityListBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<GetOpportunityListBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                       new SqlParameter("@Action", "Get_Opportunity_List"),
                       new SqlParameter("@PK_Opportunity_Id", getOpportunityListBO.Created_By),
                       new SqlParameter("@FK_Customer_Id", ""),
                       new SqlParameter("@Opportunity_Name", ""),
                       new SqlParameter("@FK_Opportunity_Id", ""),
                       new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        [ActionName("Get_Opportunity_Details")]
        public HttpResponseMessage Get_Opportunity_Details(AddOpportunityBO opportunityBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<GetOpportunityListBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                         new SqlParameter("@Action", "Get_Opportunity_Details"),
                         new SqlParameter("@PK_Opportunity_Id", opportunityBO.PK_Opportunity_Id),
                         new SqlParameter("@FK_Customer_Id", ""),
                         new SqlParameter("@Opportunity_Name", ""),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Get_ContactPerson_Details(AddOpportunityBO opportunityBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<CustomerContactPersonDetailsBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                         new SqlParameter("@Action", "Get_ContactPerson_Details"),
                         new SqlParameter("@PK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Customer_Id", opportunityBO.FK_Customer_Id),
                         new SqlParameter("@Opportunity_Name", ""),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Get_OpportunityCompetitorDetails(OpportunityCompetitorBO opportunityCompetitorBO)
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
                          new SqlParameter("@FK_Opportunity_Id", opportunityCompetitorBO.FK_Opportunity_Id),
                          new SqlParameter("@FK_Competitor_Type_Id", "")
                        ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Delete_Competitor(OpportunityCompetitorBO opportunityCompetitorBO)
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
                         new SqlParameter("@PK_Competitor_Id", opportunityCompetitorBO.PK_Competitor_Id)
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
                        response.IsSuccess = false;
                        response.Message = "Not Found";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Get_Quotation_List(QuotationListBO quotationListBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<QuotationListBO>("W2S_SP_AddCompetitor @Action,@FK_Opportunity_Id,@FK_Competitor_Type_Id,@FK_Competitor_Id,@Is_Main,@Comp_Product,@Comp_Price,@PK_Competitor_Id",
                         new SqlParameter("@Action", "Get_Quotation_List"),
                         new SqlParameter("@FK_Opportunity_Id", quotationListBO.FK_Opportunity_Id),
                         new SqlParameter("@FK_Competitor_Type_Id", ""),
                         new SqlParameter("@FK_Competitor_Id", ""),
                         new SqlParameter("@Is_Main", ""),
                         new SqlParameter("@Comp_Product", ""),
                         new SqlParameter("@Comp_Price", ""),
                         new SqlParameter("@PK_Competitor_Id", "")
                        ).ToList();

                    List<QuotationListBO> LisObkj = new List<QuotationListBO>();

                    for (int i = 0; i < data.Count; i++)
                    {
                        QuotationListBO obj = new QuotationListBO();
                        obj.PK_Quotation_Id = data[i].PK_Quotation_Id;
                        obj.FK_Opportunity_Id = data[i].FK_Opportunity_Id;
                        obj.File_Name = data[i].File_Name.Substring(18);
                        obj.Added_Date = data[i].Added_Date;
                        obj.Url = ConfigurationManager.AppSettings["QuatationAttachmentPath"] + data[i].File_Name;
                        LisObkj.Add(obj);
                    }

                    if (data != null || data.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = LisObkj;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = LisObkj;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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

                    if (data != null || data.Count > 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        if (data != null || data.Count > 0)
                        {
                            response.IsSuccess = false;
                            response.Message = "";
                            response.ResponseData = "";
                            response.Response = data;
                            return Request.CreateResponse(HttpStatusCode.OK, response);
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
        [HttpPost]
        [ActionName("Get_Document_List")]
        public HttpResponseMessage Get_Document_List(Document_ListBO document_ListBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Document_ListBO>("W2S_SP_AddCompetitor @Action,@FK_Opportunity_Id,@FK_Competitor_Type_Id,@FK_Competitor_Id,@Is_Main,@Comp_Product,@Comp_Price,@PK_Competitor_Id",
                         new SqlParameter("@Action", "Get_Document_List"),
                         new SqlParameter("@FK_Opportunity_Id", document_ListBO.FK_Opportunity_Id),
                         new SqlParameter("@FK_Competitor_Type_Id", ""),
                         new SqlParameter("@FK_Competitor_Id", ""),
                         new SqlParameter("@Is_Main", ""),
                         new SqlParameter("@Comp_Product", ""),
                         new SqlParameter("@Comp_Price", ""),
                         new SqlParameter("@PK_Competitor_Id", "")
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Get_Sub_Range_List(ChanceOfSuccessBO chanceOfSuccessBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ChanceOfSuccessBO>("W2S_SP_Get_Range_List @Action,@FK_Range_Id",
                         new SqlParameter("@Action", "Get_Sub_Range_List"),
                         new SqlParameter("@FK_Range_Id", chanceOfSuccessBO.FK_Range_Id)
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Get_Product_List(ProductBO productBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ProductBO>("W2S_SP_Product @Action,@FK_Sub_Range_Id,@FK_Opportunity_Id,@FK_Range_Id,@Quantity,@Price,@FK_Product_Id,@Created_By",
                         new SqlParameter("@Action", "Get_Product_List"),
                         new SqlParameter("@FK_Sub_Range_Id", productBO.FK_Sub_Range_Id),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Range_Id", ""),
                         new SqlParameter("@Quantity", ""),
                         new SqlParameter("@Price", ""),
                         new SqlParameter("@FK_Product_Id", ""),
                         new SqlParameter("@Created_By", "")
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
                    var data = _db.Database.SqlQuery<ProductBO>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                          new SqlParameter("@Action", "Get_Oppor_Prod_List"),
                          new SqlParameter("@PK_Opportunity_Id", PK_Opportunity_Id),
                          new SqlParameter("@FK_Customer_Id", ""),
                          new SqlParameter("@Opportunity_Name", ""),
                          new SqlParameter("@FK_Opportunity_Id", ""),
                          new SqlParameter("@FK_Competitor_Type_Id", "")
                        ).ToList();


                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Successfully";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
                        response.IsSuccess = true;
                        response.Message = "Successfully";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Add_Won(StatusClass statusClass)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<StatusClass>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Value,@Competitor_Id,@Remarks,@Reason,@BetterOrder,@Feedback,@Ordervalue,@NextPurchaseFlag,@ModelNo,@SerialNo,@FileName",
                         new SqlParameter("@Action", "Add_Won"),
                         new SqlParameter("@PK_Opportunity_Id", statusClass.PK_Opportunity_Id),
                         new SqlParameter("@Value", statusClass.Value),
                         new SqlParameter("@Competitor_Id", ""),
                         new SqlParameter("@Remarks", statusClass.Remarks),
                         new SqlParameter("@Reason", statusClass.Reason),
                         new SqlParameter("@BetterOrder", statusClass.BetterOrder),
                         new SqlParameter("@Feedback", statusClass.Feedback),
                         new SqlParameter("@Ordervalue", statusClass.Ordervalue),
                         new SqlParameter("@NextPurchaseFlag", ""),
                         new SqlParameter("@ModelNo", ""),
                         new SqlParameter("@SerialNo", ""),
                         new SqlParameter("@FileName", statusClass.filename == null ? "" : statusClass.filename)
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
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Add_Lost(StatusClass statusClass)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<StatusClass>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Value,@Competitor_Id,@Remarks,@Reason,@BetterOrder,@Feedback,@Ordervalue,@NextPurchaseFlag,@ModelNo,@SerialNo,@FileName",
                       new SqlParameter("@Action", "Add_Lost"),
                       new SqlParameter("@PK_Opportunity_Id", statusClass.PK_Opportunity_Id),
                       new SqlParameter("@Value", statusClass.Value),
                       new SqlParameter("@Competitor_Id", ""),
                       new SqlParameter("@Remarks", statusClass.Remarks),
                       new SqlParameter("@Reason", statusClass.Reason),
                       new SqlParameter("@BetterOrder", statusClass.BetterOrder),
                       new SqlParameter("@Feedback", statusClass.Feedback),
                       new SqlParameter("@Ordervalue", ""),
                       new SqlParameter("@NextPurchaseFlag", statusClass.NextPurchaseFlag),
                       new SqlParameter("@ModelNo", ""),
                       new SqlParameter("@SerialNo", ""),
                       new SqlParameter("@FileName", statusClass.filename == null ? "" : statusClass.filename)
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
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Add_Stop(StatusClass statusClass)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<StatusClass>("W2S_SP_Add_Won_Lost @Action,@PK_Opportunity_Id,@Value,@Competitor_Id,@Remarks,@Reason,@BetterOrder,@Feedback,@Ordervalue,@NextPurchaseFlag,@ModelNo,@SerialNo,@FileName",
                       new SqlParameter("@Action", "Add_Stop"),
                       new SqlParameter("@PK_Opportunity_Id", statusClass.PK_Opportunity_Id),
                       new SqlParameter("@Value", ""),
                       new SqlParameter("@Competitor_Id", statusClass.Competitor_Id),
                       new SqlParameter("@Remarks", statusClass.Remarks),
                       new SqlParameter("@Reason", statusClass.Reason),
                       new SqlParameter("@BetterOrder", ""),
                       new SqlParameter("@Feedback", ""),
                       new SqlParameter("@Ordervalue", ""),
                       new SqlParameter("@NextPurchaseFlag", ""),
                       new SqlParameter("@ModelNo", statusClass.ModelNo),
                       new SqlParameter("@SerialNo", statusClass.SerialNo),
                       new SqlParameter("@FileName", statusClass.filename == null ? "" : statusClass.filename)
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
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        public HttpResponseMessage Get_Competitor_List(CompetitorBO competitorBO)
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
                           new SqlParameter("@FK_Competitor_Type_Id", competitorBO.FK_Competitor_Type_Id)
                        ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
                    if (getOpportunityListBO.FK_Opportunity_Id != null)
                    {

                        var data1 = _db.Database.SqlQuery<ProductBO>("W2S_SP_Product @Action,@FK_Sub_Range_Id,@FK_Opportunity_Id,@FK_Range_Id,@Quantity,@Price,@FK_Product_Id,@Created_By",
                               new SqlParameter("@Action", "Delete_opporProduct"),
                               new SqlParameter("@FK_Sub_Range_Id", ""),
                               new SqlParameter("@FK_Opportunity_Id", getOpportunityListBO.FK_Opportunity_Id),
                               new SqlParameter("@FK_Range_Id", ""),
                               new SqlParameter("@Quantity", ""),
                               new SqlParameter("@Price", ""),
                               new SqlParameter("@FK_Product_Id", ""),
                               new SqlParameter("@Created_By", "")
                               ).ToList();
                    }
                    if (getOpportunityListBO.ProductBO.Count > 0)
                    {
                        for (int i = 0; i < getOpportunityListBO.ProductBO.Count; i++)
                        {
                            if (getOpportunityListBO.ProductBO[i].ISCHECKED == true)
                            {

                                var data = _db.Database.SqlQuery<ProductBO>("W2S_SP_Product @Action,@FK_Sub_Range_Id,@FK_Opportunity_Id,@FK_Range_Id,@Quantity,@Price,@FK_Product_Id,@Created_By",
                                new SqlParameter("@Action", "Add_opporProduct"),
                                new SqlParameter("@FK_Sub_Range_Id", getOpportunityListBO.FK_Sub_Range_Id),
                                new SqlParameter("@FK_Opportunity_Id", getOpportunityListBO.FK_Opportunity_Id),
                                new SqlParameter("@FK_Range_Id", getOpportunityListBO.FK_Range_Id),
                                new SqlParameter("@Quantity", getOpportunityListBO.ProductBO[i].Quantity),
                                new SqlParameter("@Price", getOpportunityListBO.ProductBO[i].Price),
                                new SqlParameter("@FK_Product_Id", getOpportunityListBO.ProductBO[i].Product_Id),
                                new SqlParameter("@Created_By", getOpportunityListBO.Created_By)
                               ).ToList();
                            }
                        }
                    }
                    response.IsSuccess = true;
                    response.Message = "Record saved successfully.";
                    response.ResponseData = "";
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
            }
            catch (Exception ex)
            {
                ExceptionHandling(ex.GetType().ToString(), "Delete_Document", ex.Message, "");
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }
        #endregion

        //Add Visit Details
        #region Visit Details api
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
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);

                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);

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
        public HttpResponseMessage Get_OpportunityVisitDetails(OpportunityVisit opportunityVisit)
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
                         new SqlParameter("@FK_Opportunity_Id", opportunityVisit.FK_Opportunity_Id),
                         new SqlParameter("@FK_Competitor_Type_Id", "")
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
                    var data = _db.Database.SqlQuery<OpportunityVisit>("W2S_SP_AddVisits @Action,@PK_Visit,@FK_Opportunity_Id,@FK_Visit_Type_Id,@List_Visit_Start_Date,@List_Visit_Start_Time,@List_Visit_End_Date,@List_Visit_End_Time,@Created_By",
                         new SqlParameter("@Action", "Add_Visits"),
                         new SqlParameter("@PK_Visit", ""),
                         new SqlParameter("@FK_Opportunity_Id", opportunityVisit.FK_Opportunity_Id),
                         new SqlParameter("@FK_Visit_Type_Id", opportunityVisit.FK_Visit_Type_Id),
                         new SqlParameter("@List_Visit_Start_Date", opportunityVisit.List_Visit_Start_Date),
                         new SqlParameter("@List_Visit_Start_Time", opportunityVisit.List_Visit_Start_Time),
                         new SqlParameter("@List_Visit_End_Date", opportunityVisit.List_Visit_End_Date),
                         new SqlParameter("@List_Visit_End_Time", opportunityVisit.List_Visit_End_Time),
                         new SqlParameter("@Created_By", opportunityVisit.Created_By)
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
        [ActionName("VisitInOut")]
        public HttpResponseMessage VisitOut(MasterCustomerBO masterCustomerBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Document_ListBO>("USP_OutVist @Action, @PK_Cust_Id,@FK_Opportunity_Id,@Visit_Tracking_Id,@CreatedBy,@Latitude,@Longitude",
                       new SqlParameter("@Action", masterCustomerBO.Action),
                       new SqlParameter("@PK_Cust_Id", masterCustomerBO.PK_Cust_Id == null ? 0 : masterCustomerBO.PK_Cust_Id),
                       new SqlParameter("@FK_Opportunity_Id", masterCustomerBO.FK_Opportunity_Id == null ? 0 : masterCustomerBO.FK_Opportunity_Id),
                       new SqlParameter("@Visit_Tracking_Id", masterCustomerBO.Visit_Tracking_Id == null ? 0 : masterCustomerBO.Visit_Tracking_Id),
                       new SqlParameter("@CreatedBy", masterCustomerBO.Created_By),
                       new SqlParameter("@Latitude", masterCustomerBO.Latitude == null ? "" : masterCustomerBO.Latitude),
                       new SqlParameter("@Longitude", masterCustomerBO.Longitude == null ? "" : masterCustomerBO.Longitude)
                      ).ToList();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "Successfully";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { Error = ex.InnerException.Message });
            }
        }

        [HttpPost]
        [ActionName("VisitList")]
        public HttpResponseMessage VisitList(OpportunityVisit opportunityVisit)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    //var data = _db.Database.SqlQuery<OpportunityVisit>("W2S_SP_Get_Opportunity_List @Action,@PK_Opportunity_Id,@FK_Customer_Id,@Opportunity_Name,@FK_Opportunity_Id,@FK_Competitor_Type_Id",
                    //     new SqlParameter("@Action", "Visit_List"),
                    //     new SqlParameter("@PK_Opportunity_Id", ""),
                    //     new SqlParameter("@FK_Customer_Id", ""),
                    //     new SqlParameter("@Opportunity_Name", ""),
                    //     new SqlParameter("@FK_Opportunity_Id", ""),
                    //     new SqlParameter("@FK_Competitor_Type_Id", "")
                    //   ).ToList();
                    var data = _db.Database.SqlQuery<OpportunityVisit>("W2S_SP_VisitDetails @Created_By ",
                         new SqlParameter("@Created_By", opportunityVisit.Created_By)
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
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
        [ActionName("DeleteVisits")]
        public HttpResponseMessage DeleteVisits(int PK_Visit)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<OpportunityVisit>("W2S_SP_AddVisits @Action,@PK_Visit,@FK_Opportunity_Id,@FK_Visit_Type_Id,@List_Visit_Start_Date,@List_Visit_Start_Time,@List_Visit_End_Date,@List_Visit_End_Time,@Created_By",
                         new SqlParameter("@Action", "Delete_Visits"),
                         new SqlParameter("@PK_Visit", PK_Visit),
                         new SqlParameter("@FK_Opportunity_Id", ""),
                         new SqlParameter("@FK_Visit_Type_Id", ""),
                         new SqlParameter("@List_Visit_Start_Date", ""),
                         new SqlParameter("@List_Visit_Start_Time", ""),
                         new SqlParameter("@List_Visit_End_Date", ""),
                         new SqlParameter("@List_Visit_End_Time", ""),
                         new SqlParameter("@Created_By", "")
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

        #endregion

        //Commen api
        #region Commen api
        [HttpPost]
        [ActionName("GetZoneList")]
        public HttpResponseMessage GetZoneList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ZoneListBO>("W2S_SP_Master_LIST @Action",
                         new SqlParameter("@Action", "GETZONELIST")
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetStateList")]
        public HttpResponseMessage GetStateList(CustomerDetails customerListBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<StateListBO>("W2S_SP_Master_LIST @Action,@FK_Zone_Id",
                         new SqlParameter("@Action", "GETSTATELIST"),
                           new SqlParameter("@FK_Zone_Id", customerListBO.FK_Zone_Id)
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetCityList")]
        public HttpResponseMessage GetCityList(Common common)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<CityListBO>("W2S_SP_Master_LIST @Action,@FK_Zone_Id,@FK_State_Id",
                         new SqlParameter("@Action", "GETCITYLIST"),
                         new SqlParameter("@FK_Zone_Id", ""),
                         new SqlParameter("@FK_State_Id", common.State_Id)
                       ).ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("GetIndustryList")]
        public HttpResponseMessage GetIndustryList(Common common)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Industry>("W2S_SP_Master_Industry '','',null,'GETALLINDUSTRies',0").ToList();

                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        #endregion

        //Login API
        #region Login API

        [HttpPost]
        [ActionName("Login")]
        public HttpResponseMessage Login(User user)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<LoginModelBO>("W2S_SP_MASTER_USER @Action,@PK_Resource_Id,@FK_Designation_Id,@FK_Zone_Id,@FK_State_Id,@Resource_Name,@Resource_Login_Id,@Resource_Password,@Resource_Mobile_No,@Navigation_Parent_Id,@Resource_Email_Id,@isActive,@Created_By,@Created_Date,@Modified_By,@Modified_Date",
                         new SqlParameter("@Action", "GETUSERLOGINDETAILS"),
                         new SqlParameter("@PK_Resource_Id", ""),
                         new SqlParameter("@FK_Designation_Id", ""),
                         new SqlParameter("@FK_Zone_Id", ""),
                         new SqlParameter("@FK_State_Id", ""),
                         new SqlParameter("@Resource_Name", ""),
                         new SqlParameter("@Resource_Login_Id", user.username),
                         new SqlParameter("@Resource_Password", user.password),
                         new SqlParameter("@Resource_Mobile_No", ""),
                         new SqlParameter("@Navigation_Parent_Id", ""),
                         new SqlParameter("@Resource_Email_Id", ""),
                         new SqlParameter("@isActive", ""),
                         new SqlParameter("@Created_By", ""),
                         new SqlParameter("@Created_Date", ""),
                         new SqlParameter("@Modified_By", ""),
                         new SqlParameter("@Modified_Date", "")
                       ).FirstOrDefault();
                    if (data != null)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("ForgotPassword")]
        public HttpResponseMessage ForgotPassword(string EmailID)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<LoginModelBO>("SP_GetAllUser @EmailID,@Action",
                         new SqlParameter("@EmailID", EmailID),
                         new SqlParameter("@Action", "GETEMAILs")
                       ).FirstOrDefault();

                    if (data != null)
                    {
                        try
                        {
                            using (MailMessage mailMessage = new MailMessage())
                            {
                                mailMessage.From = new MailAddress(ConfigurationManager.AppSettings["MailFrom"]);
                                mailMessage.Subject = "Sales project reset password ";
                                mailMessage.Body = "Hi, <br/> Your current password=" + data.Resource_Password;
                                mailMessage.IsBodyHtml = true;
                                //if (!string.IsNullOrEmpty(ccDetailId))
                                //{
                                //    mailMessage.CC.Add(new MailAddress(ccDetailId));
                                //}
                                //krios.fdtraining@gmail.com
                                //fddms@12345
                                string arMailTo = data.Resource_Email_Id;
                                mailMessage.To.Add(new MailAddress(arMailTo));
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = ConfigurationManager.AppSettings["Host"]; //reading from web.config 
                                smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]); //reading from web.config 
                                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                                NetworkCred.UserName = ConfigurationManager.AppSettings["MailFrom"]; //reading from web.config 
                                NetworkCred.Password = ConfigurationManager.AppSettings["Password"]; //reading from web.config 
                                smtp.UseDefaultCredentials = false;
                                smtp.Credentials = NetworkCred;
                                smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config 
                                smtp.Send(mailMessage);
                                if (data != null)
                                {
                                    response.IsSuccess = true;
                                    response.Message = "";
                                    response.ResponseData = "";
                                    response.Response = data;
                                    return Request.CreateResponse(HttpStatusCode.OK, response);
                                }
                                else
                                {
                                    response.IsSuccess = false;
                                    response.Message = "";
                                    response.ResponseData = "";
                                    response.Response = data;
                                    return Request.CreateResponse(HttpStatusCode.OK, response);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = false;
                            response.Message = "";
                            response.ResponseData = "";
                            response.Response = ex;
                            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                        }
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        public HttpResponseMessage ChangePassword(ChangePasswordBO changePasswordBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<int>("W2S_SP_ChangePassword @User_ID,@Password,@NEWPassword",
                         new SqlParameter("@User_ID", changePasswordBO.User_ID),
                         new SqlParameter("@Password", changePasswordBO.Password),
                         new SqlParameter("@NEWPassword", changePasswordBO.NEWPassword)
                       ).FirstOrDefault();
                    if (data != 0)
                    {
                        response.IsSuccess = true;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "";
                        response.ResponseData = "";
                        response.Response = data;
                        return Request.CreateResponse(HttpStatusCode.OK, response);
                    }
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "";
                response.ResponseData = "";
                response.Response = ex;
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
        }
        #endregion
    }
}

