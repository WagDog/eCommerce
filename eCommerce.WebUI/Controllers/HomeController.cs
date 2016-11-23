using eCommerce.DAL.Data;
using eCommerce.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using eCommerce.Model;

namespace eCommerce.WebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            CustomerRepository customers = new CustomerRepository(new DataContext());
            ProductRepository products = new ProductRepository(new DataContext());
            MembershipTypeRepository membershipType  = new MembershipTypeRepository(new DataContext());

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
    }
}