using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FRB_Projects.Models
{
    public class Student
    {
        [Key]
        public int StuId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? FatherName{ get; set; }
        [Required]
        public string? Age{ get; set; }
        [Required]
        [DisplayName("Class")]
        public string? Standard { get; set; }
        [NotMapped]
        [DisplayName("Profile Picture")]
        public IFormFile? UploadPic { get; set; }
        public string? ImageUrl { get; set; }



    }
}
