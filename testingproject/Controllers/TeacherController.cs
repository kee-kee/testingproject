using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using testingproject.Models;
namespace testingproject.Controllers
{
    public class TeacherController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "fRmLJRAABUE5pP3AO56XyOjh9N7jft11VvjcGCff",
            BasePath = "https://test-88a40.firebaseio.com/"
        };
        IFirebaseClient client;
        // GET: Teacher
        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Teachers");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Teacher>();
            foreach (var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Teacher>(((JProperty)item).Value.ToString()));
            }
            //FirebaseResponse response
            return View(list);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Teacher teacher)
        {
            try
            {
                AddTeacherToFirebase(teacher);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return View();
        }
        private void AddTeacherToFirebase(Teacher teacher)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = teacher;
            PushResponse response = client.Push("Teachers/", data);
            data.staff_ID = response.Result.name;
            SetResponse setResponse = client.Set("Teachers/" + data.staff_ID, data);
        }
        [HttpGet]
        public ActionResult Detail(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Teachers/" + id);
            Teacher data = JsonConvert.DeserializeObject<Teacher>(response.Body);
            return View(data);
        }
        [HttpGet]
        public ActionResult Edit(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Teachers/" + id);
            Teacher data = JsonConvert.DeserializeObject<Teacher>(response.Body);
            return View(data);

        }
        [HttpPost]
        public ActionResult Edit(Teacher teacher)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Teachers/" + teacher.staff_ID,teacher);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Teachers/" + id);
            return RedirectToAction("Index");

        }
    }
}