using Ads.Database;
using Ads.Services;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Ads.Controllers
{
    public class ProductsController : Controller
    {
        public JsonResult GetAll()
        {
            var products = new List<Product>
            {
                new Product {
                    Currency="$",
                    Description="",
                    Id=1,
                    Image="http://www.pplandscape-gardening.co.uk/images/6mibsYZgW5q/nike-men-black-red-2011-air-presto-84QZ.jpg",
                    Price=90,
                    Title="Professional Running Shoes",
                    OldPrice=150,
                    Url="http://www.nike.com"
                },

                new Product
                {
                    Currency="RON",
                    Image="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRn8whj7DbffK_ax0FhAd-pOiXRxmV2lRIPp-aYjCzgezdvsdB-5w",
                    Id=2,
                    Price=300,
                    OldPrice=449,
                    Title="Professional Bascketball Shoes",
                    Url="http://www.nike.com",
                    Description=""
                },

                new Product
                {
                    Currency="RON",
                    Image="http://www.katiesproperpate.co.uk/images/w5APNBV2qxy/air-max-2013-nike-shoes-30MP.jpg",
                    Id=3,
                    Price=249,
                    OldPrice=399,
                    Title="Confort Shoes",
                    Url="http://www.nike.com",
                    Description=""
                },

                new Product
                {
                    Currency="$",
                    Image="data:image/jpeg;base64,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",
                    Id=4,
                    Price=199,
                    OldPrice=249,
                    Title="Walking shoes",
                    Url="www.nike.com",
                    Description=""
                },
            };
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}
