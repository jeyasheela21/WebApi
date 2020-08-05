using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using WebApplication4.Models;
namespace WebApplication4.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        LeavesDBEntities lm = new LeavesDBEntities();
        public ActionResult Index()
        {

            return View(lm);

        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Employee insertemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44351/api/Values");
            var insertrecord = hc.PostAsJsonAsync<Employee>("Values", insertemp);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View("Create");
        }

        public ActionResult Details()
        {
            Employee empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44351/api/");
            var consumeapi = hc.GetAsync("Values");
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<Employee>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }

        public ActionResult Edit(int id)
        {
            Employee empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44351/api/");
            var consumeapi = hc.GetAsync("Values?id=" + id);
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<Employee>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }
        [HttpPost]
        public ActionResult Edit(Employee insertemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44351/api/");
            var insertrecord = hc.PutAsJsonAsync<Employee>("Values", insertemp);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            //else
            //{
            //    ViewBag.message("record updated");
            //}
            return View(insertemp);
        }
    }
}