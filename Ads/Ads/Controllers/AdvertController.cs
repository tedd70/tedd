using Ads.Database;
using Ads.Services;
using System.IO;
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
        public ActionResult Upload()
        {
            using (FileStream fileStream = System.IO.File.Create(Server.MapPath("~/App_Data/uploads.png"), (int)Request.InputStream.Length))
            {
                byte[] bytesinStream = new byte[Request.InputStream.Length];
                Request.InputStream.Read(bytesinStream, 0, (int)bytesinStream.Length);
                fileStream.Write(bytesinStream, 0, bytesinStream.Length);
            }
                
            //var something = Request.Files;
            //foreach (string aaa in Request.Files)
            //{
            //    var fileContent = Request.Files[file];

            //        }

            //if (file != null && file.ContentLength > 0)
            //{
            //    var fileName = Path.GetFileName(file.FileName);
            //    var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
            //    file.SaveAs(path);
            //}
            return RedirectToAction("Index");
        }
        private Advert GetAdvertById(int advertId)
        {
            return _adsDbContext.GetById(advertId);
        }
    }
}
