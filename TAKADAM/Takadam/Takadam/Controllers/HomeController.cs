using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takadam.Models;

namespace Takadam.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            //get All
            List<Product> allProduct = Product.GetAll();

            //Get By Id
            Product prod = Product.GetById(1);

            //Get By GetByCategoryId
            List<Product> byCateg = Product.GetByCategoryId(1);

            //Get By GetByStatus
            List<Product> byStatus = Product.GetByStatus("eget tincidunt");

            //Get By GetByCountry
            List<Product> byCountry = Product.GetByCountry("Brazil");

            return View();
        }
    }
}