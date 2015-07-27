using System.Collections.Generic;
using UnitTestingWithNBuilderAndFakeDbSet.Models;

namespace UnitTestingWithNBuilderAndFakeDbSet.ViewModels
{
    public class ProductIndexViewModel
    {
        public bool IncludeDiscontinued { get; set; }
        public List<Product> Products { get; set; }
    }
}