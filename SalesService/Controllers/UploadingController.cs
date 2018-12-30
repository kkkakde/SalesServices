using SalesService.CustomModel;
using SalesService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SalesService.Controllers
{
    public class UploadingController : ApiController
    {
        ResponseClass response = new ResponseClass();
        AtlasW2SEntities _db = null;
        #region VisitingCard
        [HttpPost]
        [ActionName("VisitingCard")]
        public HttpResponseMessage Add_Image()
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                }
            }
            #region File Upload
            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    if (httpRequest.Files[i].ContentLength > 0 && httpRequest.Files[i] != null)
                    {
                        try
                        {
                            string fileExtension = System.IO.Path.GetExtension(httpRequest.Files[i].FileName);
                            string filename = Path.GetFileName(httpRequest.Files[i].FileName);
                            string[] Name = filename.Split('.');
                            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".pdf" || fileExtension == ".png")
                            {
                                string UniqueFileName = string.Format(@"{0}{1}", DateTime.Now.Ticks, httpRequest.Files[i].FileName);
                                string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/CustomerVisitingCard"), UniqueFileName);
                                httpRequest.Files[i].SaveAs(path);

                                response.IsSuccess = true;
                                response.Message = "Uploaded successfully.";
                                response.Response = UniqueFileName;
                                return Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = "Select (.jpg |.jpeg |.pdf|.png) File";
                                response.Response = null;
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }
                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = false;
                            response.Message = "Invalid File";
                            response.Response = ex;
                            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                        }
                    }
                }
            }

            #endregion
            response.IsSuccess = false;
            response.Message = "File Not found";
            response.Response = null;
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);

        }
        #endregion
        #region Quotation Ducument
        [HttpPost]
        [ActionName("Quotationducument")]
        public HttpResponseMessage Quotationducument(int FK_Opportunity_Id)
        {


            var httpRequest = System.Web.HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                }
            }
            #region File Upload
            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    if (httpRequest.Files[i].ContentLength > 0 && httpRequest.Files[i] != null)
                    {
                        string UniqueFileName = "";
                        try
                        {
                            string fileExtension = System.IO.Path.GetExtension(httpRequest.Files[i].FileName);
                            string filename = Path.GetFileName(httpRequest.Files[i].FileName);
                            string[] Name = filename.Split('.');
                            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".pdf" || fileExtension == ".png" || fileExtension == ".xls" || fileExtension == ".xlsx" || fileExtension == ".docs" || fileExtension == ".doc")
                            {
                                UniqueFileName = string.Format(@"{0}{1}", DateTime.Now.Ticks, httpRequest.Files[i].FileName);
                                string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Quotation_Document"), UniqueFileName);
                                httpRequest.Files[i].SaveAs(path);
                                using (_db = new AtlasW2SEntities())
                                {
                                    var data = _db.Database.SqlQuery<QuotationListBO>("USP_AddQuotation @Action, @PK_Quotation_Id,@FK_Opportunity_Id,@File_Name",
                                         new SqlParameter("@Action", "Add_Quotation"),
                                         new SqlParameter("@PK_Quotation_Id", ""),
                                         new SqlParameter("@FK_Opportunity_Id", FK_Opportunity_Id),
                                         new SqlParameter("@File_Name", UniqueFileName)
                                       ).ToList();
                                }
                                response.IsSuccess = true;
                                response.Message = "Uploaded Successfully";
                                response.Response = UniqueFileName;
                                return Request.CreateResponse(HttpStatusCode.OK, response);

                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = "Invalid File";
                                response.Response = UniqueFileName;
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }

                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = false;
                            response.Message = "Invalid File";
                            response.Response = ex;
                            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                        }

                    }
                }
            }
            #endregion
            response.IsSuccess = false;
            response.Message = "File Not Found";
            response.Response = null;
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);

        }
        #endregion
        #region Quotation Ducument
        [HttpPost]
        [ActionName("Gallary")]
        public HttpResponseMessage Gallary(int? Cust_iD)
        {
            var httpRequest = System.Web.HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                }
            }
            #region File Upload
            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    if (httpRequest.Files[i].ContentLength > 0 && httpRequest.Files[i] != null)
                    {
                        try
                        {
                            string fileExtension = System.IO.Path.GetExtension(httpRequest.Files[i].FileName);
                            string filename = Path.GetFileName(httpRequest.Files[i].FileName);
                            string[] Name = filename.Split('.');
                            if (fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                            {
                                string UniqueFileName = string.Format(@"{0}{1}", DateTime.Now.Ticks, httpRequest.Files[i].FileName);
                                string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Gallary"), UniqueFileName);
                                httpRequest.Files[i].SaveAs(path);

                                response.IsSuccess = true;
                                response.Message = "Uploaded Succeefully";
                                response.Response = null;
                                return Request.CreateResponse(HttpStatusCode.OK, response);
                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = "Invalid File";
                                response.Response = null;
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }
                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = true;
                            response.Message = "Invalid File";
                            response.Response = ex;
                            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                        }
                    }
                }
            }
            #endregion
            response.IsSuccess = false;
            response.Message = "File Not Found";
            response.Response = null;
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);

        }

        #endregion

        [HttpPost]
        [ActionName("wonducument")]
        public HttpResponseMessage wonducument(int FK_Opportunity_Id)
        {


            var httpRequest = System.Web.HttpContext.Current.Request;
            if (httpRequest.Files.Count > 0)
            {
                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];
                }
            }
            #region File Upload
            if (httpRequest.Files.Count > 0)
            {
                for (int i = 0; i < httpRequest.Files.Count; i++)
                {
                    if (httpRequest.Files[i].ContentLength > 0 && httpRequest.Files[i] != null)
                    {
                        string UniqueFileName = "";
                        try
                        {
                            string fileExtension = System.IO.Path.GetExtension(httpRequest.Files[i].FileName);
                            string filename = Path.GetFileName(httpRequest.Files[i].FileName);
                            string[] Name = filename.Split('.');
                            if (fileExtension == ".pdf")
                            {
                                UniqueFileName = string.Format(@"{0}{1}", DateTime.Now.Ticks, httpRequest.Files[i].FileName);
                                string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Content/Won_Document"), UniqueFileName);
                                httpRequest.Files[i].SaveAs(path);
                                response.IsSuccess = true;
                                response.Message = "Uploaded Successfully";
                                response.Response = UniqueFileName;
                                return Request.CreateResponse(HttpStatusCode.OK, response);

                            }
                            else
                            {
                                response.IsSuccess = false;
                                response.Message = "Invalid File";
                                response.Response = UniqueFileName;
                                return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                            }

                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = false;
                            response.Message = "Invalid File";
                            response.Response = ex;
                            return Request.CreateResponse(HttpStatusCode.BadRequest, response);
                        }

                    }
                }
            }
            #endregion
            response.IsSuccess = false;
            response.Message = "File Not Found";
            response.Response = null;
            return Request.CreateResponse(HttpStatusCode.BadRequest, response);

        }
    }
}
