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
using System.Net.Http;
using Newtonsoft.Json;

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
                    ID = row.ID,
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

        public ActionResult SignUp(int ID)
        {
            if (ID != 0)
            {
                CustomerDataModel customer = DataLogic.GetCustomer(ID);
                ViewBag.Message = "Update Customer";
                return View(customer);
            }

            ViewBag.Message = "Add New Customer";

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
                if (customer.ID != null)
                {
                    int recordsUpdated = DataLogic.UpdateCustomer(
                        customer.ID,
                        customer.FirstName,
                        customer.LastName,
                        customer.Occupation,
                        customer.City,
                        customer.State,
                        customer.Email,
                        customer.ImageURL);
                }
                else
                {
                    int recordsCreated = DataLogic.CreateCustomer(
                        customer.FirstName,
                        customer.LastName,
                        customer.Occupation,
                        customer.City,
                        customer.State,
                        customer.Email,
                        customer.ImageURL);
                }
                return RedirectToAction("CustomerList");
            }

            return View();
        }

        public ActionResult DeleteCustomer(int ID)
        {
            var result = DataLogic.DeleteCustomer(ID.ToString());
            return RedirectToAction("CustomerList");
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

        public async Task<IActionResult> GetCustomersAPI()
        {
            List<Customer> customerList = new List<Customer>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://apihost.somee.com/mainapi"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    customerList = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                }
            }
            return View(customerList);
        }
    }
}
