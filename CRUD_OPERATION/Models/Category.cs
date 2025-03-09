using System.ComponentModel.DataAnnotations;

namespace CRUD_OPERATION.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public ICollection<Product> products { get; set; }

    }
}
