using System.ComponentModel.DataAnnotations;

namespace CRUD_OPERATION.Models
{
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name="Student Name")]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [MaxLength(20)]
        public string? Email { get; set; }

        public string Image { get; set; }

        [Required]
        
        public string Password { get; set; }
    }
}
