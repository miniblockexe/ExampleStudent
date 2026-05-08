using System.ComponentModel.DataAnnotations;

namespace ExampleStudent.Models.ViewModel
{
    public partial class VMStudent
    {
        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }

        [Required]
        public DateOnly Birth { get; set; }

        [Required]
        public string? Gender { get; set; }

        [Required]
        public string? ImgUrl { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Mssv { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
