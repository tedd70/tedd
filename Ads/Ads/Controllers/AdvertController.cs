using System;
using Ads.Database;
using Ads.Services;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ads.Controllers
{
    public class AdvertController : Controller
    {
        private IAdsDbContext _adsDbContext;
        public AdvertController()
        {
            _adsDbContext = new AdsDbContext();
        }
        public ActionResult Iframe(int advertId)
        {
            var advert = GetAdvertById(advertId);
            return View(advert);
        }

        [HttpGet]
        public JsonResult GetAdvert(int advertId)
        {
            return Json(GetAdvertById(advertId), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveAdvert(Advert advert)
        {
            var response = _adsDbContext.Update(advert);
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var result = "";
            if (Request.Files.AllKeys.Any())
            {
                var httpPostedFile = Request.Files["UploadedImage"];

                if (httpPostedFile != null)
                {
                    var fileName = new Guid().ToString();
                    var fileSavePath = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                    httpPostedFile.SaveAs(fileSavePath);
                    result = fileSavePath;
                }
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private Advert GetAdvertById(int advertId)
        {
            return _adsDbContext.GetById(advertId);
        }
    }
}
