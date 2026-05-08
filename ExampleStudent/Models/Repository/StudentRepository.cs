using ExampleStudent.Data;
using ExampleStudent.Models.Domain;
using ExampleStudent.Models.ViewModel;

namespace ExampleStudent.Models.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private SchoolDbContext dbContext;

        public StudentRepository(SchoolDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IEnumerable<Student> GetAll(string? searchString, string? type)
        {
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                var List = from l in dbContext.Students // lấy toàn bộ liên kết
                           select l;

                if (type == "Mssv")
                {
                    return List.Where(s => s.Mssv.Contains(searchString)); // lọc theo chuỗi tìm kiếm
                }
                else
                {
                    return List.Where(s => s.Name.Contains(searchString)); // lọc theo chuỗi tìm kiếm
                }
            }
            else
            {
                return dbContext.Students;
            }
        }

        public VMStudent? GetStudentsById(int id)
        {
            var student = dbContext.Students.FirstOrDefault(p => p.Id == id);
            if (student != null)
            {
                string GenderVm;
                if (student.Gender == false) GenderVm = "female"; else GenderVm = "male";

                var studentVM = new VMStudent()
                {
                    Id = id,
                    Name = student.Name,
                    Birth = student.Birth,
                    ImgUrl = student.ImgUrl,
                    Gender = GenderVm,
                    Mssv = student.Mssv,
                    Description = student.Description,
                };
                return studentVM;
            }
            return null;
        }

        public void UpdateStudentById(int id, VMStudent model)
        {
            var StudentById = dbContext.Students.FirstOrDefault(p => p.Id == id);
            if (StudentById != null)
            {
                StudentById.Name = model.Name;
                StudentById.Birth = model.Birth;

                if (model.Gender == "male") StudentById.Gender = true;
                else StudentById.Gender = false;

                StudentById.ImgUrl = model.ImgUrl;
                StudentById.Mssv = model.Mssv;
                StudentById.Description = model.Description;

                dbContext.Update(StudentById);
                dbContext.SaveChanges();
            }
        }

        public void AddStudent(VMStudent Model)
        {
            bool GenderData;
            if (Model.Gender == "male") GenderData = true;
            else GenderData = false;

            var student = new Student()
            {
                Name = Model.Name,
                Birth = Model.Birth,
                Gender = GenderData,
                ImgUrl = Model.ImgUrl,
                Mssv = Model.Mssv,
                Description = Model.Description
            };

            dbContext.Students.Add(student);
            dbContext.SaveChanges();
        }

        public void DeleteStudentById(int id)
        {
            var student = dbContext.Students.FirstOrDefault(p => p.Id == id);
            if (student != null)
            {
                dbContext.Students.Remove(student);
                dbContext.SaveChanges();
            }
        }
    }
}
