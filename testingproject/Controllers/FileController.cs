using Firebase.Auth;
using Firebase.Storage;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using testingproject.Models;

namespace testingproject.Controllers
{
    public class FileController : Controller
    {
        private static string apiKey = "AIzaSyCZtn7vQ2nsmeu694wjoXbFidiSlXztpBo";
        private static string bucket = "test-88a40.appspot.com";
        private static string AuthEmail = "thh@gmail.com";
        private static string AuthPassword = "teacher1";
        // GET: File
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> Index(Department department, HttpPostedFileBase file)
        {
            FileStream stream;
            if (file.ContentLength > 0)
            {
               
                    string path = Path.Combine(Server.MapPath("~/Content/images/Eng"), file.FileName);
                    file.SaveAs(path);
                    stream = new FileStream(Path.Combine(path), FileMode.Open);
                    await Task.Run(() => Upload(stream, file.FileName, department));
                
            }
            return View();
        } 
        public async void Upload(FileStream stream, string fileName, Department departmentName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images/Eng")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);
            try
            {
                string link = await task;
                //Console.WriteLine("Added Successfully!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }
        }
        public async void UploadMath(FileStream stream, string fileName, Department departmentName)
        {
            var auth = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);
            var cancellation = new CancellationTokenSource();
            var task = new FirebaseStorage(
                bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                })
                .Child("images/Math")
                .Child(fileName)
                .PutAsync(stream, cancellation.Token);
            try
            {
                string link = await task;
                //Console.WriteLine("Added Successfully!");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception was thrown: {0}", ex);
            }


        }

    }
}