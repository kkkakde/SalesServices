using SalesService.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace SalesService.Controllers
{
    
    public class LoginServiceController : ApiController
    {
        AtlasW2SEntities _db = null;

        public class Login
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        [HttpGet]
        [ActionName("CheckLogin")]
        public HttpResponseMessage CheckLogin(string username, string password)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    //List<W2S_Master_Resource> data = _db.Database.SqlQuery<W2S_Master_Resource>("W2S_SP_Master_Resource @ResourceID",
                    //       new SqlParameter("username", username),
                    //        new SqlParameter("username", password)
                    //     ).ToList();.
                    var data = _db.W2S_Master_Resource.Where(x => x.Resource_Login_Id == username && x.Resource_Password == password).FirstOrDefault();
                   // var data = (from x in _db.W2S_Master_Resource select new { Login=x.Resource_Login_Id }).FirstOrDefault();
                    if (data != null)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, data);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid User");
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
