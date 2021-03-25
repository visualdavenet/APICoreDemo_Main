using APICoreDemo.Models;
using APICoreDemo.DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using APICoreDemo.Services;
using System.Net;
using System.Data;

namespace APICoreDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult API()
        {
            return View();
        }

        public ActionResult CustomerList()
        {
            ViewBag.Message = "Customer List";

            var result = DataLogic.LoadCustomers();
            List<Customer> customer = new List<Customer>();

            foreach (var row in result)
            {
                customer.Add(new Customer
                {
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Occupation = row.Occupation,
                    City = row.City,
                    State = row.State,
                    Email = row.Email,
                    ImageURL = row.ImageURL
                });
            }

            return View(customer);
        }

        public ActionResult SignUp()
        {
            ViewBag.Message = "Customer Sign Up";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(Customer customer)
        {
            if (ModelState.IsValid)
            {
                int recordsCreated = DataLogic.CreateCustomer(
                    customer.FirstName,
                    customer.LastName,
                    customer.Occupation,
                    customer.City,
                    customer.State,
                    customer.Email,
                    customer.ImageURL);

                return RedirectToAction("CustomerList");
            }

            return View();
        }

        public JsonResult GetCustomers()
        {
            ViewBag.Message = "Customer List";

            var result = DataLogic.LoadCustomers();
            List<Customer> customer = new List<Customer>();

            foreach (var row in result)
            {
                customer.Add(new Customer
                {
                    FirstName = row.FirstName,
                    LastName = row.LastName,
                    Occupation = row.Occupation,
                    City = row.City,
                    State = row.State,
                    Email = row.Email,
                    ImageURL = row.ImageURL
                });
            }

            return Json(customer);
        }

        public ActionResult RSS()
        {
            List<RssNews> newsList = ReadNews("https://www.espn.com/espn/rss/news");
            return View(newsList);
        }

        public static List<RssNews> ReadNews(string url)
        {
            var webResponse = WebRequest.Create(url).GetResponse();
            if (webResponse == null)
                return null;
            var ds = new DataSet();
            ds.ReadXml(webResponse.GetResponseStream());

            var news = (from row in ds.Tables["item"].AsEnumerable()
                        select new RssNews
                        {
                            Title = row.Field<string>("title"),
                            Image_URL = row.Field<string>("image"),
                            Description = row.Field<string>("description"),
                            Link = row.Field<string>("link")
                        }).ToList();
            return news;
        }
    }
}
