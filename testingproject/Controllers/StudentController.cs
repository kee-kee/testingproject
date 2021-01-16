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
    [Authorize]
    public class StudentController : Controller
    {
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "fRmLJRAABUE5pP3AO56XyOjh9N7jft11VvjcGCff",
            BasePath = "https://test-88a40.firebaseio.com/"
        };
        IFirebaseClient client;
        // GET: Student

        public ActionResult Index()
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Get("Students");
            dynamic data = JsonConvert.DeserializeObject<dynamic>(response.Body);
            var list = new List<Student>();
            foreach(var item in data)
            {
                list.Add(JsonConvert.DeserializeObject<Student>(((JProperty)item).Value.ToString()));
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
        public ActionResult Create(Student student)
        {
            try
            {
                AddStudentToFirebase(student);
                ModelState.AddModelError(string.Empty, "Added Successfully");
            }
            catch (Exception ex)
            {

                ModelState.AddModelError(string.Empty,ex.Message);
            }
            return View();

        }

        private void AddStudentToFirebase(Student student)
        {
            client = new FireSharp.FirebaseClient(config);
            var data = student;
            PushResponse response = client.Push("Students/", data);
            data.student_ID=response.Result.name;
            SetResponse setResponse = client.Set("Students/" + data.student_ID, data);
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
            FirebaseResponse response = client.Get("Students/" + id);
            Student data = JsonConvert.DeserializeObject<Student>(response.Body);
            return View(data);

        }

        [HttpPost]
        public ActionResult Edit(Student student)
        {
            client = new FireSharp.FirebaseClient(config);
            SetResponse response = client.Set("Students/" + student.student_ID, student);
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult Delete(string id)
        {
            client = new FireSharp.FirebaseClient(config);
            FirebaseResponse response = client.Delete("Students/" + id);
            return RedirectToAction("Index");

        }
    }
}