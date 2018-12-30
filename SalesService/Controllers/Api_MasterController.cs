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
    public class Api_MasterController : ApiController
    {
        AtlasW2SEntities _db = null;
        ResponseClass response = new ResponseClass();
        [HttpPost]
        [ActionName("Get_Industry_List")]
        public HttpResponseMessage Get_Industry_List()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Industry>("W2S_SP_Master_Industry @Action,@Industry_Name,@Industry_Desc,@IsActive,@Industry_Id",
                         new SqlParameter("@Action", "GETALLINDUSTRies"),
                         new SqlParameter("@Industry_Name", ""),
                         new SqlParameter("@Industry_Desc", ""),
                         new SqlParameter("@IsActive", ""),
                         new SqlParameter("@Industry_Id", "")

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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("AddIndustry")]
        public HttpResponseMessage AddIndustry(Industry industry)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Industry>("W2S_SP_Master_Industry @Action,@Industry_Name,@Industry_Desc,@IsActive,@Industry_Id",
                         new SqlParameter("@Action", "INSERTUPDATE"),
                         new SqlParameter("@Industry_Name", industry.Industry_Name),
                         new SqlParameter("@Industry_Desc", industry.Industry_Desc),
                         new SqlParameter("@IsActive", industry.IsActive),
                         new SqlParameter("@Industry_Id", industry.Industry_Id)

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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("IndustryList")]
        public HttpResponseMessage IndustryList(int Industry_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<Industry>("W2S_SP_Master_Industry @Action,@Industry_Name,@Industry_Desc,@IsActive,@Industry_Id",
                         new SqlParameter("@Action", "GETBYID"),
                         new SqlParameter("@Industry_Name", ""),
                         new SqlParameter("@Industry_Desc", ""),
                         new SqlParameter("@IsActive", ""),
                         new SqlParameter("@Industry_Id", Industry_Id)

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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("EnquirySourceList")]
        public HttpResponseMessage EnquirySourceList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<EnquirySourceBO>("W2S_SP_Master_Enquiry_Source @Enquiry_Source_Name,@Enquiry_Source_Desc,@IsActive,@Action,@Enquiry_Source_Id",
                         new SqlParameter("@Enquiry_Source_Name", ""),
                         new SqlParameter("@Enquiry_Source_Desc", ""),
                         new SqlParameter("@IsActive", ""),
                         new SqlParameter("@Action", "GETALLENQUIRYSOURCEs"),
                         new SqlParameter("@Enquiry_Source_Id", "")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("AddEnquirySource")]
        public HttpResponseMessage AddEnquirySource(EnquirySourceBO enquirySourceBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<EnquirySourceBO>("W2S_SP_Master_Enquiry_Source @Enquiry_Source_Name,@Enquiry_Source_Desc,@IsActive,@Action,@Enquiry_Source_Id",
                         new SqlParameter("@Enquiry_Source_Name", enquirySourceBO.Enquiry_Source_Name),
                         new SqlParameter("@Enquiry_Source_Desc", enquirySourceBO.Enquiry_Source_Desc),
                         new SqlParameter("@IsActive", enquirySourceBO.IsActive),
                         new SqlParameter("@Action", "INSERTUPDATE"),
                         new SqlParameter("@Enquiry_Source_Id", enquirySourceBO.Enquiry_Source_Id)
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("EditEnquirySource")]
        public HttpResponseMessage EditEnquirySource(int Enquiry_Source_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<EnquirySourceBO>("W2S_SP_Master_Enquiry_Source @Enquiry_Source_Name,@Enquiry_Source_Desc,@IsActive,@Action,@Enquiry_Source_Id",
                         new SqlParameter("@Enquiry_Source_Name", ""),
                         new SqlParameter("@Enquiry_Source_Desc", ""),
                         new SqlParameter("@IsActive", ""),
                         new SqlParameter("@Action", "GETBYID"),
                         new SqlParameter("@Enquiry_Source_Id", Enquiry_Source_Id)
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("EnquirytypeList")]
        public HttpResponseMessage EnquirytypeList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterEnquiryTypeBO>("W2S_SP_Master_Enquiry_Type @Enquiry_Type_Name,@Enquiry_Type_Description,@IsActive,@Action,@Enquiry_Type_Id,@Created_By",
                         new SqlParameter("@Enquiry_Type_Name", ""),
                         new SqlParameter("@Enquiry_Type_Description", ""),
                         new SqlParameter("@IsActive", ""),
                         new SqlParameter("@Action", "GETALLENQUIRYTYPEs"),
                         new SqlParameter("@Enquiry_Type_Id", ""),
                         new SqlParameter("@Created_By", "")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("AddEnquirytypeList")]
        public HttpResponseMessage AddEnquirytypeList(MasterEnquiryTypeBO masterEnquiryTypeBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterEnquiryTypeBO>("W2S_SP_Master_Enquiry_Type @Enquiry_Type_Name,@Enquiry_Type_Description,@IsActive,@Action,@Enquiry_Type_Id,@Created_By",
                         new SqlParameter("@Enquiry_Type_Name", masterEnquiryTypeBO.Enquiry_Type_Name),
                         new SqlParameter("@Enquiry_Type_Description", masterEnquiryTypeBO.Enquiry_Type_Description),
                         new SqlParameter("@IsActive", masterEnquiryTypeBO.IsActive),
                         new SqlParameter("@Action", "INSERTUPDATE"),
                         new SqlParameter("@Enquiry_Type_Id", masterEnquiryTypeBO.Enquiry_Type_Id),
                         new SqlParameter("@Created_By", masterEnquiryTypeBO.Created_By)
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPost]
        [ActionName("EditEnquiryType")]
        public HttpResponseMessage EditEnquiryType(int Enquiry_Type_Id)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<MasterEnquiryTypeBO>("W2S_SP_Master_Enquiry_Type @Enquiry_Type_Name,@Enquiry_Type_Description,@IsActive,@Action,@Enquiry_Type_Id,@Created_By",
                         new SqlParameter("@Enquiry_Type_Name", ""),
                         new SqlParameter("@Enquiry_Type_Description", ""),
                         new SqlParameter("@IsActive", ""),
                         new SqlParameter("@Action", "GETBYID"),
                         new SqlParameter("@Enquiry_Type_Id", Enquiry_Type_Id),
                         new SqlParameter("@Created_By", "")
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
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
