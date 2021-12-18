using System;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace API_Base.Base
{
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
        private JsonResult CreateResponse(object data, string statuscode)
        {
            DTO result = new DTO();

            switch (statuscode)
            {


                case "Ok":
                    {
                        var objresult = Newtonsoft.Json.JsonConvert.SerializeObject(data,
                                Formatting.None,
                                 new Newtonsoft.Json.JsonSerializerSettings
                                 {
                                     ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                 });

                        result.data = objresult;
                        result.isSuccessful = true;
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                case "Not Found":
                    {
                        result.errors = data;
                        result.isSuccessful = false;
                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                default:
                    break;
            }

            return null;
        }

        protected JsonResult SuccessResponse(object data)
        {
            return this.CreateResponse(data, "Ok");
        }
        //protected JsonResult BadResponse(object data)
        //{
        //    return this.CreateResponse(data, "Not Found");
        //}
        protected JsonResult BadResponse(object data)
        {
            //return this.CreateResponse(data, "Not Found");

            DTO result = new DTO();
            var objresult = Newtonsoft.Json.JsonConvert.SerializeObject(data,
                            Formatting.None,
                             new Newtonsoft.Json.JsonSerializerSettings
                             {
                                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                             });

            result.data = objresult;
            result.isSuccessful = true;
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        #region PictureSaving

        public string[] SavePicture(HttpPostedFileWrapper[] pics)
        {
            string[] profilepath = new string[pics.Length];
            try
            {
                if (pics.Length > 0)
                {
                    for (int i = 0; i < pics.Length; i++)
                    {
                        string filename = System.IO.Path.GetRandomFileName().Replace(".", "") + pics[0].FileName;

                        pics[i].SaveAs(Server.MapPath("/ProductImg/" + filename));
                        profilepath[i] = "/ProductImg/" + filename;
                    }
                }
                return profilepath;

            }
            catch (Exception ex)
            {
                return profilepath;
            }

        }



        #endregion
        //private JsonResult CreateResponse(object data)
        //{
        //    throw new NotImplementedException();
        //}


        // GET: Base
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //    //Thread.Sleep(1000);
        //}
    }
}
