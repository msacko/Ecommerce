using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Takadam.Models;
using Takadam.Models.Model_View;

namespace Takadam.Controllers
{
    //Test mamoudou
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            AccueilModel model = new AccueilModel();
            model.list_prod = Product.GetAll();
            return View(model);
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