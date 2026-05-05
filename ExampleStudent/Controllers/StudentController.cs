using ExampleStudent.Data;
using ExampleStudent.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleStudent.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        //private IStudentRepository _studentRepository;
        private readonly SchoolDbContext context;
       
        public StudentController(SchoolDbContext _context)
        {
            context = _context;
        }

        [Route("Student/ListAll")]
        public IActionResult ListAll()
        {
            var allstu = context.Students.ToList();
            return View(allstu);
        }

        public IActionResult GetById(int id)
        {
            var student = context.Students.FirstOrDefault(p => p.Id == id);
            if (student == null)
            {
                return NotFound("Không tìm thấy MSSV: " + id);
            }
            return View(student);
        }

        public IActionResult Delete(int id)
        {
            var student = context.Students.Find(id);
            if (student != null)
            {
                context.Students.Remove(student);
                context.SaveChanges();
                TempData["successMessage"] = "Xóa sinh viên thành công!";
            }
            else
            {
                TempData["errorMessage"] = "Không tìm thấy sinh viên để xóa.";
            }

            return RedirectToAction("ListAll");
        }

        [HttpPost]
        public IActionResult AddStudent(Student newStudent)
        {
            if (ModelState.IsValid)
            {
                context.Students.Add(newStudent);
                context.SaveChanges();

                TempData["successMessage"] = "Thêm sinh viên thành công!";
                return RedirectToAction("getall"); 
            }

            TempData["errorMessage"] = "Dữ liệu không hợp lệ!";
            return View(newStudent);
        }

        public IActionResult Edit(int id)
        {
            return Content("Trang chỉnh sửa sinh viên ID: " + id);
        }
    }
}