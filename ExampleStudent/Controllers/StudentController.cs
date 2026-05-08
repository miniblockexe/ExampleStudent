using ExampleStudent.Data;
using ExampleStudent.Models.Domain;
using ExampleStudent.Models.Repository;
using ExampleStudent.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExampleStudent.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private IStudentRepository _studentRepository;
        private readonly SchoolDbContext context;

        //public StudentController(SchoolDbContext _context)
        //{
        //    context = _context;
        //} 
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public IActionResult GetAll(string? searchString,string? type)
        {
            var allstu = _studentRepository.GetAll(searchString, type);
            return View(allstu);
        }

        //[Route("Student/ListAll")]
        //public IActionResult ListAll()
        //{
        //    var allstu = context.Students.ToList();
        //    return View(allstu);
        //}

        public IActionResult GetStudentById(int id)
        {
            //var student = context.Students.FirstOrDefault(p => p.Id == id);
            var student = _studentRepository.GetStudentsById(id);
            if (student != null)
            {
                //return NotFound("Không tìm thấy MSSV: " + id);
                return View(student);
            }else return NotFound("Không tìm thấy MSSV: " + id);
            //return View(student);
        }

        //public IActionResult Delete(int id)
        //{
        //    var student = context.Students.Find(id);
        //    if (student != null)
        //    {
        //        context.Students.Remove(student);
        //        context.SaveChanges();
        //        TempData["successMessage"] = "Xóa sinh viên thành công!";
        //    }
        //    else
        //    {
        //        TempData["errorMessage"] = "Không tìm thấy sinh viên để xóa.";
        //    }

        //    return RedirectToAction("ListAll");
        //}
        //public IActionResult DelStudentById(int id)
        //{
        //    var StudentById = context.Students.FirstOrDefault(n => n.Id == id);
        //    if (StudentById != null)
        //    {
        //        context.Students.Remove(StudentById);
        //        context.SaveChanges();
        //        TempData["successMessage"] = "Deleted";
        //        return RedirectToAction("GetAll");
        //    }
        //    else
        //    {
        //        return View("NotFound");
        //    }
        //}

        //[HttpPost]
        //public IActionResult AddStudent(Student newStudent)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        context.Students.Add(newStudent);
        //        context.SaveChanges();

        //        TempData["successMessage"] = "Thêm sinh viên thành công!";
        //        return RedirectToAction("getall");
        //    }

        //    TempData["errorMessage"] = "Dữ liệu không hợp lệ!";
        //    return View(newStudent);
        //}

        //public IActionResult Edit(int id)
        //{
        //    return Content("Trang chỉnh sửa sinh viên ID: " + id);
        //}
        //[HttpGet]
        //public IActionResult EditStudentById(int id)
        //{
        //    var Student = context.Students.FirstOrDefault(p => p.Id == id);
        //    if (Student != null)
        //    {
        //        string GenderVm;
        //        if (Student.Gender == false) GenderVm = "female"; else GenderVm = "male";

        //        var studentVM = new VMStudent()
        //        {
        //            Name = Student.Name,
        //            Birth = Student.Birth,
        //            ImgUrl = Student.ImgUrl,
        //            Gender = GenderVm,
        //            Mssv = Student.Mssv,
        //            Description = Student.Description,
        //        };
        //        return View(studentVM);
        //    }
        //    else
        //    {
        //        return View("NotFound");
        //    }
        //}

        //[HttpPost]
        //public IActionResult EditStudentById([FromRoute] int id, VMStudent student)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            var StudentById = context.Students.FirstOrDefault(p => p.Id == id);
        //            if (StudentById != null)
        //            {
        //                StudentById.Name = student.Name;
        //                StudentById.Birth = student.Birth;

        //                if (student.Gender == "male") StudentById.Gender = true;
        //                else StudentById.Gender = false;

        //                StudentById.ImgUrl = student.ImgUrl;
        //                StudentById.Mssv = student.Mssv;
        //                StudentById.Description = student.Description;

        //                context.SaveChanges();
        //                TempData["successMessage"] = "Successful";
        //                return RedirectToAction("GetAll");
        //            }
        //            else
        //            {
        //                return View("NotFound");
        //            }
        //        }
        //        else
        //        {
        //            TempData["errorMessage"] = "data is not valid";
        //            return View();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        TempData["errorMessage"] = ex.Message;
        //        return View();
        //    }
        //}
        public IActionResult EditStudentById(int id)
        {
            var studentVM = _studentRepository.GetStudentsById(id);
            if (studentVM != null)
            {
                return View(studentVM);
            }
            else
            {
                return View("NotFound");
            }
        }

        [HttpPost]
        public IActionResult EditStudentById([FromRoute] int id, VMStudent student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var StudentById = _studentRepository.GetStudentsById(id);
                    if (StudentById != null)
                    {
                        _studentRepository.UpdateStudentById(id, student);
                        TempData["successMessage"] = "Successful";
                        return RedirectToAction("GetAll");
                    }
                    else
                    {
                        return View("NotFound");
                    }
                }
                else
                {
                    TempData["errorMessage"] = "Data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }
        [HttpGet]
        public IActionResult AddStudent()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(VMStudent studentData)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _studentRepository.AddStudent(studentData);
                    TempData["successMessage"] = "Successful";
                    return RedirectToAction("GetAll");
                }
                else
                {
                    TempData["errorMessage"] = "Data is not valid";
                    return View();
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View();
            }
        }

        public IActionResult DelStudentById(int id)
        {
            var StudentById = _studentRepository.GetStudentsById(id);
            if (StudentById != null)
            {
                _studentRepository.DeleteStudentById(id);
                TempData["successMessage"] = "Deleted";
                return RedirectToAction("GetAll");
            }
            else
            {
                return View("NotFound");
            }
        }
    }
}