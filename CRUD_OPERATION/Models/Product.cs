namespace CRUD_OPERATION.Models
{
    public class Product
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
