using System.ComponentModel.DataAnnotations;

namespace UnitTestingWithNBuilderAndFakeDbSet.Models
{
    public class Product
    {
        public string Description { get; set; }
        [Key]
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDiscontinued { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
    }
}