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


namespace SalesService.Controllers
{
    public class RangeMappingController : ApiController
    {
        ResponseClass response = new ResponseClass();
        AtlasW2SEntities _db = null;

        [HttpGet]
        [ActionName("GetSubRangeDetails")]
        public HttpResponseMessage GetSubRangeDetails()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<SubRangeDetails>("USP_Add_SubRange @PK_SubRange_Id, @FK_W2S_List_Id,@SubRange_Name,@SubRange_Description,@IsActive,@Created_Date,@Created_By,@Modified_Date,@Modified_By,@ACTION",
                         new SqlParameter("@PK_SubRange_Id", ""),
                         new SqlParameter("@FK_W2S_List_Id", ""),
                         new SqlParameter("@SubRange_Name", ""),
                         new SqlParameter("@SubRange_Description", ""),
                         new SqlParameter("@IsActive", ""),
                         new SqlParameter("@Created_Date", ""),
                         new SqlParameter("@Created_By", ""),
                         new SqlParameter("@Modified_Date", ""),
                         new SqlParameter("@Modified_By", ""),
                         new SqlParameter("@Action", "GET_ALL_SUBRANGE_DETAILS")
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
        [ActionName("SubmitSubRangeDetails")]
        public HttpResponseMessage SubmitSubRangeDetails(SubRangeDetails subRangeDetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<SubRangeDetails>("USP_Add_SubRange @PK_SubRange_Id, @FK_W2S_List_Id,@SubRange_Name,@SubRange_Description,@IsActive,@Created_Date,@Created_By,@Modified_Date,@Modified_By,@ACTION",
                     new SqlParameter("@PK_SubRange_Id", subRangeDetails.PK_SubRange_Id),
                     new SqlParameter("@FK_W2S_List_Id", subRangeDetails.FK_W2S_List_Id),
                     new SqlParameter("@SubRange_Name", subRangeDetails.SubRange_Name),
                     new SqlParameter("@SubRange_Description", subRangeDetails.SubRange_Description),
                     new SqlParameter("@IsActive", subRangeDetails.IsActive),
                     new SqlParameter("@Created_Date", ""),
                     new SqlParameter("@Created_By", ""),
                     new SqlParameter("@Modified_Date", ""),
                     new SqlParameter("@Modified_By", ""),
                     new SqlParameter("@ACTION", "INSERT_SUBRANGE_DETAILS")
                    
                     ).ToList();
                    response.IsSuccess = true;
                    response.Message = "SubRange information save successfully.";
                
                    return Request.CreateResponse(HttpStatusCode.OK, data);
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
        [ActionName("UpdateSubRangeDetails")]
        public HttpResponseMessage UpdateSubRangeDetails(SubRangeDetails subRangeDetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<SubRangeDetails>("USP_Add_SubRange @PK_SubRange_Id, @FK_W2S_List_Id,@SubRange_Name,@SubRange_Description,@IsActive,@Created_Date,@Created_By,@Modified_Date,@Modified_By,@ACTION",
                       new SqlParameter("@PK_SubRange_Id", subRangeDetails.PK_SubRange_Id),
                       new SqlParameter("@FK_W2S_List_Id", subRangeDetails.FK_W2S_List_Id),
                       new SqlParameter("@SubRange_Name", subRangeDetails.SubRange_Name),
                       new SqlParameter("@SubRange_Description", subRangeDetails.SubRange_Description),
                       new SqlParameter("@IsActive", subRangeDetails.IsActive),
                       new SqlParameter("@Created_Date", ""),
                       new SqlParameter("@Created_By", ""),
                       new SqlParameter("@Modified_Date", ""),
                       new SqlParameter("@Modified_By", ""),
                       new SqlParameter("@ACTION", "UPDATE_SUBRANGE_DETAILS")

                     ).ToList();
                    response.IsSuccess = true;
                    response.Message = "SubRange information save successfully.";

                    return Request.CreateResponse(HttpStatusCode.OK, data);
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




        //[HttpPost]
        //[ActionName("AddRangeMapping")]
        //public HttpResponseMessage AddRangeMapping(RangeMappingDetails subRangeDetails)
        //{
        //    try
        //    {
        //        using (_db = new AtlasW2SEntities())
        //        {
        //            var data = _db.Database.SqlQuery<RangeMappingDetails>("USP_Add_RangeMapping @FK_MapType_Id,@FK_Range_Id,@FK_Sub_Range_Id,FK_Prod_Id,@IsActive,@Created_Date,@Created_By,@Modified_Date,@Modified_By,@FK_W2S_List_Id,@ACTION",

        //new SqlParameter("@FK_MapType_Id", subRangeDetails.FK_MapType_Id),
        //             new SqlParameter("@FK_Range_Id", subRangeDetails.FK_Range_Id),
        //             new SqlParameter("@FK_Sub_Range_Id", subRangeDetails.FK_Sub_Range_Id), 
        //             new SqlParameter("@FK_Prod_Id", 0),
        //             new SqlParameter("@IsActive", subRangeDetails.IsActive),
        //             new SqlParameter("@Created_Date", ""),
        //             new SqlParameter("@Created_By", 0),
        //             new SqlParameter("@Modified_Date", ""),
        //             new SqlParameter("@Modified_By", 0),
        //             new SqlParameter("@FK_W2S_List_Id", subRangeDetails.FK_W2S_List_Id),
        //             new SqlParameter("@ACTION", "INSERT_RANGE_MAPPING")

        //             ).ToList();
        //            response.IsSuccess = true;
        //            response.Message = "Range information save successfully.";


        //            return Request.CreateResponse(HttpStatusCode.OK, data);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.IsSuccess = false;
        //        response.Message = "error";
        //        response.ResponseData = new { ex };
        //        return Request.CreateResponse(HttpStatusCode.BadRequest, response);
        //    }
        //}

        [HttpPost]
        [ActionName("AddRangeMapping")]
        public HttpResponseMessage AddRangeMapping(RangeMappingDetails subRangeDetails)
        {
            try
            {
                //using (_db = new AtlasW2SEntities())
                int result = 0;
                using (_db = new AtlasW2SEntities())
                {
                    // using (SqlConnection connection = new SqlConnection("data source=DESKTOP-F8J3KIS\\SQLSERVER;initial catalog=AtlasW2S;user id=sa;password=welcome@123;"))
                    using (SqlConnection connection = new SqlConnection(_db.Database.Connection.ConnectionString))
                    {
                        connection.Open();
                        using (var command = new SqlCommand())
                        {
                            try
                            {
                                command.CommandType = CommandType.StoredProcedure;
                                command.Connection = connection;
                                command.CommandText = "USP_Add_RangeMapping";
                                command.Parameters.Add(new SqlParameter("@FK_MapType_Id", subRangeDetails.FK_MapType_Id));
                                command.Parameters.Add(new SqlParameter("@FK_Range_Id", subRangeDetails.FK_Range_Id));
                                command.Parameters.Add(new SqlParameter("@FK_Sub_Range_Id", subRangeDetails.FK_Sub_Range_Id));
                                command.Parameters.Add(new SqlParameter("@FK_Prod_Id", subRangeDetails.FK_Prod_Id));
                                command.Parameters.Add(new SqlParameter("@IsActive", subRangeDetails.IsActive));
                                command.Parameters.Add(new SqlParameter("@Created_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Created_By", subRangeDetails.Created_By));
                                command.Parameters.Add(new SqlParameter("@Modified_Date", ""));
                                command.Parameters.Add(new SqlParameter("@Modified_By", 0));
                                command.Parameters.Add(new SqlParameter("@FK_W2S_List_Id", subRangeDetails.FK_W2S_List_Id));
                                command.Parameters.Add(new SqlParameter("@ACTION", "INSERT_RANGE_MAPPING"));
                                result = command.ExecuteNonQuery();
                                response.IsSuccess = true;
                                response.Message = "Range information save successfully.";
                                return Request.CreateResponse(HttpStatusCode.OK, result);
                            }
                            catch (Exception ex)
                            {
                                response.IsSuccess = false;
                                response.Message = "error";
                                response.ResponseData = new { ex };
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }
                        }
                    }
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

        [HttpGet]
        [ActionName("GetRangeMappingDetails")]
        public HttpResponseMessage GetRangeMappingDetails()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<RangeMapping>("USP_Range_Mapping @PK_RangeMapping_Id, @FK_MapType_Id, @FK_Prod_Id, @ProductName, @FK_Range_Id, @FK_Sub_Range_Id, @SubRangeName,@ACTION",
                         new SqlParameter("@PK_RangeMapping_Id", ""),
                         new SqlParameter("@FK_MapType_Id", ""),
                         new SqlParameter("@FK_Prod_Id", ""),
                         new SqlParameter("@ProductName", ""),
                         new SqlParameter("@FK_Range_Id", ""),
                         new SqlParameter("@FK_Sub_Range_Id", ""),
                         new SqlParameter("@SubRangeName", ""),
                         new SqlParameter("@Action", "GET_RANGE_MAPPING_DETAILS")
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



    }
}
