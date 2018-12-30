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
    public class ProductController : ApiController
    {
        ResponseClass response = new ResponseClass();
        AtlasW2SEntities _db = null; 

        [HttpGet]
        [ActionName("GetProductList")]
        public HttpResponseMessage GetProductList()
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ProductDetails>("W2S_SP_Master_Product @Product_Id,@Product_Name,@Product_Desc,@IsActive,@Created_By,@Created_Date,@Modified_By,@Modified_Date,@ACTION",
                       new SqlParameter("@Product_Id", ""),
                       new SqlParameter("@Product_Name", ""),
                       new SqlParameter("@Product_Desc", ""),
                       new SqlParameter("@IsActive", ""), 
                       new SqlParameter("@Created_By", ""),
                       new SqlParameter("@Created_Date", ""), 
                       new SqlParameter("@Modified_By", ""),
                       new SqlParameter("@Modified_Date", ""),
                       new SqlParameter("@Action", "GET_ALL_PRODUCTs")
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
        [ActionName("SubmitProductDetails")]
        public HttpResponseMessage SubmitCustomerDetails(ProductDetails productDetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ProductDetails>("W2S_SP_Master_Product @Product_Id, @Product_Name,@Product_Desc,@IsActive,@Created_By,@Created_Date,@Modified_By,@Modified_Date,@ACTION",
                     new SqlParameter("@Product_Id", productDetails.Product_Id),
                     new SqlParameter("@Product_Name", productDetails.Product_Name),
                     new SqlParameter("@Product_Desc", productDetails.Product_Desc),
                     new SqlParameter("@IsActive", productDetails.IsActive),
                     new SqlParameter("@Created_By", productDetails.Created_By),
                     new SqlParameter("@Created_Date", ""),
                     new SqlParameter("@Modified_By", ""),
                     new SqlParameter("@Modified_Date", ""),
                     new SqlParameter("@ACTION", "INSERT_PRODUCT_DETAILS")
                    
                     ).ToList();
                    response.IsSuccess = true;
                    response.Message = "Product information save successfully.";
                
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
        [ActionName("updateProductDetails")]
        public HttpResponseMessage updateProductDetails(ProductDetails productDetails)
        {
            try
            {
                using (_db = new AtlasW2SEntities())
                {
                    var data = _db.Database.SqlQuery<ProductDetails>("W2S_SP_Master_Product @Product_Id, @Product_Name,@Product_Desc,@IsActive,@Created_By,@Created_Date,@Modified_By,@Modified_Date,@ACTION",
                     new SqlParameter("@Product_Id", productDetails.Product_Id),
                     new SqlParameter("@Product_Name", productDetails.Product_Name),
                     new SqlParameter("@Product_Desc", productDetails.Product_Desc),
                     new SqlParameter("@IsActive", productDetails.IsActive),
                     new SqlParameter("@Created_By", ""),
                     new SqlParameter("@Created_Date", ""),
                     new SqlParameter("@Modified_By", productDetails.Modified_By),
                     new SqlParameter("@Modified_Date", ""),
                     new SqlParameter("@ACTION", "UPDATE_PRODUCT_DETAILS")

                     ).ToList();
                    response.IsSuccess = true;
                    response.Message = "Product information save successfully.";

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

        



    }
}
