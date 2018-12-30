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
    public class Api_NPSSourceController : ApiController
    {
        AtlasW2SEntities _db = null;
        ResponseClass response = new ResponseClass();
        [HttpPost]
        [ActionName("WonList")]
        public HttpResponseMessage WonList(NPSSourceBO nPSSourceBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<NPSSourceBO>("W2S_SP_NPS_Scores_List @Action,@Created_By",
                         new SqlParameter("@Action", "WonList"),
                         new SqlParameter("@Created_By", nPSSourceBO.Created_By)

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
        [ActionName("LostList")]
        public HttpResponseMessage LostList(NPSSourceBO nPSSourceBO)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<NPSSourceBO>("W2S_SP_NPS_Scores_List @Action,@Created_By",
                         new SqlParameter("@Action", "LostList"),
                         new SqlParameter("@Created_By", nPSSourceBO.Created_By)

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
