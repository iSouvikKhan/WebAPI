using MVC2.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace MVC2.Controllers
{
    public class UserMVCController : Controller
    {

        Uri baseaddress = new Uri("https://localhost:44391/api");

        HttpClient client;

        public UserMVCController()
        {
            client = new HttpClient();
            client.BaseAddress = baseaddress;
        }

        // GET
        public ActionResult Index()
        {

            List<UserMVC> modelList = new List<UserMVC>();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user").Result;
            if (response.IsSuccessStatusCode)
            {
                // Http client object returns data as a JSON string
                string data = response.Content.ReadAsStringAsync().Result;
                modelList = JsonConvert.DeserializeObject<List<UserMVC>>(data);
            }
            return View(modelList);
        }

        // Add
        public ActionResult Create()
        {
            return View();
        }

        // Add
        [HttpPost]
        public ActionResult Create(UserMVC model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(client.BaseAddress + "/user", content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }


        // Edit
        public ActionResult Edit(int id)
        {
            // make our get request for getting a single request
            UserMVC model = new UserMVC();
            HttpResponseMessage response = client.GetAsync(client.BaseAddress + "/user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                // Http client object returns data as a JSON string
                string data = response.Content.ReadAsStringAsync().Result;
                model = JsonConvert.DeserializeObject<UserMVC>(data);
            }
            return View("Create",model); // using the existing view, I am not creating a new view
        }

        // Edit
        [HttpPost]
        public ActionResult Edit(UserMVC model)
        {
            string data = JsonConvert.SerializeObject(model);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PutAsync(client.BaseAddress + "/user/" + model.UserId, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View("Create", model);
        }

        // Delete
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = client.DeleteAsync(client.BaseAddress + "/user/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("index");
            }
            return RedirectToAction("index");
            // We didn't use AJAX here
            // It means whether delete operation is successful or not, it will keep reloading the page on
        }

    }
}