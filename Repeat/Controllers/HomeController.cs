using System;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Repeat.Models;
using Repeat.ViewModels;

namespace Repeat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public HomeController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public IActionResult Index()
        {
            var studentList = _studentRepository.GetAllStudents();
            return View(studentList);
        }

        public IActionResult Details(int id)
        {
            var studentModel = _studentRepository.GetStudent(id);
            if (studentModel == null)
            {
                Response.StatusCode = 404;
                return View("StudentNotFound", id);
            }
            var homeDetails = new HomeDetailsViewModel { PageTitle = "Student Details", Student = studentModel };
            return View(homeDetails);
        }

        public IActionResult Delete(int id)
        {
            DeletePhoto(id);
            var studentModel = _studentRepository.Delete(id);
            return View("Index", _studentRepository.GetAllStudents());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var studentModel = _studentRepository.GetStudent(id);
            var studentEditViewModel = new StudentEditViewModel
            {
                Id = studentModel.Id,
                FirstName = studentModel.FirstName,
                LastName = studentModel.LastName,
                Email = studentModel.Email,
                ClassName = studentModel.ClassName,
                ExistingPhotoPath = studentModel.PhotoPath
            };
            return View(studentEditViewModel);
        }

        [HttpPost]
        public IActionResult Edit(StudentEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = null;
                if (model.Photo != null)
                {
                    DeletePhoto(model.Id);
                    uniqueFileName = UploadPhoto(model);
                }
                var student = _studentRepository.GetStudent(model.Id);
                student.FirstName = model.FirstName;
                student.LastName = model.LastName;
                student.Email = model.Email;
                student.ClassName = model.ClassName;
                student.PhotoPath = uniqueFileName;
                var updaterStudent = _studentRepository.Update(student);
                return RedirectToAction("Index", _studentRepository.GetAllStudents());
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadPhoto(model);
                var newStudent = new Student
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    ClassName = model.ClassName,
                    PhotoPath = uniqueFileName
                };
                _studentRepository.Add(newStudent);
                return RedirectToAction("Details", new { Id = newStudent.Id });
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private string UploadPhoto(StudentCreateViewModel model)
        {
            string uniqueFileName = null;
            if (model.Photo != null)
            {
                string uploadFolder = Path.Combine(Environment.CurrentDirectory, "wwwroot", "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        private void DeletePhoto(int id)
        {
            var photoPath = _studentRepository.GetStudent(id).PhotoPath;
            string filePath = Path.Combine(Environment.CurrentDirectory, "wwwroot", "img", photoPath);
            System.IO.File.Delete(filePath);
        }
    }
}
