using Ads.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Ads.Controllers
{
    public class HomeController : Controller
    {
        private IUserDbContext _userDbContext;
        public HomeController()
        {
            _userDbContext = new UserDbContext();
        }
        public ActionResult Login(string userName, string password)
        {
            var users = _userDbContext.FindByEmail(userName);
            if (users.Count() == 0)
            {
                ViewData["hasErrors"] = true;
                return View("Index");

            }

            var md5Password = CalculateMD5Hash(password);
            var user = users.Where(x => x.Password.Equals(md5Password)).FirstOrDefault();
            if (user == null)
            {
                ViewData["hasErrors"] = true;
                return View("Index");
            }

            return RedirectToAction("Index", "Configuration");
        }

        private string CalculateMD5Hash(string input)

        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}