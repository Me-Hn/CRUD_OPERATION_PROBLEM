using System.ComponentModel.DataAnnotations;

namespace CRUD_OPERATION.Models
{
    public class Employee
    {


        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
