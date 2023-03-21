using FRB_Projects.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FRB_Projects.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentContext _context;

        private readonly IWebHostEnvironment _env;
        public StudentController(StudentContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<Student> obj = _context.Students.ToList();
            return View(obj);
        }
        public IActionResult Create()
        {

            return View();
        }


        //[HttpPost]

        public IActionResult Create(Student stu, IFormFile UploadPic)
        {

            if (UploadPic != null)
            {
                string path = Path.Combine(_env.WebRootPath, "Images");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var fileName = UploadPic.FileName;

                var filePath = Path.Combine(path, fileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {

                    UploadPic.CopyTo(fs);

                }
                stu.ImageUrl = filePath;

                string storeImg = Path.Combine(path);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                stu.ImageUrl = "/Images/" + fileName;
                using (FileStream fs = System.IO.File.Create(filePath))

                {

                    UploadPic.CopyTo(fs);

                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Students.Add(stu);
                    _context.SaveChanges();
                    return new JsonResult("Data Succefull save");
                    // return RedirectToAction("Index");
                }

                return View(stu);
            }
            return View();
        }
            
               
        

        //public IActionResult CreateStudent(int? id)
        //{
        //    var student = _context.Students.Find(id);
        //    _context.Students.Add(student);
        //    _context.SaveChanges();
        //    return new JsonResult("Data Succefull save");
        //}

        public IActionResult Edit(int id)
        {
            return View();

        }
        public IActionResult EditStudent(int id)
        {
            var stu = _context.Students.Find(id);
            _context.Students.Update(stu);
            _context.SaveChanges();
            return new JsonResult("Updated Successfull");

        }
        [HttpPost]
        public IActionResult Edit(Student stu, IFormFile UploadPic)
        {
            var EmployeeInDb = _context.Students.Find(stu.StuId);
            if (UploadPic != null)
            {
                string path = Path.Combine(_env.WebRootPath, "Images");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var fileName = UploadPic.FileName;

                var filePath = Path.Combine(path, fileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {

                    UploadPic.CopyTo(fs);

                }
                stu.ImageUrl = "/Images/" + fileName;

            }

            if (ModelState.IsValid)
            {
                _context.Entry(EmployeeInDb).State = EntityState.Detached;
                _context.Students.Update(stu);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }
        public async Task<IActionResult> Details(int? id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(m => m.StuId == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }
        public IActionResult Delete(int id)
        {
            var imagePath = Path.Combine(_env.WebRootPath, "Images");
            string filePath = Path.Combine(imagePath);
            FileInfo file = new FileInfo(filePath);
            var obj = _context.Students.Find(id);

            string pathToDeleteImage = _env.WebRootPath + "\\" + obj.ImageUrl.Replace("/", "\\");
            if (System.IO.File.Exists(pathToDeleteImage))
                System.IO.File.Delete(pathToDeleteImage);
            var student = _context.Students.Find(id);

            _context.Students.Remove(student);
            _context.SaveChanges();
            return new JsonResult("Deleted Successfully");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Student stu)
        {
            var student = _context.Students.Find(id);
            _context.Students.Remove(stu);
            _context.SaveChanges();
            return View();
        }

        public JsonResult GetStudents()
        {
            return GetAll();
        }
        [HttpGet]
        public JsonResult GetAll()
        {

            var Res = _context.Students.OrderByDescending(x => x.StuId).ToList();
            var Data = new
            {
                data = Res,
                TotalCount = Res.Count

            };
            return new JsonResult(Data);
        }

        public IActionResult StudentForm(int id)
        {
            if (id == 0)
            {
                return PartialView("_student");
            }
            else
            {
                Student d = _context.Students.Find(id);
                return PartialView("_student", d);
            }
        }

        public IActionResult SaveStudent(Student stu)
        {

            if (stu.StuId == 0)
            {
                _context.Students.Add(stu);
                _context.SaveChanges();
                return new JsonResult("done");
            }
            else if (stu.StuId != null)
            {
                // var upd = _context.Students.Find(stu.StuId);
                _context.Students.Update(stu);
                _context.SaveChanges();
                return new JsonResult("updated");
            }
            return new JsonResult("false");
        }


    }
}
