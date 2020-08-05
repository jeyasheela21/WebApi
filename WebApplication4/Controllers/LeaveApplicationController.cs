using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using System.Net.Http;
namespace WebApplication4.Controllers
{
    public class LeaveApplicationController : Controller
    {
        // GET: LeaveApplication
        //displays all the values
        public ActionResult LeaveApplication()
        {
            //call the api here
            
                IEnumerable<LeaveDetail> empobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44324/api/LeaveApplicationApi");
                var consumeapi = hc.GetAsync("LeaveApi");
                consumeapi.Wait();
                var readdata = consumeapi.Result;
            //check that codition if its true return to the view
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<List<LeaveDetail>>();
                    displaydata.Wait();
                    empobj = displaydata.Result;

                }

                return View(empobj);
            }
        public ActionResult Create()
        {
            return View();
        }
        // get particular value used on id
        [HttpPost]
        public ActionResult Create(LeaveDetail insertemp)
        {
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44351/api/LeaveApplicationApi");
            var insertrecord = hc.PostAsJsonAsync<LeaveDetail>("LeaveApplicationApi", insertemp);
            insertrecord.Wait();
            var savedata = insertrecord.Result;
            if (savedata.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");

            }
            return View("Create");
        }
        //display the all the values uneditable mode
       // GET: Leave/Details/5
        public ActionResult Details(int id)
        {

            LeaveDetail empobj = null;
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri("https://localhost:44351/api/");
            var consumeapi = hc.GetAsync("LeaveApplicationApi?id=" + id.ToString());
            consumeapi.Wait();
            var readdata = consumeapi.Result;
            if (readdata.IsSuccessStatusCode)
            {
                var displaydata = readdata.Content.ReadAsAsync<LeaveDetail>();
                displaydata.Wait();
                empobj = displaydata.Result;
            }
            return View(empobj);
        }




        
        //Get the values from database with editable mode
        //GET: Leave/Edit/5
        public ActionResult Edit(int id)

            {
                LeaveDetail empobj = null;
                HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44351/api/");
                var consumeapi = hc.GetAsync("LeaveApplicationApi?id=" + id.ToString());
                consumeapi.Wait();
                var readdata = consumeapi.Result;
                if (readdata.IsSuccessStatusCode)
                {
                    var displaydata = readdata.Content.ReadAsAsync<LeaveDetail>();
                    displaydata.Wait();
                    empobj = displaydata.Result;
                }
                return View(empobj);
            }
        //after editing it will be post the values to database
            // PUT: Leave/Edit/5
            [HttpPost]

            public ActionResult Edit(LeaveDetail emp)
            {
                try
                {
                    // TODO: Add update logic here

                    HttpClient hc = new HttpClient();
                    hc.BaseAddress = new Uri("https://localhost:44351/api/");
                    var insertrecord = hc.PutAsJsonAsync<LeaveDetail>("LeaveApplicationApi", emp);

                    insertrecord.Wait();
                    var savedata = insertrecord.Result;
                    if (savedata.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");


                    }
                    else
                    {
                        ViewBag.message("record updated");
                    }
                    return View();
                }
                catch
                {
                    return View("Index");
                }
            }
        
            // GET: Leave/Delete/5
            public ActionResult Delete(int id)
            {
                return View();
            }

            // POST: Leave/Delete/5
            [HttpPost]
            public ActionResult Delete(int id, FormCollection collection)
            {
                try
                {
                    // TODO: Add delete logic here

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }

        }
    }


