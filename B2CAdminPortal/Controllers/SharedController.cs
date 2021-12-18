using API_Base.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace B2CAdminPortal.Controllers
{
    public class SharedController : BaseController
    {
        public JsonResult UploadImage()
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            
            try
            {
                var file = Request.Files[0];

                var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

                var path = Path.Combine(Server.MapPath("~/ProductImg/"), fileName);

                file.SaveAs(path);

                result.Data = new { Success = true, ImageURL = string.Format("/ProductImg/{0}", fileName) };

                //var newImage = new Image() { Name = fileName };

                //if (ImagesService.Instance.SaveNewImage(newImage))
                //{
                //    result.Data = new { Success = true, Image = fileName, File = newImage.ID, ImageURL = string.Format("{0}{1}", Variables.ImageFolderPath, fileName) };
                //}
                //else
                //{
                //    result.Data = new { success = false, Message = new HttpStatusCodeResult(500) };
                //}
            }
            catch (Exception ex)
            {
                result.Data = new { Success = false, Message = ex.Message };
            }

            return result;
        }


    }
}